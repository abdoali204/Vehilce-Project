import { Injectable } from '@angular/core';
import {  CanActivate } from '@angular/router';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth-guard.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuard extends AuthGuard {

  constructor( auth : AuthService) {
        super(auth); 
  }
  canActivate()
  {
      var loggedIn = super.canActivate();
      return loggedIn? this.auth.isInRole('Admin') : false;

  }
}
