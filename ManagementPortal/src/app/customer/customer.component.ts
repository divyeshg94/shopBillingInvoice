import { Component, OnInit } from '@angular/core';
import { CustomerService } from './customer.service';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { Customer } from '../Model/Customer';
import { AddCustomerDialog } from './AddCustomerDialog';

export interface DialogData {
  phoneNumber: string;
  name: string;
}

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})

export class CustomerComponent implements OnInit {
  totalColumns: string[] = ['Id', 'Name', 'PhoneNumber', 'RegisteredOn', 'actions'];
  displayedColumns = ['Id', 'Name', 'PhoneNumber', 'RegisteredOn', 'actions'];
  dataSource = new MatTableDataSource<Customer>();
  phoneNumber: string;
  name: string;

  constructor(private customerService: CustomerService,
              public dialog: MatDialog) { }

  ngOnInit() {
    this.getAllCustomers();
  }

  
  getAllCustomers() {
    let customers = this.customerService.getAllCustomers().then(customers => {
      this.dataSource = new MatTableDataSource(customers);
    });
  }

  openAddDialog(): void {
    var currentDate = new Date();
    const dialogRef = this.dialog.open(AddCustomerDialog, {
      width: '400px',
      data: {Name: this.name, PhoneNumber: this.phoneNumber, RegisteredOn: currentDate}
    });

    dialogRef.afterClosed().subscribe(result => {
      result.IsExists = true;
      this.customerService.addCustomer(result).then(customer => {
        this.getAllCustomers();
      });
    });
  }

  openEditDialog(employee: Customer): void{
    employee.isEditMode = true;
    const dialogRef = this.dialog.open(AddCustomerDialog, {
      width: '400px',
      data: employee
    });

    dialogRef.afterClosed().subscribe(result => {
      this.customerService.updateCustomer(result).then(customer => {
        this.getAllCustomers();
      });
    });
  }
}
