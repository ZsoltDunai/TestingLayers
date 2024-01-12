import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Item } from '../item.model';
import { ItemService } from '../item.service';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent {
  @Input() items: Item[] = [];
  @Output() selectItemForUpdate = new EventEmitter<number>();

  onItemUpdateClick(itemId: number): void {
    this.selectItemForUpdate.emit(itemId);
  }
}
