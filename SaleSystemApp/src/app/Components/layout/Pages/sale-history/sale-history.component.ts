import { Component, AfterViewInit, ViewChild } from '@angular/core';

import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';

import { SaleDetailModalComponent } from '../../Modals/sale-detail-modal/sale-detail-modal.component';
import { Sale } from 'src/app/Interfaces/sale';
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
  selector: 'app-sale-history',
  templateUrl: './sale-history.component.html',
  styleUrls: ['./sale-history.component.css'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMATS }
  ]
})
export class SaleHistoryComponent implements AfterViewInit {
  searchForm: FormGroup;
  searchOptions: any[] = [
    { value: 'date', description: 'Por Fechas' },
    { value: 'number', description: 'Numero de Venta' }
  ];
  tableColumns: string[] = ['recordDate', 'documentNumber', 'paymentType', 'total', 'action'];
  beginData: Sale[] = [];
  saleListData = new MatTableDataSource(this.beginData);
  @ViewChild(MatPaginator) paginationTable!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private _saleService: SaleService,
    private _commonService: CommonService
  ) {
    this.searchForm = this.fb.group({
      searchBy: ['date'],
      number: [''],
      beginDate: [''],
      endDate: ['']
    });

    this.searchForm.get('searchBy')?.valueChanges.subscribe(
      value => {
        this.searchForm.patchValue({
          number: '',
          beginDate: '',
          endDate: ''
        });
      }
    );
  }

  ngAfterViewInit(): void {
    this.saleListData.paginator = this.paginationTable;
  }

  /**
   * Method to apply filters to a table
   * @param event 
   */
  applyFiltersTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.saleListData.filter = filterValue.trim().toLocaleLowerCase();
  }

  /**
   * Method to search the sale list by params
   * @returns 
   */
  searchSales() {
    let _beginDate: string = '';
    let _endDate: string = '';

    if (this.searchForm.value.searchBy === 'date') {
      _beginDate = moment(this.searchForm.value.beginDate).format('DD/MM/YYYY');
      _endDate = moment(this.searchForm.value.endDate).format('DD/MM/YYYY');

      if (_beginDate === 'Invalid date' || _endDate === 'Invalid date') {
        this._commonService.showAlert('Debe ingresar ambas fechas', 'Oops!');
        return;
      }
    }

    this._saleService.history(
      this.searchForm.value.searchBy,
      this.searchForm.value.number,
      _beginDate,
      _endDate
    ).subscribe({
      next: (data) => {
        if (data.status) this.saleListData = data.value;
        else this._commonService.showAlert('No se encontraron datos', 'Oops!');
      },
      error: (error) => {
        this._commonService.showAlert('Ocurrió un error', 'Error');
      }
    });
  }

  /**
   * Method to open the modal with the sale detail
   * @param _sale 
   */
  getSaleDetail(_sale: Sale) {
    this.dialog.open(SaleDetailModalComponent, {
      data: _sale,
      disableClose: true,
      width: '700px'
    });
  }
}
