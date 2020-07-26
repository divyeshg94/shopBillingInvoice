import { Component, OnInit } from '@angular/core';
import { ItemService } from './item.service';
import { Item } from '../Model/Item';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  items: Item[] = [];
  item: Item;

  constructor(private itemsService: ItemService) { }

  ngOnInit() {
    this.getAllItems();
    this.item = this.getNewItem();
  }

  getAllItems(){
    this.itemsService.getAllItems().then(allItems => {
      this.items = allItems;
    });
  }

  getNewItem(){
    return {
      SerialNumber: 0,
      Id: 0, 
      Name: '',
      Category: '',
      Price: 0,
      IsAvailable: true
    }
  }

  editItem(item){

  }

  deleteItem(item){
    //this.itemsService.
  }
}
