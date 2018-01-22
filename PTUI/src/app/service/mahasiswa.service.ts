import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { Mahasiswa } from '../model/mahasiswa';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class MahasiswaService {
  private mhssUrl = 'http://localhost:5000/PT/mahasiswa';

  constructor(private http: HttpClient) { }

  getMhss(): Observable<Mahasiswa[]> {
    return this.http.get<Mahasiswa[]>(this.mhssUrl);
  }

  addMhs(mhs: Mahasiswa): Observable<Mahasiswa> {
    return this.http.post<Mahasiswa>(this.mhssUrl, mhs, httpOptions);
  }

  getMhs(nim: string): Observable<Mahasiswa> {
    const url = `${this.mhssUrl}/${nim}`;
    return this.http.get<Mahasiswa>(url);
  }

  getMhsDet(nim: string): Observable<Mahasiswa> {
    const url = `${this.mhssUrl}/${nim}/detail`;
    return this.http.get<Mahasiswa>(url);
  }

  updateMhs(mhs: Mahasiswa): Observable<Mahasiswa> {
    const nim = typeof mhs === 'number' ? mhs : mhs.Nim;
    const url = `${this.mhssUrl}/${nim}`;
    return this.http.put<Mahasiswa>(url, mhs, httpOptions);
  }

  deleteMhs(mhs: Mahasiswa): Observable<Mahasiswa> {
    const nim = typeof mhs === 'number' ? mhs : mhs.Nim;
    const url = `${this.mhssUrl}/${nim}`;
    return this.http.delete<Mahasiswa>(url, httpOptions);
  }
}
