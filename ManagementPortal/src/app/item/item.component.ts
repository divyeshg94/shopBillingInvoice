import { Component, OnInit } from '@angular/core';
import { ItemService } from './item.service';
import { Item } from '../Model/Item';
import { CatalogItem } from '../Model/CatalogItem';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  items: Item[] = [];
  item: Item;
  catalogItems: CatalogItem[] = [];

  constructor(private itemsService: ItemService) { }

  ngOnInit() {
    this.getAllItems();
    this.item = this.getNewItem();
  }

  getAllItems() {
    this.itemsService.getAllItems().then(allItems => {
      this.items = allItems;
      this.splitCategory();
    });
  }

  splitCategory() {
    this.catalogItems = [];
    var that = this;
    this.items.forEach(function (item) {
      var existingCatalog = that.catalogItems.find(i => i.CategoryName == item.Category);
      if (!existingCatalog) {
        var catalogItem = {
          CategoryName: item.Category, ImgSrc: "../../assets/images/"+item.Category+".jpeg", Items: []
        };
        catalogItem.Items.push(item);
        that.catalogItems.push(catalogItem);
      }else{
        existingCatalog.Items.push(item);
      }
    });
  }

  getNewItem() {
    return {
      SerialNumber: 0,
      Id: 0,
      Name: '',
      Category: '',
      Price: 0,
      IsAvailable: true
    }
  }

  editItem(item) {

  }

  deleteItem(item) {
    //this.itemsService.
  }
}
