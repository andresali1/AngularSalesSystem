import { Component, AfterViewInit, ViewChild, OnInit } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { ProductModalComponent } from '../../Modals/product-modal/product-modal.component';
import { Product } from 'src/app/Interfaces/product';
import { ProductService } from 'src/app/Services/product.service';
import { CommonService } from 'src/app/Utilities/common.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit, AfterViewInit {
  columnsTable: string[] = ['productName', 'categoryDescription', 'stock', 'price', 'status', 'actions'];
  beginData: Product[] = [];
  dataListProduct = new MatTableDataSource(this.beginData);
  @ViewChild(MatPaginator) tablePagination!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _productService: ProductService,
    private _commonService: CommonService
  ) { }

  /**
   * Method to get all the products
   */
  getProducts() {
    this._productService.list().subscribe({
      next: (data) => {
        if (data.status) {
          this.dataListProduct.data = data.value;
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
    this.getProducts();
  }

  ngAfterViewInit(): void {
    this.dataListProduct.paginator = this.tablePagination;
  }

  /**
   * Method to apply filters to the table
   * @param event 
   */
  applyFiltersTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListProduct.filter = filterValue.trim().toLocaleLowerCase();
  }

  /**
   * Method to open the modal to create a Product
   */
  newProduct() {
    this.dialog.open(ProductModalComponent, {
      disableClose: true
    }).afterClosed().subscribe(result => {
      if (result == 'true') this.getProducts();
    });
  }

  /**
   * Method to open the modal to update a Product
   * @param product 
   */
  editProduct(product: Product) {
    this.dialog.open(ProductModalComponent, {
      disableClose: true,
      data: product
    }).afterClosed().subscribe(result => {
      if (result == 'true') this.getProducts();
    });
  }

  /**
   * Method to delete a product
   * @param product 
   */
  deleteProduct(product: Product) {
    Swal.fire({
      title: '¿Desea eliminar el producto?',
      text: product.productName,
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Si, eliminar',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No, volver'
    }).then((result) => {
      if (result.isConfirmed) {
        this._productService.delete(product.productId).subscribe({
          next: (data) => {
            if (data.status) {
              this._commonService.showAlert('El producto fue eliminado', 'Exito');
              this.getProducts();
            } else {
              this._commonService.showAlert('No se pudo eliminar el producto', 'Error');
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
