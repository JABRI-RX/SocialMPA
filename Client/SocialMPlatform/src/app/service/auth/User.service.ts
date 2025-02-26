import {Injectable} from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import {TokenService} from './Token.service';

@Injectable({
   providedIn: 'root'
})
export class UserService {

   constructor(private tokenService: TokenService) {

   }

   getUsernameFromToken(): string {
      const data = this.tokenService.decodeJwt(this.tokenService.getToken());
      return data["username"] ?? "Not Username"
   }
}
