import {ChangeDetectorRef, Component, Input, OnInit} from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import {AuthService} from '../../service/auth/Auth.service';
import {UserService} from '../../service/auth/User.service';

@Component({
   selector: 'app-navbar',
   imports: [
      RouterLink
   ],
   template: `
      <div class="navbar bg-base-100">
         <div class="navbar-start">
            <div class="dropdown">
               <div tabindex="0" role="button" class="btn btn-ghost lg:hidden">
                  <svg
                     xmlns="http://www.w3.org/2000/svg"
                     class="h-5 w-5"
                     fill="none"
                     viewBox="0 0 24 24"
                     stroke="currentColor">
                     <path
                        stroke-linecap="round"
                        stroke-linejoin="round"
                        stroke-width="2"
                        d="M4 6h16M4 12h8m-8 6h16"/>
                  </svg>
               </div>
               <ul
                  tabindex="0"
                  class="menu menu-sm dropdown-content bg-base-100 rounded-box z-[1] mt-3 w-52 p-2 shadow">
                  <li><a routerLink="/">Home</a></li>
                  <li><a routerLink="/myportfolios">My Portfolio</a></li>
                  <li><a routerLink="/stocks">Stocks</a></li>
               </ul>
            </div>
            <a class="btn btn-ghost text-xl">SocialMPA</a>
         </div>
         <div class="navbar-center hidden lg:flex">
            <ul class="menu menu-horizontal px-1">
               <li><a [routerLink]="['/']">Home</a></li>
               <li><a [routerLink]="['myportfolios']">My Portfolio</a></li>
               <li><a [routerLink]="['stock']">Stocks</a></li>
            </ul>
         </div>
         @if (!isLoggedIn) {
            <div class="navbar-end space-x-4">
               <a class="btn " routerLink="/login">Login</a>
               <a class="btn " routerLink="/register">Register</a>
            </div>
         } @else {

            <div class="navbar-end space-x-1 px-3">
               <a routerLink="/profile">
                  <div class="avatar online placeholder">

                     <div class="bg-neutral text-neutral-content w-10 lg:w-15 rounded-full">
                        <span class="text-xl">{{ userName }}</span>
                     </div>
                  </div>
               </a>
            </div>
            <button (click)="logout()" class="btn btn-error">Log OUt</button>

         }
      </div>
   `,
   standalone: true,
   styles: ``
})
export class NavbarComponent implements OnInit {
   @Input() isLoggedIn = false;
   userName = "SA";
   constructor(private authService: AuthService,
               private router:Router,
               private  userService:UserService) {
   }

   ngOnInit(): void {
      if(!this.authService.isAuthenticated())
         return;
      this.isLoggedIn = true;
      this.userName = this.userService.getUsernameFromToken().substring(0,2);
   }

   logout() {
      this.authService.logout();
      this.isLoggedIn  = false;
      //this.cdref.detectChanges();
      this.router.navigate(['login'])
   }
}
