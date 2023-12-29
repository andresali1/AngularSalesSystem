import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { DashBoardComponent } from './Pages/dash-board/dash-board.component';
import { AppUserComponent } from './Pages/app-user/app-user.component';
import { ProductComponent } from './Pages/product/product.component';
import { SaleComponent } from './Pages/sale/sale.component';
import { ReportComponent } from './Pages/report/report.component';
import { SaleHistoryComponent } from './Pages/sale-history/sale-history.component';
import { SharedModule } from 'src/app/Utilities/shared/shared.module';
import { AppUserModalComponent } from './Modals/app-user-modal/app-user-modal.component';
import { ProductModalComponent } from './Modals/product-modal/product-modal.component';
import { SaleDetailModalComponent } from './Modals/sale-detail-modal/sale-detail-modal.component';


@NgModule({
  declarations: [
    DashBoardComponent,
    AppUserComponent,
    ProductComponent,
    SaleComponent,
    ReportComponent,
    SaleHistoryComponent,
    AppUserModalComponent,
    ProductModalComponent,
    SaleDetailModalComponent
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    SharedModule
  ]
})
export class LayoutModule { }
