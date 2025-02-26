import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PingModel} from '../../models/pingModel';
import {APICONFIG} from '../../config/appsetting';

@Injectable({
  providedIn: 'root'
})
export class PingService {
  constructor(private httpClient:HttpClient) {

  }
   pingService(): Observable<PingModel> {
      return this.httpClient.get<PingModel>(APICONFIG.apiUr  + "account/ping");
   }
}
