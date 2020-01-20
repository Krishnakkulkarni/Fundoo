import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';

// import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-trash',
  templateUrl: './trash.component.html',
  styleUrls: ['./trash.component.css']
})
export class TrashComponent implements OnInit {

  cardNotes = []
  id: string;
  more = "isTrash"
  constructor(public notesService: NotesService, public dataService: DataService) { }

  /**
   * 
   */
  ngOnInit() {
    this.id = localStorage.getItem("userid")
    // this.dataService.currentNum.subscribe(num=>{
    //   console.log(num);
    //   this.trashNotes();
    // })
    this.trashNotes();
  }

  trashNotes() {
    this.notesService.ViewInTrash(this.id).subscribe(
      (data: any) => {
        console.log(data);
        this.cardNotes=[]
        data['result'].forEach(element => {
          if (element.isTrash == true) {
            this.cardNotes.push(element)
          }
        });
        console.log(this.cardNotes, "Trashed notes");
      }
    ), (err: any) => {
      console.log(err);
    };
  }

  eventOccur($event) {
    this.trashNotes();
    // this.cardNotes = [];
  }
}
