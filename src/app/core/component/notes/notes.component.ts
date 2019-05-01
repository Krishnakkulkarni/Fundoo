import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  token_id: any;
  title: any;
  take_a_note: any;
  AferCloseEvent: any;

  constructor(private notesService:NotesService) { }

  ngOnInit() {
    this.token_id=localStorage.getItem('token')
  }

  AddNotes() {
    var notes ={
    UserId :this.token_id,
    Title:this.title.value,
    Description:this.take_a_note.value,
    }
    if(this.title.value !="" && this.take_a_note.value!=""){
    this.notesService.addNotes(notes).subscribe
    (
      (data: any) => {
        this.AferCloseEvent.emit(); 
    });
  }
}
}
