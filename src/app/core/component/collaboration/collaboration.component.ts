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
  FirstName: string;
  LastName: string;
  UserName: string;
  receiverEmail: any;
  userId: any;
  noteid: any;

  constructor(public dialogRef: MatDialogRef<CollaborationComponent>, private notesService: NotesService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ReceiverEmail = new FormControl('', Validators.email);

  /**
   * Main Method 
   */
  ngOnInit() {
    this.FirstName = localStorage.getItem("firstName");
    this.LastName = localStorage.getItem("lastName");
    this.UserName = localStorage.getItem("username");
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
      "senderEmail": this.UserName,
      "receiverEmail": this.ReceiverEmail.value
    }
    console.log(this.ReceiverEmail.value);

    // localStorage.setItem('receiverEmail', this.ReceiverEmail.value)
    // this.receiverEmail = localStorage.getItem('receiverEmail');
    this.notesService.addcollaborator(values).subscribe(result =>
      console.log("add collaborator", values.noteId)
    )
    // this.getcollab();
    // this.dialogRef.close(values);
  }

  /**
   * Method to get collaborator 
   */
  // getcollab() {
  //   console.log(this.data);
  //   this.notesService.getCollaboratorNote(this.userId).subscribe(result =>
  //     console.log("get collaborator", this.data)
  //   )
  // }
  removeColl() {
    console.log(this.noteid);
    this.notesService.removeCollaborator(this.noteid).subscribe(data => { console.log(); }
    )

  }
}
