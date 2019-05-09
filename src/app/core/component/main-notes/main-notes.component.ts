import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-main-notes',
  templateUrl: './main-notes.component.html',
  styleUrls: ['./main-notes.component.css']
})
export class MainNotesComponent implements OnInit {

  notes=[];
  CardNotes=[]
  id: string;

  constructor(private notesService : NotesService) { }

  ngOnInit() {
    this.id = localStorage.getItem("UserID")
    this.notesService.getNotesById(this.id).subscribe(
      (data:any) => {
          this.notes=data
        this.notes.forEach(element => {
          if(element.isArchive == false && element.isTrash == false){
            this.CardNotes.push(element)
            console.log(this.CardNotes,"notes"); 
          }
        });
      }
    ), (err: any) => {
      console.log(err);
    };
  }
}
