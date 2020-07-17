import { Invoice } from "./Invoice";
import { Item } from "./Item";

export class InvoiceItems{
    Id: number;
    InvoiceId: number;
    Invoice: Invoice;
    ItemId: number;
    Item: Item;
    UnitPrice: number;
    TotalPrice: number;
    Quantity: number;
}