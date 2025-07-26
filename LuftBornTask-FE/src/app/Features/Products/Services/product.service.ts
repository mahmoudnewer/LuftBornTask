import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';

export interface ApiResponse<T> {
  success: boolean;
  data: T;
  errors?: string[];
  statusCode: number;
}

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = 'https://localhost:7188/api/Product';

  constructor(private http: HttpClient, private auth: AuthService) {}

  getAll(): Observable<ApiResponse<any[]>> {
    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          Authorization: `Bearer ${token}`
        });
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}/GetAll`, { headers });
      })
    );
  }

  getById(id: string): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${id}`);
  }

  create(product: any): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(this.baseUrl, product);
  }

  update(id: string, product: any): Observable<ApiResponse<any>> {
    return this.http.put<ApiResponse<any>>(`${this.baseUrl}/${id}`, product);
  }

  delete(id: string): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(`${this.baseUrl}/${id}`);
  }
}
