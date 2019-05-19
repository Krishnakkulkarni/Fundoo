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
  userid: string;


  constructor(public dialogRef: MatDialogRef<ImagecropComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: DialogData, public userService: UserService, public httpService: HttpService,
    public dataService: DataService) 
    { console.log(dialogData, "in image crop dialog") }

  ngOnInit() {
    this.userid=localStorage.getItem('UserID')
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
      const formdata = new FormData();
      formdata.append('file', this.imagecroped);
      console.log(formdata,this.userid, "form data");

      this.userService.uploadImage(formdata,this.userid).subscribe(data => {
        console.log(data, "resp when setting img")
        localStorage.setItem('imageurl', data['status'].imageUrl);
        this.dialogRef.close();
        this.dataService.changeImage(true);
      }, err => {
        console.log(err, "err")
      });
    }
  
}
