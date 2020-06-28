import { BrowserModule } from '@angular/platform-browser';
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

@NgModule({
  declarations: [
    AppComponent,
    InvoiceComponent,
    EmployeeComponent,
    CustomerComponent,
    AddEmployeeDialog
  ],
  imports: [
    RouterModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    AngMaterialModule,
    HttpClientModule,
    FormsModule
  ],
  entryComponents:[AddEmployeeDialog],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
