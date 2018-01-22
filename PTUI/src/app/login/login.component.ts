import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { LoginService } from '../service/login.service';
import { MessagesService } from '../service/messages.service';
import { User } from '../model/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  hide = true;

  constructor(
    public dialogRef: MatDialogRef<LoginComponent>,
    private loginService: LoginService,
    private messageService: MessagesService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  login(Username: string, Password: string): void {
    this.loginService.login({ Username, Password } as User)
      .subscribe(
      res => { this.messageService.openSnackBar(`${Username} berhasil login`, 'Close') },
      err => { this.messageService.openSnackBar(`login gagal`, 'Close') }
      );
  }
}
