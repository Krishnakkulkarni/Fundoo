import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NotesService } from '../../services/NotesServices/notes.service';
import { Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-collaboration',
  templateUrl: './collaboration.component.html',
  styleUrls: ['./collaboration.component.css']
})
export class CollaborationComponent implements OnInit {

  public firstName: string;
  public lastName: string;
  public userName: string;
  public receiverEmail: any;
  public userId: any;
  public noteid: any;

  constructor(public dialogRef: MatDialogRef<CollaborationComponent>, private notesService: NotesService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  receiver_email = new FormControl('', Validators.email);

  /**
   * Main Method 
   */
  ngOnInit() {
    this.firstName = localStorage.getItem("firstName");
    this.lastName = localStorage.getItem("lastName");
    this.userName = localStorage.getItem("username");
    this.userId = localStorage.getItem('userid');
    this.noteid = localStorage.getItem('noteId');
  }

  /**
   * Method to add collaborator
   */
  AddCollaborator() {
    var values = {
      "UserId": this.userId,
      "noteId": this.noteid,
      "senderEmail": this.userName,
      "receiverEmail": this.receiver_email.value
    }
    this.notesService.addcollaborator(values).subscribe(result =>
      console.log(result)
    )
  }
  removeColl() {
    this.notesService.removeCollaborator(this.noteid).subscribe(data => { console.log(); }
    )
  }
}
