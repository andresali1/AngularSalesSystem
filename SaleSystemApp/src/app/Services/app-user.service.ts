import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';
import { Login } from '../Interfaces/login';
import { AppUser } from '../Interfaces/app-user';

@Injectable({
  providedIn: 'root'
})
export class AppUserService {
  private urlApi: string = `${environment.path}AppUser/`;

  constructor(private http: HttpClient) { }

  /**
   * Action to login an User
   * @param request 
   * @returns 
   */
  login(request: Login): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}Login`, request);
  }

  /**
   * Action to get the users List
   * @returns 
   */
  list(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}List`);
  }

  /**
   * Action to save an user
   * @param request 
   * @returns 
   */
  save(request: AppUser): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}Save`, request);
  }

  /**
   * Action to update an user
   * @param request 
   * @returns 
   */
  edit(request: AppUser): Observable<ResponseApi> {
    return this.http.put<ResponseApi>(`${this.urlApi}Edit`, request);
  }

  /**
   * Action to delete an user
   * @param id 
   * @returns 
   */
  delete(id: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.urlApi}Delete/${id}`);
  }
}
