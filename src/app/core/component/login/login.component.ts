import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from '../../services/user.service';
import { NgForm, Validators } from '@angular/forms';
import { User } from '../../Models/user.model';
import { MatSnackBar } from '@angular/material';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  message = '';

  isLoginError: boolean = false;
  user: { UserName: string; Password: string; };

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
    if (form.value.UserName == '' && form.value.Password == '') {
      this.message = 'Required Fields?';
    }
    else {
      this.userService.login(form.value).subscribe
        (
          (data: any) => {
            localStorage.setItem('token', data.token);
            this.router.navigateByUrl('home');
            this.snackbar.open("login successful", "close", { duration: 1500 });
          },
          err => {
            console.log(err);
            this.isLoginError = true;
          }
        );
    }
  }
}
