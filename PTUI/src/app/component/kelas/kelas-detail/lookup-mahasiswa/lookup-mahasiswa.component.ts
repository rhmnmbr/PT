import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl } from '@angular/forms';

import { MahasiswaService } from '../../../../service/mahasiswa.service';
import { KelasService } from '../../../../service/kelas.service';
import { MessagesService } from '../../../../service/messages.service';
import { Mahasiswa } from '../../../../model/mahasiswa';
import { Kelas } from '../../../../model/kelas';
import { Dosen } from '../../../../model/dosen';
import { MhsKls } from '../../../../model/mhskls';


@Component({
  selector: 'app-lookup-mahasiswa',
  templateUrl: './lookup-mahasiswa.component.html',
  styleUrls: ['./lookup-mahasiswa.component.css']
})
export class LookupMahasiswaComponent implements OnInit {
  mhss: Mahasiswa[];
  kelas: Kelas;
  dosen: Dosen[];
  klsmhss: Mahasiswa[];
  order: string = 'Nim';
  sub: any;

  constructor(
    private mahasiswaService: MahasiswaService,
    private kelasService: KelasService,
    private messageService: MessagesService,
    public dialogRef: MatDialogRef<LookupMahasiswaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.getMhss();
    this.getKlsDet();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  getMhss(): void {
    this.mahasiswaService.getMhss()
      .subscribe(resp => {
        this.mhss = resp;
        this.kelasService.execute(false);
      });
  }

  getKlsDet() {
    this.kelasService.getKlsDet(this.data.kode)
      .subscribe(resp => {
        this.kelas = resp;
        this.dosen = resp.Dosen;
        this.klsmhss = resp.Mhss;
      });
  }

  enrollMhs(Nim: number, KodeKelas: string, Nama: string): void {
    this.kelasService.enrollMhs(KodeKelas, { Nim, KodeKelas } as MhsKls)
      .subscribe(res => {
        if (res) {
          this.kelasService.execute(true);
          this.dialogRef.close();
          this.messageService.openSnackBar(`${Nim}: ${Nama} berhasil ditambahkan`, 'Close');
        };
      });
  }

  confirmEnroll(Nim: number, KodeKelas: string, Nama: string): void {
    this.messageService.snackBar
      .open(`Tambahkan ${Nim}: ${Nama}?`, 'Confirm', { duration: 5000 })
      .onAction().subscribe(d => { this.enrollMhs(Nim, KodeKelas, Nama); });
  }

  isDisabled(mhsNim: number): boolean {
    let disabled = false;
    let klsmhss = this.klsmhss;
    if (klsmhss !== undefined) {
      if (klsmhss.find(x => x.Nim === mhsNim)) { disabled = true };
    };
    return disabled;
  }
}