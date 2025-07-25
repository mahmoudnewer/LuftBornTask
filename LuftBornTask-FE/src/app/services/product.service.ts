import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';

@Injectable({ providedIn: 'root' }) // automatically available app-wide
export class ProductService {
  private baseUrl = 'https://localhost:7188/secure';

  constructor(private http: HttpClient,private auth: AuthService) {}

  getAll(): Observable<any[]> {
    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          Authorization: `Bearer ${token}`
        });
        return this.http.get<any[]>(this.baseUrl, { headers });
      })
    );
  }

  getById(id: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  create(product: any): Observable<any> {
    return this.http.post(this.baseUrl, product);
  }

  update(id: string, product: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, product);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
