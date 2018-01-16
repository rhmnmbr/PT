import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Mahasiswa } from '../../model/mahasiswa';
import { Kelas } from '../../model/kelas';
import { MahasiswaService } from '../../service/mahasiswa.service';
import { MessagesService } from '../../service/messages.service';

@Component({
  selector: 'app-mahasiswa-detail',
  templateUrl: './mahasiswa-detail.component.html',
  styleUrls: ['./mahasiswa-detail.component.css']
})
export class MahasiswaDetailComponent implements OnInit {
  @Input() mhs: Mahasiswa;
  @Input() klss: Kelas[];

  constructor(
    private route: ActivatedRoute,
    private mahasiswaService: MahasiswaService,
    private messageService: MessagesService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getMhsDet();
  }

  getMhsDet(): void {
    const nim = this.route.snapshot.paramMap.get('nim');
    this.mahasiswaService.getMhsDet(nim)
      .subscribe(resp => {
        this.mhs = resp;
        this.klss = resp.Klasses;
      });
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    this.mahasiswaService.updateMhs(this.mhs)
      .subscribe(u => this.goBack());
    this.messageService.openSnackBar(`${this.mhs.Nim} berhasil diupdate`, 'Close');
  }

  delete(): void {
    this.mahasiswaService.deleteMhs(this.mhs)
      .subscribe(d => this.goBack());
    this.messageService.openSnackBar(`${this.mhs.Nim}: ${this.mhs.Nama} berhasil dihapus`, 'Close');
  }

  confirmDelete(): void {
    this.messageService.snackBar
    .open(`Hapus ${this.mhs.Nim}: ${this.mhs.Nama} ?`, 'Confirm', { duration: 2000 })
    .onAction().subscribe(d => {this.delete();});
  }

  confirmSave(): void {
    this.messageService.snackBar
    .open(`Update ${this.mhs.Nim} ?`, 'Confirm', { duration: 2000 })
    .onAction().subscribe(d => {this.save();});
  }
}
