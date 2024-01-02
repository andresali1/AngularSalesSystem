import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { Menu } from 'src/app/Interfaces/menu';

import { MenuService } from 'src/app/Services/menu.service';
import { CommonService } from 'src/app/Utilities/common.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {
  menuList: Menu[] = [];
  userEmail: string = '';
  userRole: string = '';

  constructor(
    private router: Router,
    private _menuService: MenuService,
    private _commonService: CommonService
  ) { }

  ngOnInit(): void {
    const user = this._commonService.getUserSession();

    console.log(user);

    if (user != null) {
      this.userEmail = user.email;
      this.userRole = user.roleDescription;

      this._menuService.list(user.userId).subscribe({
        next: (data) => {
          if (data.status) this.menuList = data.value;
        },
        error: (error) => { }
      });
    }
  }

  /**
   * Method to logout an User
   */
  logout() {
    this._commonService.deleteUserSession();
    this.router.navigate(['login']);
  }
}
