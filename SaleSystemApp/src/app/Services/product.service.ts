import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';
import { Product } from '../Interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private urlApi: string = `${environment.path}Product/`;

  constructor(private http: HttpClient) { }

  /**
   * Action to get the Product list
   * @returns 
   */
  list(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}List`);
  }

  /**
   * Action to save a product
   * @param request 
   * @returns 
   */
  save(request: Product): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}Save`, request);
  }

  /**
   * Action to Update a product
   * @param request 
   * @returns 
   */
  edit(request: Product): Observable<ResponseApi> {
    return this.http.put<ResponseApi>(`${this.urlApi}Edit`, request);
  }

  /**
   * Action to Delete a Product
   * @param id 
   * @returns 
   */
  delete(id: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.urlApi}Delete/${id}`);
  }
}
