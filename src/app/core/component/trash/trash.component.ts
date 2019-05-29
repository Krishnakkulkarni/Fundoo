import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-trash',
  templateUrl: './trash.component.html',
  styleUrls: ['./trash.component.css']
})
export class TrashComponent implements OnInit {

  notes = [];
  CardNotes = []
  id: string;
  more="isTrash"

  constructor(public notesService: NotesService) { }

  ngOnInit() {
    this.id = localStorage.getItem("userid")
    this.notesService.ViewInTrash(this.id).subscribe(
      (data: any) => {
        this.notes = data['result']
        this.notes.forEach(element => {
          if (element.isTrash == true) {
            this.CardNotes.push(element)
            console.log(this.CardNotes, "notes");
          }
        });
      }
    ), (err: any) => {
      console.log(err);
    };
  }
}
