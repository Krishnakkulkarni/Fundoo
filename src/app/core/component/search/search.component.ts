import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/DataServices/data.service';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  userId: string;
  searchCard: any;
  searchText: string = ''

  constructor(public dataServices: DataService, public notesService: NotesService) { }

  /**
   * Main Method
   */
  ngOnInit() {
    this.userId = localStorage.getItem("userid")
    this.dataServices.currentSearchmsg.subscribe(response => {
      this.searchText = response;
      this.getallCards();
    })
  }

  /**
   * Method to get cards
   */
  getallCards() {
    this.notesService.getNotesById(this.userId).subscribe(data => { this.searchCard = data; },
      err => { console.log(err); }
    )
  }
}
