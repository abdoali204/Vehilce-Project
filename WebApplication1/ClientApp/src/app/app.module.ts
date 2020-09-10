import {  FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ChartModule } from 'angular2-chartjs';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { VehicleService } from './services/vehicle.service';
import { AppErrorHandler } from './app.error-handler';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { PhotoService } from './services/photo.service';
import { AuthService } from './services/auth.service';
import { InterceptorService } from './services/interceptor.service';
import { AdminComponent } from './admin/admin.component';
import { AuthGuard } from './services/auth-guard.service';
import { AdminAuthGuard } from './services/admin-auth-gard.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    VehicleDetailsComponent,
    AdminComponent
  ],
  imports: [
    FormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ChartModule,
    
    RouterModule.forRoot([
      { path: '',redirectTo : 'vehicles', pathMatch: 'full' },
      { path: 'admin', component : AdminComponent, canActivate : [AdminAuthGuard]},
      { path: 'vehicles', component : VehicleListComponent},
      { path: 'vehicles/new', component : VehicleFormComponent, canActivate : [AuthGuard]},
      { path: 'vehicles/edit/:id', component : VehicleFormComponent,canActivate : [AuthGuard]},
      { path: 'vehicles/:id', component : VehicleDetailsComponent},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent }
      
    ])
  ],

  bootstrap: [AppComponent],
  providers: [
   
    AuthService,
    {provide: HTTP_INTERCEPTORS, useClass : InterceptorService, multi : true},
    VehicleService,
    AuthGuard,
    AdminAuthGuard,
    PhotoService,

    
  ]
})
export class AppModule { }
