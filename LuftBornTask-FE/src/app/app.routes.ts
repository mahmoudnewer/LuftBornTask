import { Routes } from '@angular/router';
import { authGuard } from './Guards/auth.guard';
import { ProductManagerComponent } from './Features/Products/Components/product-manager.component';
import { LoginComponent } from './Features/Auth/Login/login.component';


export const routes: Routes = [
    { path: 'products', component: ProductManagerComponent, canActivate: [authGuard] },
    { path: 'login', component: LoginComponent },
    { path: '', redirectTo: 'products', pathMatch: 'full' }
  ];
  
