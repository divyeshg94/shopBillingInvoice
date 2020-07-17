import { InvoiceItems } from "./InvoiceItems";

export class Invoice{
    Id: number;
    CustomerId: number;
    EmployeeId: number;
    TotalAmount: string;
    SaleDate: Date;
    DiscountPercentage: number;
    DiscountAmount: number;
    TaxPercentage: number;
    Notes: string;
    InvoiceItemses: InvoiceItems[];
}