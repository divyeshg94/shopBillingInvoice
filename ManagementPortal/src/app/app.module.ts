import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { InvoiceComponent } from './invoice/invoice.component';
import { EmployeeComponent } from './employee/employee.component';
import { AddEmployeeDialog } from "./employee/AddEmployeeDialog";
import { CustomerComponent } from './customer/customer.component';
import { AngMaterialModule } from './mat.module';
import { HttpClientModule } from '@angular/common/http'; 
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddCustomerDialog } from './customer/AddCustomerDialog';
import { ItemComponent } from './item/item.component';
import { EmployeeattendanceComponent } from './employeeattendance/employeeattendance.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    InvoiceComponent,
    EmployeeComponent,
    CustomerComponent,
    AddEmployeeDialog,
    AddCustomerDialog,
    ItemComponent,
    EmployeeattendanceComponent,
    DashboardComponent
  ],
  imports: [
    RouterModule,
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    AngMaterialModule,
    HttpClientModule,
    FormsModule
  ],
  entryComponents:[AddEmployeeDialog, AddCustomerDialog],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
