import { Component, OnInit } from '@angular/core';
import { ItemService } from './item.service';
import { Item } from '../Model/Item';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  items: Item[];

  constructor(private itemsService: ItemService) { }

  ngOnInit() {
    this.items = new Item[1]; 
  }

  getAllItems(){
    this.itemsService.getAllItems().then(allItems => {
      this.items = allItems;
    });
  }

  editItem(item){

  }

  deleteItem(item){
    //this.itemsService.
  }
}
