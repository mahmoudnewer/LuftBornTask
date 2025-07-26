import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  login() {
    window.location.href = 'https://localhost:7188/auth/login';
  }

  logout() {
    window.location.href = 'https://localhost:7188/auth/logout';
  }
}
//If I want to delegate the authorization process to the be