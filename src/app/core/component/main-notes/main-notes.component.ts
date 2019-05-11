import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';
@Component({
  selector: 'app-main-notes',
  templateUrl: './main-notes.component.html',
  styleUrls: ['./main-notes.component.css']
})
export class MainNotesComponent implements OnInit {

  notes = [];
  CardNotes = []
  id: string;

  constructor(private notesService: NotesService, private dataservice: DataService) { }

  ngOnInit() {
    this.id = localStorage.getItem("UserID")
    this.getAllCard();
    this.dataservice.current.subscribe(data => {
      console.log('data ', data);
      if (data.type == 'image') {
        this.getAllCard();
      }

    })
  }
  getAllCard() {
    this.CardNotes = [];
    this.notesService.getNotesById(this.id).subscribe(
      (data: any) => {
        this.notes = data
        this.notes.forEach(element => {
          if (element.isArchive == false && element.isTrash == false) {
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
