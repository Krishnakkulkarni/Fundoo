import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { UserLogin } from '../../Models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: UserLogin;
  emailPattern = "^[a-z0-9.%+-]+@[a-z.-]+\.[a-z]{2,4}$";
  
  constructor(private userService: UserService, private router: Router, public snackbar: MatSnackBar) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('home');
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.user =
      {
        UserName: '',
        Password: '',
      }
  }
  onSubmit(form: NgForm) {
    if (form.value.UserName == ' ' && form.value.Password == ' ') {
    }
    else {
      this.userService.login(form.value).subscribe
        (
          (data: any) => {
            localStorage.setItem('token', data.result);
            this.router.navigateByUrl('home');
            this.snackbar.open("login successful", "close", { duration: 2000 });
          },
          err => { console.log(err); }
        );
    }
  }
}