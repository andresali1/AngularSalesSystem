import { Component, AfterViewInit, ViewChild, OnInit } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { AppUserModalComponent } from '../../Modals/app-user-modal/app-user-modal.component';
import { AppUser } from 'src/app/Interfaces/app-user';
import { AppUserService } from 'src/app/Services/app-user.service';
import { CommonService } from 'src/app/Utilities/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-app-user',
  templateUrl: './app-user.component.html',
  styleUrls: ['./app-user.component.css']
})
export class AppUserComponent implements AfterViewInit, OnInit {
  columnsTable: string[] = ['completeName', 'email', 'roleDescription', 'status', 'actions'];
  beginData: AppUser[] = [];
  dataListAppUser = new MatTableDataSource(this.beginData);
  @ViewChild(MatPaginator) tablePagination!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _appUserService: AppUserService,
    private _commonService: CommonService
  ) { }

  getUsers() {
    this._appUserService.list().subscribe({
      next: (data) => {
        if (data.status) {
          this.dataListAppUser.data = data.value;
        } else {
          this._commonService.showAlert('No se encontraron datos', 'Oops!');
        }
      },
      error: (error) => {
        this._commonService.showAlert('Ocurrió un error', 'Error!');
      }
    });
  }

  ngOnInit(): void {
    this.getUsers();
  }

  ngAfterViewInit(): void {
    this.dataListAppUser.paginator = this.tablePagination;
  }

  /**
   * Method to apply filters to a table
   * @param event 
   */
  applyFiltersTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListAppUser.filter = filterValue.trim().toLocaleLowerCase();
  }

  /**
   * Method to Open the AppUser Modal to create an User
   */
  newUser() {
    this.dialog.open(AppUserModalComponent, {
      disableClose: true
    }).afterClosed().subscribe(result => {
      if (result == 'true') this.getUsers();
    });
  }

  /**
   * Method to open the AppUser modal to edit an user
   * @param user 
   */
  editUser(user: AppUser) {
    this.dialog.open(AppUserModalComponent, {
      disableClose: true,
      data: user
    }).afterClosed().subscribe(result => {
      if (result == 'true') this.getUsers();
    });
  }

  /**
   * Method to delete an user
   * @param user 
   */
  deleteUser(user: AppUser) {
    Swal.fire({
      title: '¿Desea eliminar el usuario?',
      text: user.completeName,
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Si, eliminar',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No, volver'
    }).then((result) => {
      if (result.isConfirmed) {
        this._appUserService.delete(user.userId).subscribe({
          next: (data) => {
            if (data.status) {
              this._commonService.showAlert('El usuario fue eliminado', 'Exito');
              this.getUsers();
            } else {
              this._commonService.showAlert('No se pudo eliminar el Usuario', 'Error');
            }
          },
          error: (error) => {
            this._commonService.showAlert('Ocurrió un error', 'Error');
          }
        });
      }
    })
  }
}
