import {Injectable} from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import {JWTClaimsModel} from '../../models/auth/JWTClaimsModel';

@Injectable({
   providedIn: "root"
})
export class TokenService {
   private readonly jwtHelper = new JwtHelperService();
   private readonly mustHaveClaims = ["roles", "uid", "username"]

   decodeJwt(token: string) {
      return this.jwtHelper.decodeToken(token);
   }

   saveToken(jwt: string) {
      if (jwt.trim().length === 0) {
         console.log("The JWT Is empty something is wrong");
      }
      localStorage.setItem("JWT", jwt)
   }

   getToken(): string {
      return localStorage.getItem("JWT") ?? "";
   }

   isValidToken(): boolean {//checks JWT format,empty,claims
      const token = this.getToken();
      if (token.trim().length === 0) {
         // console.log("empty token")
         return false;
      }
      let result: boolean = false;
      try {
         if (this.jwtHelper.isTokenExpired(token))
            result = false;
         const decodeToken = this.jwtHelper.decodeToken(token) ?? "";
         this.mustHaveClaims.forEach((claim) => {
            result = decodeToken.hasOwnProperty(claim);
         });
      } catch (e) {
         result = false;
      }

      return result;
   }
}
