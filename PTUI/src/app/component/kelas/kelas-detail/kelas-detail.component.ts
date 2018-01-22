import { Component, OnInit, AfterViewInit, ViewChild, Input} from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { Mahasiswa } from '../../../model/mahasiswa';
import { Kelas } from '../../../model/kelas';
import { Dosen } from '../../../model/dosen';
import { MhsKls } from '../../../model/mhskls';
import { KelasService } from '../../../service/kelas.service';
import { MessagesService } from '../../../service/messages.service';
import { LookupMahasiswaComponent } from './lookup-mahasiswa/lookup-mahasiswa.component';
import { UpdateNilaiComponent } from './update-nilai/update-nilai.component';


@Component({
  selector: 'app-kelas-detail',
  templateUrl: './kelas-detail.component.html',
  styleUrls: ['./kelas-detail.component.css']
})
export class KelasDetailComponent implements OnInit, AfterViewInit {
  @Input() mhss: Mahasiswa[];
  @Input() kelas: Kelas;
  @Input() dosen: Dosen[];

  private kode: string;
  private nim: number;
  private sub: any;

  displayedColumns = ['Nim', 'Nama', 'NilaiMid', 'NilaiSem', 'NilaiMutu', 'Buttons'];
  dataSource = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private kelasService: KelasService,
    private messageService: MessagesService,
    private route: ActivatedRoute,
    private location: Location,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.kode = params['kode'];
      this.getKlsDet();
    })
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getKlsDet() {
    this.kelasService.getKlsDet(this.kode)
      .subscribe(resp => {
        this.kelas = resp;
        this.dosen = resp.Dosen;
        this.mhss = resp.Mhss;
        this.dataSource.data = this.mhss;
      });
  }

  confirmUnEnroll(KodeKelas: string, Nim: number, Nama: string): void {
    this.messageService.snackBar
      .open(`UnEnroll ${Nim}: ${Nama}?`, 'Confirm', { duration: 5000 })
      .onAction().subscribe(d => { this.unEnrollMhs(KodeKelas, Nim, Nama); });
  }

  unEnrollMhs(KodeKelas: string, Nim: number, Nama: string): void {
    this.kelasService.unEnrollMhs(KodeKelas, Nim)
      .subscribe(d => this.getKlsDet());
    this.messageService.openSnackBar(`${Nim}: ${Nama} berhasil diunEnroll`, 'Close');
  }

  confirmUpdateMhs(KodeKelas: string, Nim: number, Nama: string, NilaiMid: number, NilaiSem: number): void {
    this.messageService.snackBar
      .open(`Update ${Nim}: ${Nama}?`, 'Confirm', { duration: 5000 })
      .onAction().subscribe(d => { this.updateMhs(KodeKelas, Nim, Nama, NilaiMid, NilaiSem); });
  }

  updateMhs(KodeKelas: string, Nim: number, Nama: string, NilaiMid: number, NilaiSem: number): void {
    this.kelasService.updateMhs(KodeKelas, Nim, {Nim, KodeKelas, NilaiMid, NilaiSem} as MhsKls)
      .subscribe(d => this.getKlsDet());
    this.messageService.openSnackBar(`${Nim}: ${Nama} berhasil diupdate`, 'Close');
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  openDialog(): void {
    let dialogRef = this.dialog.open(LookupMahasiswaComponent, {
      data: { kode: this.kode }
    });

    dialogRef.afterClosed().subscribe(result => {
      let exec = this.kelasService.executeMe();
      if (exec == true) { this.getKlsDet(); };
    });
  }

  openDialog2(kode: string, nim: number, nama: string, uts: number, uas: number): void {
    let dialogRef = this.dialog.open(UpdateNilaiComponent, {
      data: { kode, nim, nama, uts, uas }
    });

    dialogRef.afterClosed().subscribe(result => {
      let exec = this.kelasService.executeMe();
      if (exec == true) { this.getKlsDet(); };
      this.kelasService.execute(false);
    });
  }
}
