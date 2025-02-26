import {SComment} from './SComment';

export interface Stock {
   id: number
   symbole: string
   companyName: string
   purchase: number
   lastDiv: number
   industry: string
   marketCap: number
   comments: SComment[]
}


