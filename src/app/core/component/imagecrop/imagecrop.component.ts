import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material'
import { UserService } from '../../services/user.service';
import { HttpService } from '../../services/HttpServices/http.service';
import { DataService } from '../../services/DataServices/data.service';
import { DialogData } from '../home/home.component';

@Component({
  selector: 'app-imagecrop',
  templateUrl: './imagecrop.component.html',
  styleUrls: ['./imagecrop.component.css']
})
export class ImagecropComponent implements OnInit {

  imagecroped: any;
  userId: string;
  imageurl: any

  constructor(public dialogRef: MatDialogRef<ImagecropComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: DialogData, public userService: UserService, public httpService: HttpService,
    public dataService: DataService) 
    {
       console.log(dialogData, "in image crop dialog") 
    }

  ngOnInit() {
    this.userId=localStorage.getItem('userid')
  }

  private token = localStorage.getItem('token')

  imageCropped($event) {
    this.imagecroped = $event.file;
    console.log(this.imagecroped, "image crop")
  }

  cancel() {
    this.dialogRef.close();
  }

  setprofile() {
      let formdata = new FormData();
      formdata.append('file', this.imagecroped);
      console.log(formdata,this.userId, "form data");

      this.userService.profilePicture(this.userId,formdata).subscribe(data => {
        console.log(data, "resp when setting img")
        
        localStorage.setItem('result', data['result']);
        this.imageurl=localStorage.getItem('result')
        console.log('imageurl',this.imageurl);
        
        this.dialogRef.close();
        this.dataService.changeImage(true);
      }, err => {
        console.log(err, "err")
      });
    }
}
