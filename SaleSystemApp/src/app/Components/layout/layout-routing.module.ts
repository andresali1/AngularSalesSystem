import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { DashBoardComponent } from './Pages/dash-board/dash-board.component';
import { AppUserComponent } from './Pages/app-user/app-user.component';
import { ProductComponent } from './Pages/product/product.component';
import { SaleComponent } from './Pages/sale/sale.component';
import { SaleHistoryComponent } from './Pages/sale-history/sale-history.component';
import { ReportComponent } from './Pages/report/report.component';

const routes: Routes = [{
  path: '',
  component: LayoutComponent,
  children: [
    {
      path: 'dashboard',
      component: DashBoardComponent
    },
    {
      path: 'usuario',
      component: AppUserComponent
    },
    {
      path: 'producto',
      component: ProductComponent
    },
    {
      path: 'venta',
      component: SaleComponent
    },
    {
      path: 'historial_venta',
      component: SaleHistoryComponent
    },
    {
      path: 'reporte',
      component: ReportComponent
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
