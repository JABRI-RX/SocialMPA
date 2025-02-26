import {Injectable, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Stock} from '../../models/entities/Stock';
import {QueryObject} from '../../models/QueryObject';
import {APICONFIG} from '../../config/appsetting';
import {TokenService} from '../auth/Token.service';
import {Observable} from 'rxjs';

@Injectable({
   providedIn: "root"
})
export class StockService {
   stocks: Stock[] = [];
   private httpHeaders: HttpHeaders;

   constructor(private httpClient: HttpClient,
               private tokenService: TokenService) {
      this.httpHeaders = new HttpHeaders({
         Authorization: `Bearer ${this.tokenService.getToken()}`
      })
   }

   getAllStocks(query:QueryObject): Observable<Stock[]> {
      console.log(APICONFIG.apiUr + "stock?"+query);
      return this.httpClient.get<Stock[]>(APICONFIG.apiUr + "stock?"+query, {headers: this.httpHeaders})
   }
   getStockById(id:number):Observable<Stock>{
      return this.httpClient.get<Stock>(APICONFIG.apiUr + "stock/"+id,{headers: this.httpHeaders})
   }
}
