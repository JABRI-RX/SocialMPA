import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {APICONFIG} from '../../config/appsetting';
import {JwtHelperService} from '@auth0/angular-jwt';
import {RegisterRequestModel} from '../../models/auth/registerRequestModel';
import {AuthResponseModel} from '../../models/auth/authResponseModel';
import {LoginRequestModel} from '../../models/auth/loginRequestModel';
import {TokenService} from './Token.service';

@Injectable({
   providedIn: 'root'
})
export class AuthService {
   private readonly apiUrl = APICONFIG.apiUr
   private readonly jwtHelper = new JwtHelperService();
   isLoggedIn:boolean = false;
   constructor(private httpClient: HttpClient,
               private tokenService:TokenService) {

   }

   register(registerModel: RegisterRequestModel): Observable<AuthResponseModel> {
      return this.httpClient.post<AuthResponseModel>(this.apiUrl + "account/register",registerModel);
   }

   login(loginModel: LoginRequestModel): Observable<AuthResponseModel> {
      return this.httpClient.post<AuthResponseModel>(this.apiUrl + "account/login", loginModel);
   }
   logout(){//TODO invalidate the token from the backend
      if(localStorage.getItem("JWT") == null){
         // console.log("JWT Token is Empty");
      }
      localStorage.removeItem("JWT");
   }
   isAuthenticated():boolean{
      // console.log(`this.tokenService.isValidToken == ${this.tokenService.isValidToken()}`)
      if(!this.tokenService.isValidToken()){
         // console.log("authService isAuthenticated I should be left")
         return false;
      }
      const token = this.tokenService.getToken();
      // console.log("authService isAuthenticated If I am here then something is fuckd up")
      return !this.jwtHelper.isTokenExpired(token);
   }


}
