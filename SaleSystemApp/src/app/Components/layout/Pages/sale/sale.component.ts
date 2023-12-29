import { Component } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from 'src/app/Services/product.service';
import { SaleService } from 'src/app/Services/sale.service';
import { CommonService } from 'src/app/Utilities/common.service';

import { Product } from 'src/app/Interfaces/product';
import { Sale } from 'src/app/Interfaces/sale';
import { SaleDetail } from 'src/app/Interfaces/sale-detail';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.css']
})
export class SaleComponent {
  productsList: Product[] = [];
  productsListFilter: Product[] = [];

  toSaleProductList: SaleDetail[] = [];
  registerButtomLock: boolean = false;

  selectedProduct!: Product;
  defaultPaymentType: string = 'Efectivo';
  total: number = 0;

  saleProductForm: FormGroup;
  tableColumns: string[] = ['product', 'amount', 'price', 'total', 'action'];
  saleDetailData = new MatTableDataSource(this.toSaleProductList);

  /**
   * Method to filter products
   * @param param 
   * @returns 
   */
  productsByFilter(param: any): Product[] {
    const searchedValue = typeof param === 'string' ? param.toLocaleLowerCase() : param.productName.toLocaleLowerCase();

    return this.productsList.filter(item => item.productName.toLocaleLowerCase().includes(searchedValue));
  }

  constructor(
    private fb: FormBuilder,
    private _productService: ProductService,
    private _saleService: SaleService,
    private _commonService: CommonService
  ) {
    this.saleProductForm = this.fb.group({
      product: ['', Validators.required],
      amount: ['', Validators.required]
    });

    this._productService.list().subscribe({
      next: (data) => {
        if (data.status) {
          const list = data.value as Product[];
          this.productsList = list.filter(p => p.isActive == 1 && p.stock > 0);
        }
      },
      error: (error) => {
        this._commonService.showAlert('Ocurrió un error', 'Error!');
      }
    });

    this.saleProductForm.get('product')?.valueChanges.subscribe(value => {
      this.productsListFilter = this.productsByFilter(value);
    });
  }

  /**
   * Method to chow the name of a product
   * @param product 
   * @returns 
   */
  showProduct(product: Product): string {
    return product.productName;
  }

  /**
   * Method to select a product to sale
   * @param event 
   */
  getToSaleProduct(event: any) {
    this.selectedProduct = event.option.value;
  }

  /**
   * Method to add a Product to sale
   */
  addProductToSale() {
    const _amount: number = this.saleProductForm.value.amount;
    const _price: number = parseFloat(this.selectedProduct.price);
    console.log(`precio: ${_price}`)
    const _total: number = _amount * _price;
    console.log(`total: ${_total}`)
    this.total = this.total + _total;

    this.toSaleProductList.push({
      productId: this.selectedProduct.productId,
      productDescription: this.selectedProduct.productName,
      amount: _amount,
      priceText: String(_price.toFixed(2)),
      totalText: String(_total.toFixed(2))
    });

    this.saleDetailData = new MatTableDataSource(this.toSaleProductList);

    this.saleProductForm.patchValue({
      product: '',
      amount: ''
    });
  }

  /**
   * Method to remove a product from the sale list
   * @param detail 
   */
  deleteProduct(detail: SaleDetail) {
    this.total = this.total - parseFloat(detail.totalText);
    this.toSaleProductList = this.toSaleProductList.filter(p => p.productId != detail.productId);
    this.saleDetailData = new MatTableDataSource(this.toSaleProductList);
  }

  /**
   * Method to register a new Sale
   */
  registerSale() {
    if (this.toSaleProductList.length > 0) {
      this.registerButtomLock = true;

      const request: Sale = {
        paymentType: this.defaultPaymentType,
        totalText: String(this.total.toFixed(2)),
        saleDetails: this.toSaleProductList
      }

      this._saleService.register(request).subscribe({
        next: (response) => {
          if (response.status) {
            this.total = 0.00;
            this.toSaleProductList = [];
            this.saleDetailData = new MatTableDataSource(this.toSaleProductList);

            Swal.fire({
              icon: 'success',
              title: 'Venta Registrada!',
              text: `Número de venta ${response.value.documentNumber}`
            });
          } else {
            this._commonService.showAlert('No se pudo registrar la Venta', 'Oops!');
          }
        },
        complete: () => {
          this.registerButtomLock = false;
        },
        error: (error) => {
          this._commonService.showAlert('Ocurrió un error', 'Error');
        }
      });
    }
  }
}
