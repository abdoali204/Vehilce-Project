import { Injectable } from '@angular/core';
import {  CanActivate } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(protected auth : AuthService) { }
  canActivate()
  {
    if(this.auth.loggedIn)
      return true;
    this.auth.login();
    return false;
  }
}
