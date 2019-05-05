import { Component, OnInit, Input } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-displaynotes',
  templateUrl: './displaynotes.component.html',
  styleUrls: ['./displaynotes.component.css']
})
export class DisplaynotesComponent implements OnInit {

  constructor(private notesService: NotesService) { }

  @Input() cards: any;

  ngOnInit() {
    
  }
}