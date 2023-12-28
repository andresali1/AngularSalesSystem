import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';

@Injectable({
  providedIn: 'root'
})
export class DashBoardService {
  private urlApi: string = `${environment.path}DashBoard/`;

  constructor(private http: HttpClient) { }

  /**
   * Action to get the DashBoard Summary
   * @returns 
   */
  summary(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}Summary`);
  }
}
