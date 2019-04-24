import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Userforgotpassword } from '../../Models/user.model';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  userforgotpassword : Userforgotpassword 
  
  constructor(private userService : UserService,private router:Router) { }

  ngOnInit() {
    this.resetForm();
  }
  
  resetForm(form?:NgForm)
  {
   if(form !=null)
  form.reset();
  this.userforgotpassword ={
    Email :''
  }
  }

  onSubmit(form:NgForm)
{
    console.log('in forgot',form.value);
    this.userService.forgotPassword(form.value)
    .subscribe
    ((data:any)=>
  { 
    console.log(data);
    alert("Email Sent successfully");
    // if(data.succeeded == true)
    // {
    //   console.log('successfull');
    //   alert("Email Sent successfully");
    //   this.router.navigateByUrl('userforgotpassword/forgotpassword');
    //   this.resetForm(form);
    // }
  })
}
}