import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { from } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { UserRegisterComponent } from './core/component/user-register/user-register.component';
import { LoginComponent } from './core/component/login/login.component';
import { UserComponent } from './core/component/user/user.component';
import { HomeComponent } from './core/component/home/home.component';
import { AppMaterial } from './app.material.module';
import { ForgotPasswordComponent } from './core/component/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './core/component/reset-password/reset-password.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import 'hammerjs';
import { MainNotesComponent } from './core/component/main-notes/main-notes.component';
@NgModule({
  declarations: [
    AppComponent,
    UserRegisterComponent,
    LoginComponent,
    UserComponent,
    HomeComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    MainNotesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    AppMaterial,
    ReactiveFormsModule,
    BrowserAnimationsModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
