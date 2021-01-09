import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { InvoiceComponent } from './invoice/invoice.component';
import { EmployeeComponent } from './employee/employee.component';
import { CustomerComponent } from './customer/customer.component';
import { ItemComponent } from './item/item.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ExpenditureComponent } from './expenditure/expenditure.component';
import { InvoicehistoryComponent } from './invoicehistory/invoicehistory.component';
import { InventoryComponent } from './inventory/inventory.component';

const appRoutes: Routes = [
  { path: 'dashboard', component: DashboardComponent }, 
  { path: 'invoice', component: InvoiceComponent },
  { path: 'employee', component: EmployeeComponent },  
  { path: 'customer', component: CustomerComponent },
  { path: 'item', component: ItemComponent},
  { path: 'inventory', component: InventoryComponent},
  { path: 'history', component: InvoicehistoryComponent},
  { path: 'expenditure', component: ExpenditureComponent}
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
