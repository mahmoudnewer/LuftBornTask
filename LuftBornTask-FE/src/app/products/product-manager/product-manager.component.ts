import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { ProductService } from "../../services/product.service";
import { AuthService } from "@auth0/auth0-angular";

// ... same imports
@Component({
  selector: 'app-product-manager',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './product-manager.component.html'
})
export class ProductManagerComponent implements OnInit {
  products: any[] = [];
  product: any = this.getEmptyProduct();
  toasts: { message: string; type: 'success' | 'danger' }[] = [];

  constructor(private productService: ProductService, public auth: AuthService) {}

  ngOnInit(): void {
    debugger;
    this.loadProducts();
  }
  logout() {
    this.auth.logout({
      logoutParams: {
        returnTo: window.location.origin
      }
    });  }
  getEmptyProduct() {
    return { id: null, name: '', price: null };
  }

  loadProducts() {
    this.productService.getAll().subscribe({
      next: data => {      debugger;
        this.products = data},
      error: () => this.showToast('Failed to load products', 'danger')
    });
  }

  saveProduct() {
    const action = this.product.id
      ? this.productService.update(this.product.id, this.product)
      : this.productService.create(this.product);

    action.subscribe({
      next: () => {
        this.loadProducts();
        this.clearForm();
        this.showToast('Product saved!', 'success');
      },
      error: () => this.showToast('Failed to save product', 'danger')
    });
  }

  editProduct(p: any) {
    this.product = { ...p };
  }

  confirmDelete(id: string) {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.delete(id).subscribe({
        next: () => {
          this.loadProducts();
          this.showToast('Product deleted!', 'success');
        },
        error: () => this.showToast('Failed to delete product', 'danger')
      });
    }
  }

  clearForm() {
    this.product = this.getEmptyProduct();
  }

  showToast(message: string, type: 'success' | 'danger') {
    const toast = { message, type };
    this.toasts.push(toast);
    setTimeout(() => this.removeToast(toast), 3000);
  }

  removeToast(toast: any) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}
