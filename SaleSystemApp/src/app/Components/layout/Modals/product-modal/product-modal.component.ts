import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Category } from 'src/app/Interfaces/category';
import { Product } from 'src/app/Interfaces/product';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductService } from 'src/app/Services/product.service';
import { CommonService } from 'src/app/Utilities/common.service';

@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrls: ['./product-modal.component.css']
})
export class ProductModalComponent implements OnInit {
  productForm: FormGroup;
  actionTitle: string = 'Agregar';
  actionbuttom: string = 'Guardar';
  categoriesList: Category[] = [];

  constructor(
    private actualModal: MatDialogRef<ProductModalComponent>,
    @Inject(MAT_DIALOG_DATA) public productData: Product,
    private fb: FormBuilder,
    private _categoryService: CategoryService,
    private _productService: ProductService,
    private _commonService: CommonService
  ) {
    this.productForm = fb.group({
      productName: ['', Validators.required],
      categoryId: ['', Validators.required],
      stock: ['', Validators.required],
      price: ['', Validators.required],
      isActive: ['1', Validators.required]
    });

    if (this.productData != null) {
      this.actionTitle = 'Editar';
      this.actionbuttom = 'Actualizar';
    }

    this._categoryService.list().subscribe({
      next: (data) => {
        if (data.status) this.categoriesList = data.value
      },
      error: (error) => {
        this._commonService.showAlert('Ocurrió un error', 'Error!');
      }
    });
  }

  ngOnInit(): void {
    if (this.productData != null) {
      this.productForm.patchValue({
        productName: this.productData.productName,
        categoryId: this.productData.categoryId,
        stock: this.productData.stock,
        price: this.productData.price,
        isActive: this.productData.isActive.toString()
      });
    }
  }

  /**
   * Method to Save or Update a Product
   */
  saveProduct() {
    const _product: Product = {
      productId: this.productData == null ? 0 : this.productData.productId,
      productName: this.productForm.value.productName,
      categoryId: this.productForm.value.categoryId,
      categoryDescription: '',
      stock: this.productForm.value.stock,
      price: this.productForm.value.price,
      isActive: parseInt(this.productForm.value.isActive)
    }

    if (_product.productId == 0) {
      this._productService.save(_product).subscribe({
        next: (data) => {
          if (data.status) {
            this._commonService.showAlert('El producto fue registrado', 'Exito');
            this.actualModal.close('true');
          } else {
            this._commonService.showAlert('No se pudo registrar el producto', 'Error!');
          }
        }, error: (error) => {
          this._commonService.showAlert('Ocurrió un error', 'Error!');
        }
      });
    } else {
      this._productService.edit(_product).subscribe({
        next: (data) => {
          if (data.status) {
            this._commonService.showAlert('El producto fue actualizado', 'Exito');
            this.actualModal.close('true');
          } else {
            this._commonService.showAlert('No se pudo actualizar el producto', 'Error!');
          }
        }, error: (error) => {
          this._commonService.showAlert('Ocurrió un error', 'Error!');
        }
      });
    }
  }
}
