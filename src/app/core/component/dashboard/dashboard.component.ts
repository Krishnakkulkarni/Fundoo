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
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public flag: boolean = true;

  public message: boolean;
  public profilePic: boolean;
  public imageprofile: string;

  public payLoad: any;
  public notesLabel: any;

  public token: string;
  public userId: string;
  public first_name: string;
  public last_name: string;
  public userName: string;
  public UserName: string;
  public selectedFile: File;
  public value: any;
  public photo: string;

  public headerName = "Fundoo";
  public imageFile = null;

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
    this.photo = localStorage.getItem("profileUrl");
    this.UserName = localStorage.getItem("username");
    this.first_name = localStorage.getItem("firstName");
    this.last_name = localStorage.getItem("lastName");
    this.dataService.currentMsg.subscribe(message => this.message = message);
    this.getLabels();
  }

  /**
   * Method to navigate to login page
   */
  dashboard() {
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
    this.router.navigate(['/dashboard/search'])
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
    localStorage.removeItem('receiverEmail');
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
  getNotes() {
    this.router.navigate(['dashboard/mainNotes'])
  }

  /**
   * Method to set the view(grid or list)
   */
  reverseFlag() {
    this.flag = !this.flag
    this.dataService.changeView(this.flag)
  }

  /**
   * Method to uploade Profile
   * @param event 
   */
  fileUpload(event: { path: { files: any[]; }[]; }) {
    this.imageFile = event.path[0].files[0]
    const uploadImage = new FormData();
    uploadImage.append('file', this.imageFile, this.imageFile.name);
    this.changePic(event)
  }

  /**
   * Method to change the profile picture
   * @param data 
   */
  changePic(data: any) {
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
  reminder() {
    this.router.navigate(['dashboard/Reminder'])
  }

  /**
   * Method to add labels
   */
  editLables(): void {
    const dialogConfig = new MatDialogConfig();
    let dialogRef = this.dialog.open(EditLabelsComponent,
      { data: this.notesLabel }
    );
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

  labelEve($event){
    this.getLabels();
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
  archive() {
    this.router.navigate(['dashboard/Archive'])
  }

  /**
   * Method to navigate trash page
   */
  trash() {
    this.router.navigate(['dashboard/Trash'])
  }
}