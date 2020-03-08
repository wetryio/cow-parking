import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// *******************************************************************************
// NgBootstrap

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

// *******************************************************************************
// App

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { AppService } from './app.service';
import { LayoutModule } from './layout/layout.module';

// *******************************************************************************
// Pages

import { AuthService } from './services/auth.service';
import { InterceptorService } from './interceptors/interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { EntityService } from './services/entity.service';
import { DeviceService } from './services/device.service';

// *******************************************************************************
//

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    HttpClientModule,
    AppRoutingModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    Title,
    AppService,
    EntityService,
    DeviceService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }
  ],

  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
