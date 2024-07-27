import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, RouterModule } from '@angular/router';

import { routes } from './app.routes';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { LocalService } from '../services/local.service';
import { jwtFactory } from './jwt-options';
import { error } from 'console';
import { errorInterceptor } from '../inceptors/errorHandling.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(
      withInterceptors([errorInterceptor])
    ),
    importProvidersFrom([
      FormsModule,
      RouterModule,
      HttpClientModule,
      BrowserModule,
      JwtModule.forRoot({
        jwtOptionsProvider: {
          provide: JWT_OPTIONS,
          useFactory: jwtFactory,
          deps: [LocalService]
        }
      }),
    ])]
  
};
