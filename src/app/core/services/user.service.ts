import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, Userresettpassword, Userforgotpassword } from '../Models/user.model';
import { HttpService } from './HttpServices/http.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  Authentication(userName: any, password: any) {
    throw new Error("Method not implemented.");
  }

  rooturl = environment.rooturl;
  constructor(private http: HttpClient, public httpService: HttpService) { }

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

//   profile(UserId) {
//     return this.httpService.Get('Account/url/"+UserId);
//  }

  profilePicture(data,UserId) {
    return this.httpService.postImage('Account/profile/'+UserId,data);
  }
  imageurl(userid){
    return this.httpService.GetString('Account/url/'+userid)
  }

}

//  profilePicture(path,email)
//   {
// return this.http.post(this.BaseURI+'applicationuser/profilepicture/'+email,path
//  ,{responseType:'text'});
//   }