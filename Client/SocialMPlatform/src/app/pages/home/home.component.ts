import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../service/auth/Auth.service';
import {TokenService} from '../../service/auth/Token.service';

@Component({
  selector: 'app-home',
  imports: [],
  template: `
    <div class="flex justify-center">

       <button class="btn btn-primary" (click)="getClaims()">Get Claims</button>
    </div>
  `,
  standalone: true,
  styles: ``
})
export class HomeComponent  implements  OnInit{
   token:string = "empty";
   constructor(private tokenService:TokenService) {
   }
   ngOnInit() {
      this.token = this.tokenService.getToken();


   }

   getClaims() {
      console.log(this.tokenService.isValidToken())

   }
}
