import {Component, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavbarComponent} from './components/navbar/navbar.component';
import {PingService} from './service/ping/ping.service';
import {NgIf} from '@angular/common';
import {fadeInAnimation} from './animations/animations';
import {FormsModule} from '@angular/forms';

@Component({
   selector: 'app-root',
   imports: [RouterOutlet, NavbarComponent, NgIf, FormsModule],
   animations: [fadeInAnimation],
   template: `
      <app-navbar></app-navbar>
      <div *ngIf="apiDown" class="toast toast-top toast-center" @fadeIn>
         <div class="alert alert-error">
            <span>The Api Is Down Check For Backend</span>
         </div>
      </div>
      <router-outlet/>
   `,
   standalone: true,

})
export class AppComponent implements OnInit {
   constructor(private pingService: PingService) {
   }

   apiDown = false;

   ngOnInit(): void {
      this.pingService.pingService().subscribe((response) => {
         this.apiDown = false;
      }, (error) => {
         this.apiDown = true;
         setTimeout(()=>{
            this.apiDown = false;
         },2000)

      })
   }

   title = 'SocialMPlatform';
}
