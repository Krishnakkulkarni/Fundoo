import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Userforgotpassword } from '../../Models/user.model';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  userforgotpassword: Userforgotpassword

  constructor(private userService: UserService, private router: Router, public snackbar: MatSnackBar) { }

  /**
   * 
   */
  ngOnInit() {
    this.resetForm();
  }

  /**
   * 
   * @param form 
   */
  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.userforgotpassword = { UserName: '' }
  }
  /**
   * 
   * @param form 
   */
  onSubmit(form: NgForm) {
    console.log('in forgot', form.value);
    this.userService.forgotPassword(form.value)
      .subscribe
      ((data: any) => {
        console.log(data);
        this.snackbar.open("Email sent successful", "close", { duration: 2000 });
        this.resetForm(form);
      })
  }
}