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
  CardNotes = [];
  userid: any;
  Token_Id: any;

  constructor(private notesService: NotesService, private dataservice: DataService) { }

  ngOnInit() {
    this.Token_Id = localStorage.getItem('token')
    this.userid = localStorage.getItem("UserID")

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
    this.notesService.getNotesById(this.userid).subscribe(
      (data: any) => {
        this.notes = data.note;

        for (let i = 0; i < this.notes.length; i++) {
          if (this.notes[i].isArchive == false && this.notes[i].isTrash == false) {
            this.CardNotes.push(this.notes[i])
            console.log(this.CardNotes, "notes");
          }
        }
      });
  }
  eventOccur(event) {
    this.getAllCard();
  }
  getnotes($event){
    console.log('event occur');
    
    this.getAllCard();
  }
}
