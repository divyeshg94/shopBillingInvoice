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
import { InvoiceItems } from '../Model/InvoiceItems';
import { forEach } from '@angular/router/src/utils/collection';
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
  items: InvoiceItems[] = [];
  item: InvoiceItems;
  isEditMode: boolean = false;
  
  constructor(private customerService: CustomerService,
              private employeeService: EmployeeService,
              private itemService: ItemService,
              private invoiceService: InvoiceService ) { }

  ngOnInit() {
    this.invoiceModel.Customer = new Customer;
    this.invoiceModel.Employee = new Employee;
    this.invoiceModel.SaleDate = new Date;
    this.item = this.getNewItem();
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
    this.employeeService.getAllEmployees().then(employees => {
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
    this.getAttrValue('Name',this.item.Id);
    this.item.ItemId = this.item.Id;
    this.item['SerialNumber']=this.items.length+1;
    this.getTotalPrice(this.item);
    this.items.push(this.item);
    this.item = this.getNewItem();
    this.isEditMode = false;
  }

  getNewItem(){
    return {'SerialNumber':0,'Id': 0,'Quantity':1,'UnitPrice':0,'ServicedBy':0,DiscountPercent:0, DiscountAmount: 0, TotalPrice: 0, InvoiceId: 0, Invoice: new Invoice, Item: new Item, ItemId:0};
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
        if(attr == 'Price'){
          this.item['UnitPrice'] = service[attr];
        }
      }
    })
  }

  getTotalPrice(item){
    item.TotalPrice = item.UnitPrice * item.Quantity;
    if(item.DiscountPercent && item.DiscountPercent != 0)
    {
        item.TotalPrice = item.TotalPrice - ((item.TotalPrice)*(item.DiscountPercent/100));
    }
    this.getInvoiceTotalPrice();
  }

  getInvoiceTotalPrice(){
    var invoiceTotal = 0;
    this.items.forEach(function (value) {
      invoiceTotal+=value.TotalPrice;
    });
    this.invoiceModel.TotalAmount = invoiceTotal;
  }

  closeModel(item){
    this.getTotalPrice(item);
    this.isEditMode = false;
    this.item = this.getNewItem();
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
    this.invoiceModel.InvoiceItemses = this.items;
    this.getInvoiceTotalPrice();
    this.invoiceModel.DiscountAmount = 0;
    this.invoiceModel.DiscountPercent = 0;
    this.invoiceModel.TaxPercentage = 0;
    this.invoiceService.addInvoice(this.invoiceModel).then();
  }

  clearInvoice(){

  }
}
