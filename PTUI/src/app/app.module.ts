import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material.module';
import { NgProgressModule, NgProgressInterceptor } from 'ngx-progressbar';
import { OrderModule } from 'ngx-order-pipe';

import { AppComponent } from './app.component';
import { MahasiswaComponent } from './component/mahasiswa/mahasiswa.component';
import { MahasiswaDetailComponent } from './component/mahasiswa/mahasiswa-detail/mahasiswa-detail.component';
import { NavigationComponent } from './component/navigation/navigation.component';
import { LoginComponent } from './component/login/login.component';
import { KelasComponent } from './component/kelas/kelas.component';
import { KelasDetailComponent } from './component/kelas/kelas-detail/kelas-detail.component';
import { LookupMahasiswaComponent } from './component/kelas/kelas-detail/lookup-mahasiswa/lookup-mahasiswa.component';
import { UpdateNilaiComponent } from './component/kelas/kelas-detail/update-nilai/update-nilai.component';

import { MahasiswaService } from './service/mahasiswa.service';
import { MessagesService } from './service/messages.service';
import { LoginService } from './service/login.service';
import { KelasService } from './service/kelas.service';


@NgModule({
  declarations: [
    AppComponent,
    MahasiswaComponent,
    NavigationComponent,
    MahasiswaDetailComponent,
    LoginComponent,
    KelasComponent,
    KelasDetailComponent,
    LookupMahasiswaComponent,
    UpdateNilaiComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    LayoutModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgProgressModule,
    OrderModule,
    ScrollToModule.forRoot()
  ],
  providers: [
    MahasiswaService,
    MessagesService,
    LoginService,
    KelasService,
    { provide: HTTP_INTERCEPTORS, useClass: NgProgressInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
  entryComponents: [LoginComponent, LookupMahasiswaComponent, UpdateNilaiComponent]
})
export class AppModule { }
