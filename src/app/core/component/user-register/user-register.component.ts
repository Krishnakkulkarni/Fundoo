import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { formArrayNameProvider } from '@angular/Forms/src/directives/reactive_directives/form_group_name';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { User } from '../../Models/user.model';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {
  user: User;
  emailPattern = "^[a-z0-9.%+-]+@[a-z.-]+\.[a-z]{2,4}$";
  constructor(private userService: UserService, private router: Router, public snackbar: MatSnackBar) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.user =
      {
        UserName: '',
        Password: '',
        Email: '',
        FirstName: '',
        LastName: ''
      }
  }

  onSubmit(form: NgForm) {
    console.log('in register', form.value);
    this.userService.registerUser(form.value).subscribe
      (
        (data: any) => {
          console.log(data);

          if (data) {
            this.router.navigateByUrl('user/login');
            this.snackbar.open("register successful", "close", { duration: 1500 });
            this.resetForm(form);
          }
        }
      );
  }
}
