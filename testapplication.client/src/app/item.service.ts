import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Item } from './item.model';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  private apiUrl = 'http://localhost:4200/api/items';

  constructor(private http: HttpClient) { }

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.apiUrl);
  }

  getItemById(id: number): Observable<Item> {
    return this.http.get<Item>(`${this.apiUrl}/${id}`);
  }

  createItem(newItem: Item): Observable<Item> {
    return this.http.post<Item>(this.apiUrl, newItem);
  }

  updateItem(id: number, updatedItem: Item): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, updatedItem);
  }
}
