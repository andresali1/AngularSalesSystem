import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';
import { Sale } from '../Interfaces/sale';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private urlApi: string = `${environment.path}Sale/`;

  constructor(private http: HttpClient) { }

  /**
   * Action to register a Sale
   * @param request 
   * @returns 
   */
  register(request: Sale): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}Register`, request);
  }

  /**
   * Action to get the sale History
   * @param searchBy 
   * @param saleNumber 
   * @param beginDate 
   * @param endDate 
   * @returns 
   */
  history(searchBy: string, saleNumber: string, beginDate: string, endDate: string): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}History?searchBy=${searchBy}&saleNumber=${saleNumber}&beginDate=${beginDate}&endDate=${endDate}`);
  }

  /**
   * Action to get the sale report
   * @param beginDate 
   * @param endDate 
   * @returns 
   */
  report(beginDate: string, endDate: string): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}Report?beginDate=${beginDate}&endDate=${endDate}`);
  }
}
