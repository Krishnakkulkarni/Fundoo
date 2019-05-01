import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User, Userresettpassword, Userforgotpassword } from '../Models/user.model';
import { environment } from 'src/environments/environment';
import { HttpService } from './HttpServices/http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  Authentication(userName: any, password: any) {
    throw new Error("Method not implemented.");
  }

  rooturl = environment.rooturl;
  constructor(private http: HttpClient, public httpService:HttpService) { }

  registerUser(user: User) {
    return this.httpService.post('ApplicationUser/register', user);
  }

  login(data) {
    return this.httpService.post('ApplicationUser/login', data);
  }

  forgotPassword(userforgotpassword: Userforgotpassword) {
    console.log(userforgotpassword);
    // const formdata =
    // {
    //   Email: userforgotpassword
    // }
    //console.log('data in service', formdata);

    return this.httpService.post('ApplicationUser/forgotPassword', userforgotpassword);
  }

  userresettpassword(userresettpassword: Userresettpassword) {
    console.log(userresettpassword);
    // const formdata: Userresettpassword =
    // {
    //   Email: userresettpassword.Email,
    //   Password: userresettpassword.Password,
    //   ConfirmPassword: userresettpassword.ConfirmPassword
    // }
    return this.httpService.post('ApplicationUser/resetPassword', userresettpassword);
  }
}