import { Component } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/Interfaces/login';
import { AppUserService } from 'src/app/Services/app-user.service';
import { CommonService } from 'src/app/Utilities/common.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  formLogin: FormGroup;
  hidePassword: boolean = true;
  showLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private _appUserSevice: AppUserService,
    private _commonService: CommonService
  ) {
    this.formLogin = this.fb.group({
      email: ['', Validators.required],
      pass: ['', Validators.required]
    });
  }

  login() {
    this.showLoading = true;

    const request: Login = {
      email: this.formLogin.value.email,
      pass: this.formLogin.value.pass
    }
    this._appUserSevice.login(request).subscribe({
      next: (data) => {
        if (data.status) {
          this._commonService.saveUserSession(data.value);
          this.router.navigate(['pages']);
        } else {
          this._commonService.showAlert('Las credenciales son incorrectas', 'Opps!');
        }
      },
      complete: () => {
        this.showLoading = false;
      },
      error: () =>{
        this._commonService.showAlert('Ocurrió un error', 'Opps!');
      }
    });
  }
}
