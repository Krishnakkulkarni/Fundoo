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
  Email: string;
  userId: any;
  notesid: any;
  receiveremail: void;

  constructor(public dialogRef: MatDialogRef<CollaborationComponent>, private notes: NotesService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ReceiverEmail = new FormControl('', Validators.email);
  /**
   * 
   */
  ngOnInit() {
    this.FirstName = localStorage.getItem("FirstName");
    this.LastName = localStorage.getItem("LastName");
    this.Email = localStorage.getItem("Email");
    this.userId = localStorage.getItem('userid');

  }
  /**
   * 
   */
  add() {
    var values = {
      "UserId": this.userId,
      "noteId": localStorage.getItem('noteId'),
      "senderEmail": this.Email,
      "receiverEmail": this.ReceiverEmail.value
    }
    // this.receiveremail=localStorage.setItem('receiverEmail',this.ReceiverEmail.value)
    // this.notes.addcollaborator(values).subscribe(result=>
    //   console.log(values)    
    //   )
    // this.getcollab(); 
    this.dialogRef.close(values);
  }
  // getcollab(){
  //   this.notes.getCollaboratorNote(this.data).subscribe(result=>
  //     console.log(this.data)          
  //     )  
  // }
}
