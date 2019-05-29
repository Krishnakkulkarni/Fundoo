import { Component, OnInit, Output, EventEmitter } from '@angular/core';
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
  add : any;
  @Output() AddEvent = new EventEmitter();
  

  constructor(private notesService: NotesService) { }

  ngOnInit() {
    localStorage.getItem('token');
    this.id = localStorage.getItem("userid")
    
  }

  AddNotes() {
    if (this.title.value.trim() != "" && this.title.value != undefined &&
      this.take_a_note.value.trim() != "" && this.take_a_note != undefined) {
      var notes = {
        UserId: this.id,
        Title: this.title.value,
        Description: this.take_a_note.value,
        Color: this.color
      }
      this.notesService.addNotes(notes).subscribe
        (data => {
          console.log(data)
          this.AddEvent.emit(data)
        },
          err => {
            this.AddEvent.emit({})
            console.log(err)
          }
        )
    }
  }

  SetColor($event: string) {
    this.color = $event
  }

}
