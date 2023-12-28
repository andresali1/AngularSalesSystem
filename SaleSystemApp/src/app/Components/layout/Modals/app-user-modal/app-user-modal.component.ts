import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserRole } from 'src/app/Interfaces/user-role';
import { AppUser } from 'src/app/Interfaces/app-user';

import { UserRoleService } from 'src/app/Services/user-role.service';
import { AppUserService } from 'src/app/Services/app-user.service';
import { CommonService } from 'src/app/Utilities/common.service';

@Component({
  selector: 'app-app-user-modal',
  templateUrl: './app-user-modal.component.html',
  styleUrls: ['./app-user-modal.component.css']
})
export class AppUserModalComponent implements OnInit {
  appUserForm: FormGroup;
  hidePassword: boolean = true;
  actionTitle: string = 'Agregar';
  actionbuttom: string = 'Guardar';
  rolesList: UserRole[] = [];

  constructor(
    private actualModal: MatDialogRef<AppUserModalComponent>,
    @Inject(MAT_DIALOG_DATA) public userData: AppUser,
    private fb: FormBuilder,
    private _userRoleService: UserRoleService,
    private _appUserService: AppUserService,
    private _commonService: CommonService
  ) {
    this.appUserForm = this.fb.group({
      completeName: ['', Validators.required],
      email: ['', Validators.required],
      roleId: ['', Validators.required],
      pass: ['', Validators.required],
      isActive: ['1', Validators.required]
    });

    if (this.userData != null) {
      this.actionTitle = 'Editar';
      this.actionbuttom = 'Actualizar';
    }

    this._userRoleService.list().subscribe({
      next: (data) => {
        if (data.status) this.rolesList = data.value
      },
      error: (error) => {
        this._commonService.showAlert('Ocurrió un error', 'Error!');
      }
    });
  }

  ngOnInit(): void {
    if (this.userData != null) {
      this.appUserForm.patchValue({
        completeName: this.userData.completeName,
        email: this.userData.email,
        roleId: this.userData.roleId,
        pass: this.userData.pass,
        isActive: this.userData.isActive
      });
    }
  }

  /**
   * Method to Create or Update an User
   */
  saveUser() {
    const _user: AppUser = {
      userId: this.userData == null ? 0 : this.userData.userId,
      completeName: this.appUserForm.value.completeName,
      email: this.appUserForm.value.email,
      roleId: this.appUserForm.value.roleId,
      roleDescription: '',
      pass: this.appUserForm.value.pass,
      isActive: parseInt(this.appUserForm.value.isActive)
    }

    if (_user.userId == 0) {
      this._appUserService.save(_user).subscribe({
        next: (data) => {
          if (data.status) {
            this._commonService.showAlert('El usuario fue registrado', 'Exito');
            this.actualModal.close('true');
          } else {
            this._commonService.showAlert('No se pudo registrar el usuario', 'Error!');
          }
        }, error: (error) => {
          this._commonService.showAlert('Ocurrió un error', 'Error!');
        }
      });
    } else {
      this._appUserService.edit(_user).subscribe({
        next: (data) => {
          if (data.status) {
            this._commonService.showAlert('El usuario fue actualizado', 'Exito');
            this.actualModal.close('true');
          } else {
            this._commonService.showAlert('No se pudo actualizar el usuario', 'Error!');
          }
        }, error: (error) => {
          this._commonService.showAlert('Ocurrió un error', 'Error!');
        }
      });
    }
  }
}
