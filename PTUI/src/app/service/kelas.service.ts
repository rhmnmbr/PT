import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { Kelas } from '../model/kelas';
import { MhsKls } from '../model/mhskls';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class KelasService {
  private klassesUrl = 'http://localhost:5000/PT/kelas';
  exec: boolean = false;

  constructor(private http: HttpClient) { }

  getKlasses(): Observable<Kelas[]> {
    return this.http.get<Kelas[]>(this.klassesUrl);
  }

  getKlsDet(kode: string): Observable<Kelas> {
    const url = `${this.klassesUrl}/${kode}/detail`;
    return this.http.get<Kelas>(url);
  }

  enrollMhs(kode: string, mhskls: MhsKls): Observable<MhsKls> {
    const url = `${this.klassesUrl}/${kode}/detail`;
    return this.http.post<MhsKls>(url, mhskls, httpOptions);
  }

  unEnrollMhs(kode: string, nim: number): Observable<MhsKls> {
    const url = `${this.klassesUrl}/${kode}/detail/${nim}`;
    return this.http.delete<MhsKls>(url, httpOptions);
  }

  updateMhs(kode: string, nim: number, mhskls: MhsKls): Observable<MhsKls> {
    const url = `${this.klassesUrl}/${kode}/detail/${nim}`;
    return this.http.put<MhsKls>(url, mhskls, httpOptions);
  }

  execute(exec: boolean) {
    this.exec = exec;
  }

  executeMe() {
    return this.exec;
  }
}
