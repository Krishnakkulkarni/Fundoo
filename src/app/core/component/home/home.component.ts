import { MediaMatcher } from '@angular/cdk/layout';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { DataService } from '../../services/DataServices/data.service';

import { ImagecropComponent } from '../imagecrop/imagecrop.component';
import { UserService } from '../../services/user.service';
import { EditLabelsComponent } from '../edit-labels/edit-labels.component';
import { NotesService } from '../../services/NotesServices/notes.service';
import { LabelService } from '../../services/LabelServices/label.service';

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
  userId: string;
  FirstName: string;
  LastName: string;
  userName: string;
  UserName: string;
  selectedFile: File;
  value: any;
  photo: string;

  HeaderName = "Fundoo"

  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;

  constructor(private router: Router, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher,
    public snackBar: MatSnackBar, public userService: UserService, public notesService: NotesService,
    public dataService: DataService, public labelService: LabelService, public dialog: MatDialog) {

    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

    this.payLoad = localStorage.getItem('token');
  }

  /**
   * Main Method
   */
  ngOnInit() {

    this.userId = localStorage.getItem("userid");
    this.photo = localStorage.getItem("result");

    this.UserName = localStorage.getItem("username");
    this.FirstName = localStorage.getItem("firstName");
    this.LastName = localStorage.getItem("lastName");

    this.dataService.currentMsg.subscribe(message => this.message = message);
    this.userName = localStorage.getItem("UserName")

    this.getLabels();
  }

  /**
   * Method to navigate to login page
   */
  ToHome() {
    this.router.navigateByUrl('user/login');
  }

  /**
   * Method to search
   */
  lookfor() {
    this.dataService.changeSearchMsg(this.value)
  }

  /**
   * 
   */
  goSearch() {
    this.router.navigate(['/home/search'])
  }

  /**
   * Method to logout from app
   */
  onLogout() {
    localStorage.removeItem('token');
    localStorage.removeItem('result');
    localStorage.removeItem('username');
    localStorage.removeItem('lastName');
    localStorage.removeItem('firstName');
    localStorage.removeItem('userid');
    localStorage.removeItem('noteId');
    localStorage.removeItem('profileUrl');
    this.router.navigate(['user/login']);
    this.snackBar.open("logout successful", "close", { duration: 1500 });
  }

  /**
   * refresh the page
   */
  refresh() {
    location.reload()
  }

  /**
   * Method to navigate to notes page
   */
  Note() {
    this.router.navigate(['home/MainNotes'])
  }

  /**
   * Method to set the view(grid or list)
   */
  ReverseFlag() {
    this.flag = !this.flag
    this.dataService.changeView(this.flag)
  }

  imageFile = null;

  /**
   * Method to uploade Profile
   * @param event 
   */
  fileUpload(event: { path: { files: any[]; }[]; }) {
    console.log(event, this.userId, "......")
    console.log(event.path[0].files[0], "uploaded file ")
    this.imageFile = event.path[0].files[0]
    const uploadImage = new FormData();
    uploadImage.append('file', this.imageFile, this.imageFile.name);
    this.ChangePic(event)
  }

  /**
   * Method to change the profile picture
   * @param data 
   */
  ChangePic(data: any) {
    {
      try {
        const dialogRef = this.dialog.open(ImagecropComponent,
          { data: data, width: '600px' });
        dialogRef.afterClosed().subscribe(result => {
          this.dataService.currentImage.subscribe(response => { })
        })
      }
      catch (err) { console.log('error occurs ', err) }
    }
  }

  /**
   * Method to navigate reminder page
   */
  Reminder() {
    this.router.navigate(['home/Reminder'])
  }

  /**
   * Method to add labels
   */
  EditLables(): void {
    const dialogConfig = new MatDialogConfig();
    let dialogRef = this.dialog.open(EditLabelsComponent,
      { data: this.notesLabel }
    );

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
    // )
  }


  /**
   * Method to get labels
   */
  getLabels() {
    this.labelService.getlabels(this.userId).subscribe(responselabels => {
      this.notesLabel = responselabels['result'];
    }, err => {
      console.log(err);
    })
  }

  /**
   * 
   * @param $event 
   */
  add($event: any) {
    this.getLabels();
  }

  /**
   * Method to navigate archive page
   */
  Archive() {
    this.router.navigate(['home/Archive'])
  }

  /**
   * Method to navigate trash page
   */
  Trash() {
    this.router.navigate(['home/Trash'])
  }
}