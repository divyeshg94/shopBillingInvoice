import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../customer/customer.service';
import { Customer } from '../Model/Customer';
import { EmployeeService } from '../employee/employee.service';
import { Employee } from '../Model/Employee';
import { Invoice } from '../Model/Invoice';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {
  servicesList :any = [{"id":1,"name":"eye"},{"id":2,"name":"wax"}];
  service : any = {};
  customers: Customer[];
  employees: Employee[];
  customer: {};
  invoiceModel: Invoice;

  constructor(private customerService: CustomerService,
              private employeeService: EmployeeService) { }

  ngOnInit() {
    this.getCustomers();
    this.getAllEmployees();
  }


  getCustomers(){
    let customers = this.customerService.getAllCustomers().then(customers => {
      this.customers = customers;
    });
  }

  getAllEmployees() {
    let employees = this.employeeService.getAllEmployees().then(employees => {
      this.employees = employees;
    });
  }
}
