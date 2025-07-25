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
