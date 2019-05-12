import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-main-notes',
  templateUrl: './main-notes.component.html',
  styleUrls: ['./main-notes.component.css']
})
export class MainNotesComponent implements OnInit {

  notes = [];
  CardNotes = []
  userid:any;
  Token_Id : any;

  constructor(private notesService: NotesService, private dataservice: DataService) { }

  ngOnInit() {
    this.Token_Id = localStorage.getItem('token')
    var jwt_token = jwt_decode(this.Token_Id);
    localStorage.setItem("UserID",jwt_token.UserID)
    this.userid=localStorage.getItem("UserID")

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
    console.log(this.userid,"show");
    
    this.notesService.getNotesById(this.userid).subscribe(
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
