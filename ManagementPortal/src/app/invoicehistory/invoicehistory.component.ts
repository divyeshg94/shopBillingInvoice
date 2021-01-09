import { Component, OnInit } from '@angular/core';
import { Invoice } from '../Model/Invoice';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { DataTablesResponse } from '../Model/DataTableResponse';

@Component({
  selector: 'app-invoicehistory',
  templateUrl: './invoicehistory.component.html',
  styleUrls: ['./invoicehistory.component.css']
})
export class InvoicehistoryComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  invoices: Invoice[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    const that = this;

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        that.http
          .post<DataTablesResponse>(environment.apiBaseUrl + "/invoice",
            dataTablesParameters
          ).subscribe(resp => {
            that.invoices = resp.data;

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