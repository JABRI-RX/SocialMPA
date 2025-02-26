import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {StockService} from '../../service/stock/Stock.service';
import {Stock} from '../../models/entities/Stock';
import {CurrencyPipe} from '@angular/common';
import {ActivatedRoute, Route, Router} from '@angular/router';
import {Subscription} from 'rxjs';
import {SComment} from '../../models/entities/SComment';

@Component({
   selector: 'app-stock-detail',
   imports: [
      CurrencyPipe
   ],
   template: `
      <br>
      @if (stock) {
         <div class="flex justify-center">
            <div class="grid  grid-cols-1 w-[50%] md:grid-cols-1 lg:grid-cols-1 py-3">
<!--               gap-x-80 gap-y-80-->
               <!-- Repeat this card for each stock -->
               <div class="card bg-base-100 shadow-xl hover:shadow-2xl transition-shadow duration-200">
                  <div class="card-body">
                     <div class="flex items-center justify-between mb-4">
                        <div>
                           <h2 class="card-title text-2xl">
                              {{ stock.symbole }}
                              <div class="badge badge-accent">{{ stock.industry }}</div>
                           </h2>
                           <p class="text-sm text-gray-500">{{ stock.companyName }}</p>
                        </div>
                        <div class="text-right">
                           <div class="text-lg font-bold text-primary">
                              {{ stock.marketCap | currency }}
                           </div>
                           <div class="text-xs">Market Cap</div>
                        </div>
                     </div>

                     <div class="grid grid-cols-2 gap-4 mb-4">
                        <div>
                           <p class="text-sm font-semibold">Purchase Price</p>
                           <p class="text-lg">{{ stock.purchase | currency }}</p>
                        </div>
                        <div>
                           <p class="text-sm font-semibold">Last Dividend</p>
                           <p class="text-lg">{{ stock.lastDiv | currency }}</p>
                        </div>
                     </div>

                     <div class="flex justify-between items-center mt-4">
                        <div class="flex-1">
                           <div class="text-xs uppercase text-gray-500">Stock Details</div>
                           <div class="text-sm">NYSE • Common Stock</div>
                        </div>
                        <div class="flex gap-2">
                           <a class="btn btn-accent btn-sm" href="{{'https://finance.yahoo.com/quote/'+stock.symbole}}" target="_blank">
                              <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 24 24" fill="none"
                                   stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                 <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"></path>
                                 <circle cx="12" cy="12" r="3"></circle>
                              </svg>
                              Yahoo
                           </a>
                           <button class="btn btn-primary btn-sm">
                              <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 24 24" fill="none"
                                   stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                 <line x1="12" y1="5" x2="12" y2="19"></line>
                                 <line x1="5" y1="12" x2="19" y2="12"></line>
                              </svg>
                              Add
                           </button>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </div>


      } @else {
         <h1>Stock Not Found</h1>
      }

   `,
   styles: ``
})
export class StockDetailComponent implements OnInit, OnDestroy {
   stock: Stock | undefined;
   private routerSub: Subscription = new Subscription();

   constructor(private stockService: StockService,
               private router: ActivatedRoute) {
   }

   ngOnDestroy(): void {
      this.routerSub.unsubscribe();
   }

   ngOnInit(): void {
      this.routerSub = this.router.params.subscribe((params) => {
         let id = params["id"]
         this.stockService.getStockById(id).subscribe((stock: Stock) => {
            this.stock = stock;
         });
      })
   }

}
