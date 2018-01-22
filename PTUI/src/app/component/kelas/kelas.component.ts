import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

import { Kelas } from '../../model/kelas';
import { KelasService } from '../../service/kelas.service';
import { MessagesService } from '../../service/messages.service';

@Component({
  selector: 'app-kelas',
  templateUrl: './kelas.component.html',
  styleUrls: ['./kelas.component.css']
})
export class KelasComponent implements OnInit, AfterViewInit {
  klasses: Kelas[];
  displayedColumns = ['KodeKelas', 'Nama'];
  dataSource = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private kelasService: KelasService,
    private messageService: MessagesService
  ) { }

  ngOnInit() {
    this.getKlasses();
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getKlasses(): void {
    this.kelasService.getKlasses()
      .subscribe(resp => {
        this.klasses = resp;
        this.dataSource.data = this.klasses;
      });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
}
