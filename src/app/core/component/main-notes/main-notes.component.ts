import { Component, OnInit, Input } from '@angular/core';
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
  userId: any;
  Token_Id: any;
  CollNotes = [];
  firstId="";

  constructor(private notesService: NotesService, private dataservice: DataService) { }

  /**
   * Main Method
   */
  ngOnInit() {
    this.Token_Id = localStorage.getItem('token')
    this.userId = localStorage.getItem('userid')

    this.getAllCard();
    this.dataservice.current.subscribe(data => {
      console.log('data ', data);
      if (data.type == 'image') {
        this.getAllCard();
      }
    })
  }

  /**
   * To get all cards 
   */
  getAllCard() {
    this.CardNotes = [];
    this.notesService.getNotesById(this.userId).subscribe(
      (data: any) => {
        console.log(data);
        
        this.CardNotes = data.note.item1;
        this.CollNotes = data.note.item2;
        
        
        // for (let i = 0; i < data.note.item1.length; i++) {
        //   if (data.note.item1[i].id!=this.firstId) {
        //     this.firstId=data.note.item1[i].id
        //     this.CardNotes.push(data.note.item1[i])
        //   }
        // }
        console.log(this.CardNotes, "notes");
        console.log(this.CollNotes,"Collaborator");
        
      });
  }

  /**
   * 
   * @param event 
   */
  eventOccur(event) {
    this.getAllCard();
  }

  /**
   * Method to show all notes on dashboard
   * @param $event 
   */
  getnotes($event) {
    this.getAllCard();
  }

  labelEvent($event){
    this.getAllCard();
  }

}
