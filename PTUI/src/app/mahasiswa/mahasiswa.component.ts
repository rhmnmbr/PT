import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

import { Mahasiswa } from '../model/mahasiswa';
import { MahasiswaService } from '../service/mahasiswa.service';
import { MessagesService } from '../service/messages.service';

@Component({
  selector: 'app-mahasiswa',
  templateUrl: './mahasiswa.component.html',
  styleUrls: ['./mahasiswa.component.css']
})

export class MahasiswaComponent implements OnInit, AfterViewInit {
  mhss: Mahasiswa[];
  displayedColumns = ['Nim', 'Nama'];
  dataSource = new MatTableDataSource();
  newNim: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private mahasiswaService: MahasiswaService,
    private messageService: MessagesService
  ) { }

  ngOnInit() {
    this.getMhss();
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getMhss(): void {
    this.mahasiswaService.getMhss()
      .subscribe(resp => {
        this.mhss = resp;
        this.dataSource.data = this.mhss;
        this.newNim = Math.max.apply(Math, this.mhss.map(function (o) { return o.Nim; })) + 1;
      });
  }

  add(Nim: number, Nama: string, TgLahir: string, TpLahir: string, JenisKelamin: string): void {
    Nama = Nama.trim();
    TgLahir = TgLahir.trim();
    TpLahir = TpLahir.trim();
    JenisKelamin = JenisKelamin.trim();
    if (!Nama || !TgLahir || !TpLahir || JenisKelamin.length != 1) { return; }
    this.mahasiswaService.addMhs({ Nim, Nama, TgLahir, TpLahir, JenisKelamin } as Mahasiswa)
      .subscribe(res => { if (res) { this.getMhss(); }; });
    this.messageService.openSnackBar(`${this.newNim}: ${Nama} berhasil ditambahkan`, 'Close');
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
}