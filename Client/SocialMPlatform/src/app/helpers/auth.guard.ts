import {ActivatedRouteSnapshot,
   CanActivate,
   Router,
} from '@angular/router';
import {AuthService} from '../service/auth/Auth.service';
import {Injectable} from '@angular/core';
import {TokenService} from '../service/auth/Token.service';
@Injectable({
   providedIn:"root"
})
export class AuthGuard implements CanActivate {
   constructor(private authService: AuthService,
               private tokenService:TokenService,
               private router: Router) {
   }

   canActivate():boolean {
      // console.log("canActiavte has been triggred "+this.tokenService.isValidToken())
      if(!this.authService.isAuthenticated()


      )
      {
         this.router.navigate(["login"])
         return false;
      }
      return true;

    }

}
