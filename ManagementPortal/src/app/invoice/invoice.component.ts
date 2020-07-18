import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../customer/customer.service';
import { Customer } from '../Model/Customer';
import { EmployeeService } from '../employee/employee.service';
import { Employee } from '../Model/Employee';
import { Invoice } from '../Model/Invoice';
import { ItemService } from '../item/item.service';
import { Item } from '../Model/Item';
import { InvoiceService } from './invoice.service';
@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {
  servicesList: Item[];
  customers: Customer[];
  employees: Employee[];
  invoiceModel: Invoice = new Invoice;
  items= [];
  item = {'SerialNumber':0,'id': '','quantity':'','Price':0,'servicedBy':'','discountPercentage':0};
  isEditMode: boolean = false;
  
  constructor(private customerService: CustomerService,
              private employeeService: EmployeeService,
              private itemService: ItemService,
              private invoiceService: InvoiceService ) { }

  ngOnInit() {
    this.invoiceModel.Customer = new Customer;
    this.invoiceModel.Employee = new Employee;
    this.invoiceModel.SaleDate = new Date;
    this.getCustomers();
    this.getAllEmployees();
    this.getAllItems();
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

  getAllItems() {
    let employees = this.itemService.getAllItems().then(items => {
      this.servicesList = items;
      console.log(this.servicesList);
    });
  }

  addItemToInvoice() {
    console.log("addItemToInvoice");
    this.getAttrValue('Name',this.item.id);
    this.item['SerialNumber']=this.items.length+1;
    this.items.push(this.item);
    this.item = {'SerialNumber':0,'id': '','quantity':'','Price':0,'servicedBy':'','discountPercentage':0};

  }

  searchByPhoneNumber(phoneNumber){
    let customers = this.customerService.getCustomer(null,phoneNumber).then(customers => {
      this.invoiceModel.Customer = customers;
      this.invoiceModel.CustomerId = customers.Id;
    });
  }

  searchByName(name){
    let customers = this.customerService.getCustomer(name,null).then(customers => {
      this.invoiceModel.Customer = customers;
      this.invoiceModel.CustomerId = customers.Id;
    });
  }

  getAttrValue(attr,value){
    this.servicesList.filter(service => {
      if(service.Id == value){
        this.item[attr] = service[attr];
      }
    })
  }

  editItem(item){
    this.isEditMode = true;
    document.getElementById("openModalButton").click();
    this.item = item;
  }

  deleteItem(item){
    let index = this.items.findIndex(d => d.SerialNumber === item.SerialNumber); 
    this.items.splice(index, 1);
  }

  generateInvoice(){
    this.invoiceService.addInvoice(this.invoiceModel).then();
  }

  clearInvoice(){

  }
}
