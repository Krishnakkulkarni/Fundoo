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
    if(localStorage.getItem('token')!= null)
    this.router.navigateByUrl('home');
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
        localStorage.setItem('token',data.token);
        this.router.navigateByUrl('home');
        alert("successfully login");
      },
      err=>
      {
        console.log(err); 
        this.isLoginError = true;
      }
    );
  }
}
