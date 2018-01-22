import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MahasiswaComponent } from './component/mahasiswa/mahasiswa.component';
import { MahasiswaDetailComponent } from './component/mahasiswa/mahasiswa-detail/mahasiswa-detail.component';
import { KelasComponent } from './component/kelas/kelas.component';
import { KelasDetailComponent } from './component/kelas/kelas-detail/kelas-detail.component';

const routes: Routes = [
  { path: 'mahasiswa', component: MahasiswaComponent,
    children: [{ path: ':nim', component: MahasiswaDetailComponent }]
  },
  { path: 'kelas', component: KelasComponent,
    children: [{ path: ':kode', component: KelasDetailComponent }]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }