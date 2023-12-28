import { Injectable } from '@angular/core';

import { MatSnackBar } from '@angular/material/snack-bar';
import { Session } from '../Interfaces/session';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  constructor(private _snackBar: MatSnackBar) { }

  /**
   * Action to ahow alert messages
   * @param message 
   * @param type 
   */
  showAlert(message: string, type: string) {
    this._snackBar.open(message, type, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 3000
    });
  }

  /**
   * Method to save user in LocalStorage
   * @param userSession 
   */
  saveUserSession(userSession: Session) {
    localStorage.setItem('user', JSON.stringify(userSession));
  }

  /**
   * Action to get the user info from LocalStorage
   * @returns 
   */
  getUserSession() {
    const dataStr = localStorage.getItem('user');
    const user = JSON.parse(dataStr!);
    return user;
  }

  deleteUserSession() {
    localStorage.removeItem('user');
  }
}
