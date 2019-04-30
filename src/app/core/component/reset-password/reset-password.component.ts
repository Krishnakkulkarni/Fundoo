import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Userresettpassword } from '../../Models/user.model';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  userresettpassword: Userresettpassword

  constructor(private userService: UserService, private router: Router, private snackBar: MatSnackBar) { }

  ngOnInit() { this.resetForm(); }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.userresettpassword =
      {
        Email: '',
        Password: '',
        ConfirmPassword: ''
      }
  }

  onSubmit(form: NgForm) {
    if (form.value.Password != form.value.ConfirmPassword) {
      this.resetForm();
      this.snackBar.open('Password and ConfirmPassword Missmatch', 'close', { duration: 2000 });
      return;
    }
    console.log('in reset', form.value);
    this.userService.userresettpassword(form.value).subscribe
      (
        (data: any) => {
          if (data.succeeded == true) {
            alert("reset successfully");
            this.resetForm(form);
          }
        }
      )
    this.resetForm();
  }
}