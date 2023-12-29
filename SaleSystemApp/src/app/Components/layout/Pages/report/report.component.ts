import { Component, AfterViewInit, ViewChild } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';
import * as XLSX from 'xlsx';

import { Report } from 'src/app/Interfaces/report';
import { SaleService } from 'src/app/Services/sale.service';
import { CommonService } from 'src/app/Utilities/common.service';

export const MY_DATA_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY'
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY'
  }
}
@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMATS }
  ]
})
export class ReportComponent implements AfterViewInit {
  filterForm: FormGroup;
  saleReportList: Report[] = [];
  tableColumns: string[] = ['recordDate', 'saleNumber', 'paymentType', 'total', 'product', 'amount', 'price', 'productTotal'];
  saleReportData = new MatTableDataSource(this.saleReportList);
  @ViewChild(MatPaginator) paginationTable!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private _saleService: SaleService,
    private _commonService: CommonService
  ) {
    this.filterForm = this.fb.group({
      beginDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });
  }

  ngAfterViewInit(): void {
    this.saleReportData.paginator = this.paginationTable;
  }

  /**
   * Method to search sales by Dates
   * @returns 
   */
  searchSales() {
    const _beginDate = moment(this.filterForm.value.beginDate).format('DD/MM/YYYY');
    const _endDate = moment(this.filterForm.value.endDate).format('DD/MM/YYYY');

    if (_beginDate === 'Invalid date' || _endDate === 'Invalid date') {
      this._commonService.showAlert('Debe ingresar ambas fechas', 'Oops!');
      return;
    }

    this._saleService.report(_beginDate, _endDate).subscribe({
      next: (data) => {
        if (data.status) {
          this.saleReportList = data.value;
          this.saleReportData.data = data.value;
        } else {
          this.saleReportList = [];
          this.saleReportData.data = [];
          this._commonService.showAlert('No se encontraron datos', 'Oops!');
        }
      },
      error: (error) => {
        this._commonService.showAlert('Ocurrió un error', 'Error');
      }
    });
  }

  /**
   * Method to automatically download an excel report with the searched data
   */
  exportExcel() {
    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.json_to_sheet(this.saleReportList);

    XLSX.utils.book_append_sheet(wb, ws, 'Reporte');
    XLSX.writeFile(wb, 'ReporteVentas.xlsx');
  }
}