import {Routes} from '@angular/router';
import {HomeComponent} from '../pages/home/home.component';
import {LoginComponent} from '../pages/login/login.component';
import {RegisterComponent} from '../pages/register/register.component';
import {PortfolioComponent} from '../pages/portfolio/portfolio.component';
import {StocksComponent} from '../pages/stocks/stocks.component';
import {AuthGuard} from '../helpers/auth.guard';
import {ProfileComponent} from '../pages/profile/profile.component';
import {StockDetailComponent} from '../pages/stock-detail/stock-detail.component';

export const routes: Routes = [
   {
      path: "",
      component: HomeComponent,
      title: "Home "
   },
   {
      path: "login",
      component: LoginComponent,
      title: "Login "
   },
   {
      path: "register",
      component: RegisterComponent,
      title: "register "
   },
   {
      path: "myportfolios",
      component: PortfolioComponent,
      title: "Portfolio ",
      canActivate: [AuthGuard]
   },
   {
      path: "stock",
      component: StocksComponent,
      title: "stocks",
      canActivate: [AuthGuard]
   },
   {
      path: "stock/:id",
      component:StockDetailComponent,
      title : "stock Details",
      canActivate:[AuthGuard]
   },
   {
      path: "profile",
      component: ProfileComponent,
      title: "My Profile",
      canActivate: [AuthGuard]
   }
];
