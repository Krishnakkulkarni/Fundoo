import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User, Userresettpassword, Userforgotpassword } from '../Models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  Authentication(userName: any, password: any) {
    throw new Error("Method not implemented.");
  }

  rooturl = environment.rooturl;
  constructor(private http: HttpClient) { }

  registerUser(user : User){
    console.log(user);
      const body : User = {
        UserName: user.UserName,
        Password:user.Password,
        Email:user.Email,
        FirstName:user.FirstName,
        LastName:user.LastName
      }
      return this.http.post(this.rooturl+ 'ApplicationUser/register',body);
  }
  login(formdata: any)
  {
    console.log('in service');
    
    return this.http.post(this.rooturl+ 'ApplicationUser/login',formdata);
  }
  
  forgotPassword(userforgotpassword: Userforgotpassword)
  {
    console.log(userforgotpassword);
    const formdata  =
    {
      Email : userforgotpassword.Email
    }
    console.log('data in service0',formdata);
    
    return this.http.post(this.rooturl+ 'ApplicationUser/forgotPassword',formdata);
  }

  userresettpassword(userresettpassword : Userresettpassword)
  {
    console.log(userresettpassword);
    const formdata : Userresettpassword =
    {
      Email : userresettpassword.Email,
      Password : userresettpassword.Password,
      ConfirmPassword : userresettpassword.ConfirmPassword
    }
     return this.http.post(this.rooturl+ 'ApplicationUser/resetPassword', formdata);
  }
}