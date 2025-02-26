import {Component, signal} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgClass, NgIf} from '@angular/common';
import {AuthService} from '../../service/auth/Auth.service';
import {HttpErrorResponse} from '@angular/common/http';
import {ErrorType} from '../../models/errorModel';
import {AuthResponseModel} from '../../models/auth/authResponseModel';
import {RegisterRequestModel} from '../../models/auth/registerRequestModel';

@Component({
   selector: 'app-register',
   imports: [
      FormsModule,
      NgIf,
      ReactiveFormsModule,
      NgClass
   ],
   template: `
      <div class="hero bg-base-200 min-h-screen">
         <div class="hero-content flex-col lg:flex-row-reverse">
            <div class="card bg-base-100 max-w-xl shrink-0 shadow-2xl w-[22rem] lg:w-[52rem]">
               <div *ngIf="status.stat" class="toast toast-top toast-end">
                  <div class="alert alert-success" [ngClass]="status.type">
                     <span>{{ this.status.text }}</span>
                  </div>
               </div>
               <form class="card-body" [formGroup]="registerForm">
                  <div class="form-control">
                     <label class="label">
                        <span class="label-text">Username</span>
                     </label>
                     <input formControlName="username" type="text" placeholder="username" class="input input-bordered"
                            required/>
                     <p *ngIf="username?.hasError('required')" class="text-red-400 text-sm">Usename is required</p>
                     <p *ngIf="username?.hasError('minlength') || username?.hasError('maxlength')"
                        class="text-red-400 text-sm">
                        Username Length Should Be Between 5 and 50 characters
                     </p>
                  </div>
                  <div class="form-control">
                     <label class="label">
                        <span class="label-text">Email</span>
                     </label>
                     <input formControlName="email" type="email" placeholder="email" class="input input-bordered"
                            required/>
                     <p *ngIf="email?.hasError('required')" class="text-red-400 text-sm">User is required</p>
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
                     <p *ngIf="password?.hasError('minlength') || username?.hasError('maxlength')"
                        class="text-red-400 text-sm">
                        Password Length Should Between 12 and 100 characters
                     </p>
                  </div>

                  <div class="form-control mt-6">
                     <button class="btn btn-primary" (click)="register()" [disabled]="!registerForm.valid || loading()">
                        Register
                     </button>
                  </div>
               </form>
            </div>
         </div>
      </div>
   `,
   standalone: true,
   styles: ``
})
export class RegisterComponent {
   registerForm = new FormGroup({
      username: new FormControl("salah", [
         Validators.required,
         Validators.minLength(5),
         Validators.maxLength(50)
      ]) ?? "",
      email: new FormControl("youness@gmail.com", [
         Validators.required,
         Validators.email
      ]) ?? "",
      password: new FormControl("Youness12345@", [
         Validators.required,
         Validators.minLength(12),
         Validators.maxLength(100),
      ])
   })
   status = {
      text: "",
      stat: false,
      type: ""
   };
   //use state for loading check hhh
   loading = signal(false);
   constructor(private authService: AuthService) {}
   get username() {
      return this.registerForm.get("username");
   }
   get email() {
      return this.registerForm.get("email");
   }
   get password() {
      return this.registerForm.get("password");
   }
   register() {
      const registerModel: RegisterRequestModel = {
         username: this.registerForm.value.username ?? "",
         email: this.registerForm.value.email ?? "",
         password: this.registerForm.value.password ?? ""
      };
      this.loading.set(true);
      this.authService.register(registerModel)
         .subscribe((authModel : AuthResponseModel) => {
            this.updateStatus("Account Created",ErrorType.info)

         }, (error:HttpErrorResponse )=> {
            console.log(error.error.message);
            this.updateStatus(error.error.message,ErrorType.error)
         });
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
         this.loading.set(false);
         // this.cdref.detectChanges();
      }, 2000);

   }
}
