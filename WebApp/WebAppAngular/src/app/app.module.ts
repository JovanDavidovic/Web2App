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

const routes: Routes = [
  {path:"home", component: HomeComponent},
  {path:"login", component: LoginComponent},
  {path:"register", component: RegisterComponent},
  {path:"modify", component: ModifyComponent},
  {path:"error", component: ErrorComponent},
  {path:"uploadPhoto", component: UploadPhotoComponent},
  {path:"validateUsers", component: ValidateUsersComponent, canActivate: [ControllerGuard]},
  {path:"viewProcessingUser", component:ViewProcessingUserComponent, canActivate: [ControllerGuard]},
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
    ViewProcessingUserComponent
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
