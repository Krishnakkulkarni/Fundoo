import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { UserLogin, User } from '../../Models/user.model';
import { AuthService, FacebookLoginProvider } from 'angular-6-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: UserLogin;
  emailPattern = "^[a-z0-9.%+-]+@[a-z.-]+\.[a-z]{2,4}$";
  userName: any;

  constructor(private userService: UserService, private router: Router, public snackbar: MatSnackBar,
    private authService: AuthService) { }

  /**
   * Social login to popup the fb dailog
   * @param socialPlatform 
   */
  public socialSignIn(socialPlatform: string) {
    let socialPlatformProvider: string;
    if (socialPlatform == "facebook") {
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    }
    this.authService.signIn(socialPlatformProvider).then(
      (userData) => {
        console.log(socialPlatform + " sign in data : ", userData);
        console.log(userData.email, "email from fb")
        this.userName = userData.email;
        /**
         * Calling the api for social login
         */
        console.log(this.userName, "check");

        this.userService.fbLogin(this.userName).subscribe(
          (data: any) => {
            console.log(data);

            // localStorage.setItem('token', data.result.token);
          },
          (error: any) => { console.log(error); }
        )
      }
    )
  }

  /**
   * Main Method
   */
  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('home');
    this.resetForm();

  }

  /**
   * Method to hold the information in form
   * @param form 
   */
  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.user =
      {
        UserName: '',
        Password: '',
      }
  }

  /**
   * Submit method to pass information for user to login
   * @param form 
   */
  onSubmit(form: NgForm) {
    if (form.value.UserName == '' && form.value.Password == '') {
      this.snackbar.open("Invalid UserName and Password", "close", { duration: 2000 });
    }
    else {
      this.userService.login(form.value).subscribe
        (
          (data: any) => {
            this.userService.imageurl(data.result.userid).subscribe
              (result => { console.log(result); localStorage.setItem('result', result) },
                err => { console.log(err); }
              )
            localStorage.setItem('token', data.result.token);
            localStorage.setItem('userid', data.result.userid);
            localStorage.setItem('username', data.result.userName);
            this.router.navigateByUrl('home');
            this.snackbar.open("login successful", "close", { duration: 2000 });
          },
          error => {
            console.log(error);
            this.snackbar.open("Entered wrong username Or password", "close", { duration: 2500 })
          }
        );
    }
  }
}
