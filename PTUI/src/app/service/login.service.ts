import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { User } from '../model/user';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class LoginService {
  private loginUrl = 'http://localhost:5000/PT/login';

  constructor(private http: HttpClient) { }

  login(usr: User) : Observable<User> {
    return this.http.post<User>(this.loginUrl, usr, httpOptions);
      // .shareReplay();
  }
}
