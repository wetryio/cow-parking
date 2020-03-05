import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DevicesDebugComponent } from './components/devices-debug/devices-debug.component';
import { HttpClientModule } from '@angular/common/http';
import { DeviceService } from './services/device.service';

@NgModule({
  declarations: [
    AppComponent,
    DevicesDebugComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [DeviceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
