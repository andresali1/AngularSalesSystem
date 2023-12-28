import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private urlApi: string = `${environment.path}Menu/`;

  constructor(private http: HttpClient) { }

  /**
   * Action to get the User's Menu list
   * @returns 
   */
  list(appUserId: number): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}List?appUserId=${appUserId}`);
  }
}
