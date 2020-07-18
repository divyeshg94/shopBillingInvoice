import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { InvoiceComponent } from './invoice/invoice.component';
import { EmployeeComponent } from './employee/employee.component';
import { CustomerComponent } from './customer/customer.component';
import { ItemComponent } from './item/item.component';

const appRoutes: Routes = [
  { path: 'invoice', component: InvoiceComponent }, 
  { path: 'employee', component: EmployeeComponent },  
  { path: 'customer', component: CustomerComponent },
  { path: 'item', component: ItemComponent}
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  declarations: []
})
export class AppRoutingModule { }
