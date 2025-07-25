import { Routes } from '@angular/router';
import { authGuard } from './auth.guard';
import { ProductManagerComponent } from './products/product-manager/product-manager.component';
import { LoginComponent } from './Auth/login/login.component';

export const routes: Routes = [
    { path: 'products', component: ProductManagerComponent, canActivate: [authGuard] },
    { path: 'login', component: LoginComponent },
    { path: '', redirectTo: 'products', pathMatch: 'full' }
  ];
  
