import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from '../../services/user.service';
import { NgForm, Validators } from '@angular/forms';
import { User } from '../../Models/user.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit
 {
  isLoginError: boolean = false;
  user: { UserName: string; Password: string; };

  constructor(private userService : UserService,private router : Router) { }

  ngOnInit()
  {
    this.resetForm();

  }

  resetForm(form?:NgForm)
  {
   if(form !=null)
    form.reset();
    this.user =
    {
      UserName:'',
      Password:''
    }
  }
  onSubmit(form:NgForm)
  {
    this.userService.login(form.value).subscribe
    (
      (data:any)=>
      {
        console.log(data);
        alert("successfully login");
        this.router.navigateByUrl('home');
      },
      err=>
      {
        console.log(err); 
        this.isLoginError = true;
      }
    );
  }
}
