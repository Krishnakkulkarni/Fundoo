import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  title = new FormControl('', [Validators.required]);
  take_a_note = new FormControl('', [Validators.required]);
  id: any;
  color: string = "#ffffff";
  constructor(private notesService: NotesService) { }

  ngOnInit() {

    localStorage.getItem('token');
    this.id = localStorage.getItem("UserID")
  }

  AddNotes() {
    var notes = {
      UserId: this.id,
      Title: this.title.value,
      Description: this.take_a_note.value,
      Color: this.color
    }
    if (this.title.value != " " && this.title.value != undefined && 
      this.take_a_note.value != " " && this.take_a_note != undefined) {
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
  SetColor($event: string) {
    this.color = $event
  }
}
