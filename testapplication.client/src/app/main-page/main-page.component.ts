import { Component, OnInit } from '@angular/core';
import { Item } from '../item.model';
import { ItemService } from '../item.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  items: Item[] = [];
  newItemName: string = '';
  selectedItemId: number | null = null;
  updateItemName: string = '';
  showForm: boolean = false;

  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
    this.getItems();
  }

  getItems(): void {
    this.itemService.getItems()
      .subscribe(
        (items: Item[]) => {
          this.items = items;
        },
        error => {
          console.error('Error fetching items:', error);
        }
      );
  }

  createNewItem(): void {
    const maxId = Math.max(...this.items.map(item => item.id), 0);

    const newItem: Item = {
      id: maxId + 1,
      name: this.newItemName,
    };

    this.itemService.createItem(newItem)
      .subscribe(
        createdItem => {
          this.items.push(createdItem);
          this.newItemName = '';
          this.showForm = false;
        },
        error => {
          console.error('Error creating item:', error);
        }
      );
  }

  selectItemForUpdate(itemId: number): void {
    this.selectedItemId = itemId;
    this.updateItemName = '';
  }

  updateItem(): void {
    if (this.selectedItemId !== null) {
      const updatedItem: Item = {
        id: this.selectedItemId,
        name: this.updateItemName,
      };

      this.itemService.updateItem(this.selectedItemId, updatedItem)
        .subscribe(
          () => {
            const index = this.items.findIndex(item => item.id === this.selectedItemId);
            if (index !== -1) {
              this.items[index].name = this.updateItemName;
            }

            this.selectedItemId = null;
            this.updateItemName = '';
          },
          error => {
            console.error('Error updating item:', error);
          }
        );
    }
  }
}
