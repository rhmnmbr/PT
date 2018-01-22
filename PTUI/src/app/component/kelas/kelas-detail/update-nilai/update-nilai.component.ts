import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { KelasService } from '../../../../service/kelas.service';
import { MessagesService } from '../../../../service/messages.service';
import { MhsKls } from '../../../../model/mhskls';


@Component({
  selector: 'app-update-nilai',
  templateUrl: './update-nilai.component.html',
  styleUrls: ['./update-nilai.component.css']
})
export class UpdateNilaiComponent {
  options: FormGroup;
  myForm = new FormGroup({});

  constructor(
    private kelasService: KelasService,
    private messageService: MessagesService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<UpdateNilaiComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.options = fb.group({ floatLabel: 'always' });
    this.myForm = fb.group({ 'myNum': ['', [Validators.min(0), Validators.max(100)]] })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  confirmUpdateMhs(KodeKelas: string, Nim: number, Nama: string, NilaiMid: number, NilaiSem: number): void {
    this.messageService.snackBar
      .open(`Update ${Nim}: ${Nama}?`, 'Confirm', { duration: 5000 })
      .onAction().subscribe(d => { this.updateMhs(KodeKelas, Nim, Nama, NilaiMid, NilaiSem); });
  }

  updateMhs(KodeKelas: string, Nim: number, Nama: string, NilaiMid: number, NilaiSem: number): void {
    this.kelasService.updateMhs(KodeKelas, Nim, { Nim, KodeKelas, NilaiMid, NilaiSem } as MhsKls)
      .subscribe(res => {
        if (res) {
          this.kelasService.execute(true);
          this.dialogRef.close();
          this.messageService.openSnackBar(`Nilai ${Nim}: ${Nama} berhasil diUpdate`, 'Close');
        };
      });
  }
}
