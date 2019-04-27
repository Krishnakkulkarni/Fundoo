import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './core/component/login/login.component';
import { UserRegisterComponent } from './core/component/user-register/user-register.component';
import { UserComponent } from './core/component/user/user.component';
import { HomeComponent } from './core/component/home/home.component';
import { ForgotPasswordComponent } from './core/component/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './core/component/reset-password/reset-password.component';
import { AuthGuard } from './core/auth/auth.guard';

//This is my case 
const routes: Routes = [  
  {path:'',redirectTo:'/user/login',pathMatch:'full'},
  {
    path: 'user', component: UserComponent,
    children: 
    [
      { path: 'user-register', component: UserRegisterComponent },
      { path: 'login', component: LoginComponent },
    ]
  },
    { path:'forgot-password',component:ForgotPasswordComponent },
     {path:'reset-password', component:ResetPasswordComponent },
    { path:'home', component:HomeComponent, canActivate : [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }