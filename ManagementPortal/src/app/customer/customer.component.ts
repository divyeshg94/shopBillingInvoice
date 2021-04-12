import { Component, OnInit } from '@angular/core';
import { CustomerService } from './customer.service';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { Customer } from '../Model/Customer';
import { AddCustomerDialog } from './AddCustomerDialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
  totalColumns: string[] = ['Id', 'Name', 'PhoneNumber', 'EmailId', 'RegisteredOn', 'actions'];
  displayedColumns = ['Id', 'Name', 'PhoneNumber', 'EmailId', 'RegisteredOn', 'actions'];
  dataSource = new MatTableDataSource<Customer>();
  phoneNumber: string;
  name: string;  
  registerForm: FormGroup;
  submitted = false;

  constructor(private customerService: CustomerService,
    private formBuilder: FormBuilder,
    public dialog: MatDialog) { }

  ngOnInit() {
    this.getAllCustomers();

    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required, Validators.minLength(3)],
      phoneNumber: ['', Validators.required, Validators.minLength(6)],
      email: ['', [Validators.required, Validators.email,Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
  });
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

  openEditDialog(customer: Customer): void{
    customer.isEditMode = true;
    const dialogRef = this.dialog.open(AddCustomerDialog, {
      width: '400px',
      data: customer
    });

    dialogRef.afterClosed().subscribe(result => {
      this.customerService.updateCustomer(result).then(customer => {
        this.getAllCustomers();
      });
    });
  }
}
