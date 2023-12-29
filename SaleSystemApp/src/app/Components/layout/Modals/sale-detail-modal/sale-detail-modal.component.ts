import { Component, Inject, OnInit } from '@angular/core';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Sale } from 'src/app/Interfaces/sale';
import { SaleDetail } from 'src/app/Interfaces/sale-detail';

@Component({
  selector: 'app-sale-detail-modal',
  templateUrl: './sale-detail-modal.component.html',
  styleUrls: ['./sale-detail-modal.component.css']
})
export class SaleDetailModalComponent {
  recordDate: string = '';
  documentNumber: string = '';
  paymentType: string = '';
  total: string = '';
  saleDetail: SaleDetail[] = [];
  tableColumns: string[] = ['product', 'amount', 'price', 'total'];

  constructor(
    @Inject(MAT_DIALOG_DATA) public _saleData: Sale,
  ) {
    this.recordDate = _saleData.recordDate!;
    this.documentNumber = _saleData.documentNumber!;
    this.paymentType = _saleData.paymentType;
    this.total = _saleData.totalText;
    this.saleDetail = _saleData.saleDetails;
  }
}
