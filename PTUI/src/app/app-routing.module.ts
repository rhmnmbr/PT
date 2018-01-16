import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MahasiswaComponent } from './mahasiswa/mahasiswa.component';
import { MahasiswaDetailComponent } from './mahasiswa/mahasiswa-detail/mahasiswa-detail.component';

const routes: Routes = [
  { path: 'mahasiswa', component: MahasiswaComponent },
  { path: 'mahasiswa/:nim', component: MahasiswaDetailComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}