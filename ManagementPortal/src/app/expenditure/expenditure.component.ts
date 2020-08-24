import { Component, OnInit } from '@angular/core';
import { Expenditure } from '../Model/Expenditure';
import { HttpClient } from '@angular/common/http';
import { DataTablesResponse } from '../Model/DataTableResponse';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-expenditure',
  templateUrl: './expenditure.component.html',
  styleUrls: ['./expenditure.component.css']
})
export class ExpenditureComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  expenditure: Expenditure[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    const that = this;

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 2,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        that.http
          .post<DataTablesResponse>(environment.apiBaseUrl + "/expenditure",
            dataTablesParameters, {}
          ).subscribe(resp => {
            that.expenditure = resp.data;

            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: []
            });
          });
      },
      columns: [{ data: 'Id' }, { data: 'Name' }, { data: 'BillDate' }, { data: 'Type' }, { data: 'Amount' }]
    };
  }
}