import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';

@Injectable({
  providedIn: 'root'
})
export class UserRoleService {
  private urlApi: string = `${environment.path}UserRole/`;

  constructor(private http: HttpClient) { }

  /**
   * Action to get the User Roles list
   * @returns 
   */
  list(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}List`);
  }
}
