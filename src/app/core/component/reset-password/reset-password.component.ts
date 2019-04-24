import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Userresettpassword } from '../../Models/user.model';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  userresettpassword : Userresettpassword
  
  constructor(private userService : UserService,private router:Router) { }

  ngOnInit() {
    this.resetForm();
  }
  
  resetForm(form?:NgForm)
  {
   if(form !=null)
  form.reset();
  this.userresettpassword ={
    Email:'',
    Password:'',
    ConfirmPassword : ''
  }
  }

  onSubmit(form:NgForm)
{
    console.log('in reset',form.value);
    this.userService.userresettpassword(form.value)
    .subscribe
    ((data:any)=>
  { 
    console.log(data);

    if(data.succeeded == true)
    {
      console.log('successfull');
      alert("reset successfully");
      //this.router.navigateByUrl('userresettpassword/resetPassword');
      this.resetForm(form);
    }
  })
}
}