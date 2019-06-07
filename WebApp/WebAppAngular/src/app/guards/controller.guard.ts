import { Injectable } from '@angular/core';
import {
  CanActivate, Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivateChild,
} from '@angular/router';
import { JwtService } from '../services/jwt.service';

@Injectable({
  providedIn: 'root',
})
export class ControllerGuard implements CanActivate, CanActivateChild {
  constructor(private router: Router, private jwt: JwtService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {    
    if (this.jwt.getRole() === 'Controller') {
      return true;
    }
    // not logged in so redirect to login page
    else {
      console.error("Can't access, not controller");
      this.router.navigate(["home"]);
      return false;
    }
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }

}
