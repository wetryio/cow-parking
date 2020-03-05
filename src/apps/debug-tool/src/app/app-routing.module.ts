import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DevicesDebugComponent } from './components/devices-debug/devices-debug.component';


const routes: Routes = [
  {
    path: 'debug/device',
    component: DevicesDebugComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
