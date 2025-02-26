import {Component, signal} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgClass, NgIf} from '@angular/common';
import {AuthService} from '../../service/auth/Auth.service';
import {HttpErrorResponse} from '@angular/common/http';
import {ErrorType} from '../../models/errorModel';
import {Router} from '@angular/router';
import {fadeInAnimation} from '../../animations/animations';
import {TokenService} from '../../service/auth/Token.service';
import {AuthResponseModel} from '../../models/auth/authResponseModel';
import {LoginRequestModel} from '../../models/auth/loginRequestModel';

@Component({
   selector: 'app-login',
   imports: [
      ReactiveFormsModule,
      NgIf,
      FormsModule,
      NgClass,
   ],
   animations: [fadeInAnimation],
   template: `
      <div class="hero bg-base-200 min-h-screen">
         <div class="hero-content flex-col lg:flex-row-reverse">
            <div class="card bg-base-100 max-w-xl shrink-0 shadow-2xl w-[100%] lg:w-[120%]">
               <div *ngIf="status.stat" class="toast toast-top toast-end" @fadeIn>
                  <div class="alert " [ngClass]="status.type">
                     <span>{{ this.status.text }}</span>
                  </div>
               </div>
               <form class="card-body" [formGroup]="loginForm">
                  <div class="form-control">
                     <label class="label">
                        <span class="label-text">Email</span>
                     </label>
                     <input formControlName="email" type="email" placeholder="email" class="input input-bordered"
                            required/>
                     <p *ngIf="email?.hasError('required')" class="text-red-400 text-sm">Email is required</p>
                     <p *ngIf="email?.hasError('email')" class="text-red-400 text-sm">Email format wrong</p>
                  </div>
                  <div class="form-control space-y-1">
                     <label class="label">
                        <span class="label-text">Password</span>
                     </label>
                     <input formControlName="password" type="password" placeholder="password"
                            class="input input-bordered"
                            required/>
                     <p *ngIf="password?.hasError('required')" class="text-red-400 text-sm">Password is required</p>
                     <!-- Open the modal using ID.showModal() method -->
                     <br>
                     <a class="label-text-alt link link-hover" onclick="my_modal_1.showModal()">Forgot Password</a>
                     <dialog id="my_modal_1" class="modal">
                        <div class="modal-box">
                           <h3 class="text-lg font-bold">Forgot Password</h3>
                           <p class="py-4">If your email exists we will send you th link </p>
                           <label class="label">
                              <span class="label-text">Email</span>
                           </label>
                           <input type="email" placeholder="email" class="input input-bordered w-full"
                                  required [(ngModel)]="resetEmail" [ngModelOptions]="{standalone: true}"/>
                           <div class="modal-action">
                              <form method="dialog">
                                 <!-- if there is a button in form, it will close the modal -->
                                 <button class="btn btn-primary" (click)="resetPassword()">Send</button>
                              </form>
                           </div>
                        </div>
                     </dialog>
                  </div>

                  <div class="form-control mt-6">
                     <button class="btn btn-primary" (click)="login()" [disabled]="!loginForm.valid || loading()">
                        @if (loading()) {
                           <span *ngIf="loading()" class="loading loading-spinner "></span>
                        } @else {
                           Login
                        }
                     </button>
                  </div>
               </form>
            </div>
         </div>
      </div>
   `,
   standalone: true,
})
export class LoginComponent {
   loginForm = new FormGroup({
      email: new FormControl('liquid22@DD.com', [Validators.required, Validators.email]),
      password: new FormControl('Liquid1986@@@@', [Validators.required])
   });
   status = {
      text: "",
      stat: false,
      type: ""
   };
   resetEmail: string = "";
   loading = signal(false);

   constructor(private authService: AuthService,
               private tokenService:TokenService,
               private router: Router) {

   }

   login(): void {
      const loginModel: LoginRequestModel = {
         email: this.loginForm.value.email ?? "",
         password: this.loginForm.value.password ?? ""
      }
      this.loading.set(true);
      this.authService.login(loginModel)
         .subscribe(
            (authModel: AuthResponseModel)   => {
               this.router.navigate(["/"])
               this.tokenService.saveToken(authModel.token);

            },
            (error: HttpErrorResponse) => {
               console.log(error);
               this.updateStatus(error.error.message, ErrorType.error);
            });

   }

   resetPassword(): void {
      console.log(this.resetEmail);
   }

   get email() {
      return this.loginForm.get("email");
   }

   get password() {
      return this.loginForm.get("password");
   }

   updateStatus(error: string, type: ErrorType) {

      this.status.text = error;
      this.status.stat = true;
      switch (type) {
         case ErrorType.error:
            this.status.type = "alert-error";
            break;
         case ErrorType.success:
            this.status.type = "alert-success";
            break;
         case ErrorType.info:
            this.status.type = "alert-info";
            break;
      }

      setTimeout(() => {
         this.status.stat = false;
         // this.cdref.detectChanges();
      }, 2000);
      this.loading.set(false);
   }
}
