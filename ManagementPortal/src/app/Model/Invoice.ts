import { InvoiceItems } from "./InvoiceItems";
import { Customer } from "./Customer";
import { Employee } from "./Employee";

export class Invoice{
    Id: number;
    CustomerId: number;
    Customer: Customer;
    EmployeeId: number;
    Employee: Employee;
    TotalAmount: string;
    SaleDate: Date;
    DiscountPercentage: number;
    DiscountAmount: number;
    TaxPercentage: number;
    Notes: string;
    InvoiceItemses: InvoiceItems[];
}