import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-main-notes',
  templateUrl: './main-notes.component.html',
  styleUrls: ['./main-notes.component.css']
})
export class MainNotesComponent implements OnInit {

  notes: any;
  id: string;

  constructor(private notesService : NotesService) { }

  ngOnInit() {
    this.id = localStorage.getItem("UserID")
    this.notesService.getNotesById(this.id).subscribe(
      data => {
        this.notes = data;
      }
    ), (err: any) => {
      console.log(err);
    };
  }

}
