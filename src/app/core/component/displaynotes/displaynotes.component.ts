import { Component, OnInit, Input } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';

@Component({
  selector: 'app-displaynotes',
  templateUrl: './displaynotes.component.html',
  styleUrls: ['./displaynotes.component.css']
})
export class DisplaynotesComponent implements OnInit {
  grid : boolean= true

  constructor(private notesService: NotesService,public dataService: DataService) { }

  @Input() cards: any;

  ngOnInit() {
    this.dataService.currentMessage.subscribe(data=>{console.log(data)
      this.grid=data
    });
  }
}