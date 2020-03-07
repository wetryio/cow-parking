import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';

import { AuthGuard } from './guards/auth.guard';
import { Layout2Component } from './layout/layout-2/layout-2.component';


const routes: Routes = [

  {
    path: '',
    component: Layout2Component,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'account',
        loadChildren: () => import('./modules/account/account.module')
                                .then(m => m.AccountModule)
      },
      {
        path: 'entity',
        loadChildren: () => import('./modules/entity/entity.module')
                                .then(m => m.EntityModule)
      },
      {
        path: '',
        loadChildren: () => import('./modules/dashboard/dashboard.module')
                                .then(m => m.DashboardModule)
      }
    ]
  },
  { path: '**', component: NotFoundComponent }
];

// *******************************************************************************
//

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
