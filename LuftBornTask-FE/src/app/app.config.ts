import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideAuth0 } from '@auth0/auth0-angular';
import { routes } from './app.routes';
import { AuthInterceptor } from './Core/interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptorsFromDi()),
    provideRouter(routes),
    provideAuth0({
      domain: 'https://dev-rwd7fqhnvxkbfppi.us.auth0.com',
      clientId: 'ngiRwEvA9S5Dubl0qq65tcFiwVMb90cd',
      authorizationParams: {
        redirect_uri: window.location.origin,
        audience: 'https://localhost:7188',
        scope: 'openid profile email'
      }}),
      { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }

  ]
};
