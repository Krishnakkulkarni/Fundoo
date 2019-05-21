import { MediaMatcher } from '@angular/cdk/layout';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { DataService } from '../../services/DataServices/data.service';
import { environment } from 'src/environments/environment';
import { ImagecropComponent } from '../imagecrop/imagecrop.component';

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

  profilePic: boolean;
  imageprofile: string;
  userid : string;

  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;

  HeaderName = "Fundoo"

  constructor(private router: Router, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, public snackBar: MatSnackBar
    , public dataService: DataService, public dialog: MatDialog) {

    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

  }
  ngOnInit() {
    this.userid=localStorage.getItem("UserID");
    
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


  imageFile = null;
  public imageNew = localStorage.getItem('imageurl')
  img = environment.profileUrl + this.imageNew;


  fileUpload($event) {
    console.log($event,this.userid ,"......")
    console.log($event.path[0].files[0], "uploaded file ")
    this.imageFile = $event.path[0].files[0]
    const uploadImage = new FormData();
    uploadImage.append('file', this.imageFile, this.imageFile.name);
    this.ChangePic($event)
    
  }

  ChangePic(data: any) {
    {
      try {
        const dialogRef = this.dialog.open(ImagecropComponent, {
          data: data,
          width: '600px'
        });
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

  EditLables() {
    this.router.navigate(['home/EditLables'])
  }

  Archive() {
    this.router.navigate(['home/Archive'])
  }

  Trash() {
    this.router.navigate(['home/Trash'])
  }
}