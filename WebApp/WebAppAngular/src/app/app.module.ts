import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { HttpService } from './services/http/http.services';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorComponent } from './error/error.component';
import { ModifyComponent } from './modify/modify.component';
import { UploadPhotoComponent } from './upload-photo/upload-photo.component';
import { AuthGuard } from './guards/auth.guard';
import { JwtService } from './services/jwt.service';
import { ValidateUsersComponent } from './validate-users/validate-users.component';
import { ControllerGuard } from './guards/controller.guard';
import { ViewProcessingUserComponent } from './view-processing-user/view-processing-user.component';
import { PricelistComponent } from './pricelist/pricelist.component';
import { ViewPricelistsComponent } from './view-pricelists/view-pricelists.component';
import { ViewFuturePricelistsComponent } from './view-future-pricelists/view-future-pricelists.component';
import { ModifyPricelistComponent } from './modify-pricelist/modify-pricelist.component';
import { ErrorPricelistComponent } from './error-pricelist/error-pricelist.component';
import { BuyTicketComponent } from './buy-ticket/buy-ticket.component';

const routes: Routes = [
  {path:"home", component: HomeComponent},
  {path:"login", component: LoginComponent},
  {path:"register", component: RegisterComponent},
  {path:"modify", component: ModifyComponent},
  {path:"error", component: ErrorComponent},
  {path:"uploadPhoto", component: UploadPhotoComponent},
  {path:"validateUsers", component: ValidateUsersComponent, canActivate: [ControllerGuard]},
  {path:"viewProcessingUser", component:ViewProcessingUserComponent, canActivate: [ControllerGuard]},
  {path:"pricelist", component: PricelistComponent, canActivate: [AuthGuard]},
  {path:"viewPricelists", component: ViewPricelistsComponent, canActivate: [AuthGuard]},
  {path:"viewFuturePricelists", component: ViewFuturePricelistsComponent, canActivate: [AuthGuard]},
  {path:"modifyPricelist", component: ModifyPricelistComponent, canActivate: [AuthGuard]},
  {path:"errorPricelist", component: ErrorPricelistComponent, canActivate: [AuthGuard]},
  {path: "buyTicket", component: BuyTicketComponent},
  {path:"", component: HomeComponent, pathMatch:"full"}
]

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ErrorComponent,
    ModifyComponent,
    UploadPhotoComponent,
    ValidateUsersComponent,
    ViewProcessingUserComponent,
    PricelistComponent,
    ViewPricelistsComponent,
    ViewFuturePricelistsComponent,
    ModifyPricelistComponent,
    ErrorPricelistComponent,
    BuyTicketComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule
  ],
  providers: [HttpService, {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}, JwtService],
  bootstrap: [AppComponent]
})
export class AppModule { }
