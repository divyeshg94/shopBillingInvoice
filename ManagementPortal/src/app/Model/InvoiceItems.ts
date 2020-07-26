import { Invoice } from "./Invoice";
import { Item } from "./Item";

export class InvoiceItems{
    Id: number;
    SerialNumber: number;
    InvoiceId: number;
    Invoice: Invoice;
    ItemId: number;
    Item: Item;
    UnitPrice: number;
    TotalPrice: number;
    Quantity: number;
    ServicedBy: number;
    DiscountPercent : number;
    DiscountAmount : number;
}