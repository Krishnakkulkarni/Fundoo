import { Component, OnInit, Input } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';

@Component({
  selector: 'app-main-notes',
  templateUrl: './main-notes.component.html',
  styleUrls: ['./main-notes.component.css']
})
export class MainNotesComponent implements OnInit {

  public notes = [];
  public cardNotes = [];
  public userId: any;
  public token_Id: any;
  public collNotes = [];
  public num;
  public pinnedNotes = [];

  constructor(private notesService: NotesService, private dataservice: DataService) { }

  /**
   * Main Method
   */
  ngOnInit() {
    this.token_Id = localStorage.getItem('token')
    this.userId = localStorage.getItem('userid')

    this.getAllCard();
    this.dataservice.current.subscribe(data => {
      if (data.type == 'image') {
        this.getAllCard();
      }
    })
  }

  /**
   * To get all cards 
   */
  getAllCard() {
    this.notesService.getNotesById(this.userId).subscribe(
      (data: any) => {
        console.log(data);
        this.cardNotes = data.note.item1;
        for (let i = 0; i < data.note.item1.length; i++) {
          if (data.note.item1[i].pin == true) {
            this.pinnedNotes.push(data.note.item1[i])
          }
        }
        console.log(this.pinnedNotes);
      });
  }

  /**
   * 
   * @param event 
   */
  eventOccur(event) {
    this.getAllCard();
  }

  trashEvent(event) {
    this.getAllCard();

    console.log(event, "D in mainNotes");

    this.getAllCard();
  }

  /**
   * Method to show all notes on dashboard
   * @param $event 
   */
  getnotes($event) {
    this.getAllCard();
    this.pinnedNotes=[];
  }

  labelEvent($event) {
    this.getAllCard();
  }

}
