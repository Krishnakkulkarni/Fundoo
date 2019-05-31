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
  noteCards=[];
  SearchCard:any;
  searchText:string=''
  
  constructor(public dataServices: DataService, public notesService:NotesService) { }

  /**
   * 
   */
  ngOnInit() {
    this.userId = localStorage.getItem("userid")
    this.dataServices.currentSearchmsg.subscribe(response => {
      console.log('message in search',typeof response);
      
      this.searchText=response;
      this.getallCards();
  })
  }

  /**
   * Method to get cards
   */
  getallCards(){
    this.notesService.getNotesById(this.userId).subscribe(data =>{
      this.noteCards=[];
      console.log(data);
      this.SearchCard=data;
    },err=>{
      console.log(err);
      
    })
  }
}
