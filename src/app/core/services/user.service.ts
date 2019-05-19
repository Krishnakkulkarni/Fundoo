import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
    return this.httpService.post('Account/register', user);
  }

  login(data) {
    return this.httpService.post('Account/login', data);
  }

  forgotPassword(userforgotpassword: Userforgotpassword) {
    console.log(userforgotpassword);
    return this.httpService.post('Account/forgotPassword', userforgotpassword);
  }

  userresettpassword(userresettpassword: Userresettpassword) {
    console.log(userresettpassword);
    return this.httpService.post('Account/resetPassword', userresettpassword);
  }

  uploadImage(data,userid){
    return this.httpService.post('Account/profile/'+userid,data)
  }
}