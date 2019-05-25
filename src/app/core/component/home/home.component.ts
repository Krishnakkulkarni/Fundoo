import { MediaMatcher } from '@angular/cdk/layout';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { DataService } from '../../services/DataServices/data.service';

import { ImagecropComponent } from '../imagecrop/imagecrop.component';
import { UserService } from '../../services/user.service';
import { environment } from '../../../../environments/environment';
import { EditLabelsComponent } from '../edit-labels/edit-labels.component';
import { NotesService } from '../../services/NotesServices/notes.service';
import * as jwt_decode from "jwt-decode";

export interface DialogData {
  data: any
}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  flag: boolean = true;

  message: boolean;
  profilePic: boolean;
  imageprofile: string;

  payLoad: any;
  notesLabel: any;

  token: string;
  userid: string;
  FirstName: string;
  userName: string;
  UserName: string;
  selectedFile: File;
  value;
  photo;

  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;

  HeaderName = "Fundoo"

  constructor(private router: Router, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher,
    public snackBar: MatSnackBar, public userService: UserService, public notesService: NotesService,
    public dataService: DataService, public dialog: MatDialog) {

    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

    this.token = localStorage.getItem('token');
    this.payLoad = jwt_decode(this.token, "decode");
  }

  ngOnInit() {
    this.userid = localStorage.getItem("UserID");
    this.photo = localStorage.getItem('imageUrl');
    // localStorage.setItem('result',this.userid)
    this.UserName = localStorage.getItem("UserName");
    this.FirstName = localStorage.getItem("FirstName");
    this.dataService.currentMsg.subscribe(message => this.message = message);
    this.userName = localStorage.getItem("UserName")
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['user/login']);
    this.snackBar.open("logout successful", "close", { duration: 1500 });
  }

  refresh() {
    location.reload()
  }

  Note() {
    this.router.navigate(['home/MainNotes'])
  }

  ReverseFlag() {
    this.flag = !this.flag
    this.dataService.changeView(this.flag)
  }

  // onFileChanged(event) {
  //   this.selectedFile = event.path[0].files[0];
  //   let uploadData = new FormData();
  //   uploadData.append('file', this.selectedFile, 'file');
  //   console.log(uploadData);

  //   this.userService.profilePicture(uploadData, this.userName).subscribe(data => {
  //     // let obj = JSON.parse(data)
  //     // localStorage.setItem('profile', obj.result)
  //   }, err => {
  //     console.log(err);
  //   }
  //   )
  // }

  imageFile = null;
  public imageNew = localStorage.getItem('result');
  img = environment.profileUrl + this.imageNew;


  fileUpload(event) {
    console.log(event, this.userid, "......")
    console.log(event.path[0].files[0], "uploaded file ")
    this.imageFile = event.path[0].files[0]
    const uploadImage = new FormData();
    uploadImage.append('file', this.imageFile, this.imageFile.name);
    this.ChangePic(event)
  }

  ChangePic(data: any) {
    {
      try {
        const dialogRef = this.dialog.open(ImagecropComponent,
          { data: data, width: '600px' });
        dialogRef.afterClosed().subscribe(result => {
          this.dataService.currentImage.subscribe(response => {
          }
          )

          this.imageprofile = localStorage.getItem('imageurl')
          this.img = environment.profileUrl + this.imageprofile;

        })
      } catch (err) {
        console.log('error occurs ', err)
      }
    }
  }


  Reminder() {
    this.router.navigate(['home/Reminder'])
  }

  EditLables(): void {
    const dialogConfig = new MatDialogConfig();
    let dialogRef = this.dialog.open(EditLabelsComponent,

    //   { data: this.notesLabel, width: '600px' });

    // dialogRef.afterClosed().subscribe(result => {
    //   console.log(result.result, "dash");
    //   if (result.label != '' && result.label != null) {
    //     this.notesService.AddNotesLabels(result).subscribe((data: any) => {
    //       console.log(data)

    //     }, err => {
    //       console.log(err);

    //     });
    //   }
    // }
    )
  }
  getLabels() {
    this.notesService.getlabels(this.payLoad.UserID).subscribe(responselabels => {
      this.notesLabel = responselabels['result'];

    }, err => {
      console.log(err);
    })
  }
  add($event) {
    this.getLabels();
  }

  Archive() {
    this.router.navigate(['home/Archive'])
  }

  Trash() {
    this.router.navigate(['home/Trash'])
  }
}