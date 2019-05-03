import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { FormControl, Validators } from '@angular/forms';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  title = new FormControl('', [Validators.required]);
  take_a_note = new FormControl('', [Validators.required]);
  token_id: any;
  id: any;
  constructor(private notesService: NotesService) { }

  ngOnInit() {

    var token = localStorage.getItem('token');
    var jwt_token = jwt_decode(token);
    localStorage.setItem("UserID", jwt_token.UserID)
    this.id = localStorage.getItem("UserID")
  }

  AddNotes() {
    var notes = {
      UserId: this.id,
      Title: this.title.value,
      Description: this.take_a_note.value,
    }
    if (this.title.value != "" && this.take_a_note.value != "") {
      this.notesService.addNotes(notes).subscribe
        (data => {
          console.log(data)
        },
          err => {
            console.log(err)
          }
        )
    }
  }
}
