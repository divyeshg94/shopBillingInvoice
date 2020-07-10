import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {
  servicesList :any = [{"id":1,"name":"eye"},{"id":2,"name":"wax"}];
  service : any = {};
  constructor() { }

  ngOnInit() {
  }

}
