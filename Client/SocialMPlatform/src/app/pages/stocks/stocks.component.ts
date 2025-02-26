import {Component, OnInit} from '@angular/core';
import {StockService} from '../../service/stock/Stock.service';
import {Stock} from '../../models/entities/Stock';
import {RouterLink} from '@angular/router';
import {CurrencyPipe, NgForOf} from '@angular/common';

@Component({
   selector: 'app-stocks',
   imports: [
      RouterLink,
      CurrencyPipe
   ],
   template: `
      <div class="overflow-x-auto">
         <table class="table">
            <!-- head -->
            <thead>
            <tr>
               <th></th>
               <th>Symbol</th>
               <th>CompanyName</th>
               <th>Purchase</th>
               <th>LastDiv</th>
               <th>Industry</th>
               <th>MarketCap</th>
               <th>Comments</th>
               <th>Add</th>
            </tr>
            </thead>
            <tbody>
            <!-- row 1 -->
               @for (stock of stocks; track stock.id) {
                  <tr>
                     <th></th>
                     <td>

                        <div class="font-bold">{{ stock?.symbole }}</div>
                     </td>
                     <td>
                        {{ stock?.companyName }}
                     </td>
                     <td>{{ stock?.purchase }}</td>
                     <td>{{ stock?.lastDiv }}</td>
                     <td>{{ stock?.industry }}</td>
                     <td>{{ stock?.marketCap  | currency }}</td>
                     <td>
                        <a [routerLink]="['/stock',stock.id]" class="btn btn-accent">Details</a>
                     </td>
                     <td>
                        <a routerLink="" class="btn btn-primary">
                           Add
                        </a>
                     </td>
                  </tr>
               }
            </tbody>

         </table>

      </div>
   `,
   standalone: true,
   styles: ``
})
export class StocksComponent implements OnInit {
   stocks: Stock[] = [];

   constructor(private stockService: StockService) {
   }

   ngOnInit(): void {
      this.stockService.getAllStocks({
         isDescending: false,
         pageNumber: 0,
         pageSize: 10,
      })
         .subscribe((value: Stock[]) => {
            this.stocks = value;
            // console.log(format(20000))
         }, (error) => {
            console.log(error);
         })
   }

}
