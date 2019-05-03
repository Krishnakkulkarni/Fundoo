import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-displaynotes',
  templateUrl: './displaynotes.component.html',
  styleUrls: ['./displaynotes.component.css']
})
export class DisplaynotesComponent implements OnInit {
  
  notes: any;
  id: string;

  constructor(private notesService: NotesService) { }

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