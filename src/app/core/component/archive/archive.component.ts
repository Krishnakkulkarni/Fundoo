import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-archive',
  templateUrl: './archive.component.html',
  styleUrls: ['./archive.component.css']
})
export class ArchiveComponent implements OnInit {

  notes = [];
  CardNotes = []
  id: string;
  more='isArchive'
  
  constructor(public notesService: NotesService) { }

  ngOnInit() {
    this.id = localStorage.getItem("UserID")
    this.notesService.GetArchiveNotes(this.id).subscribe(
      (data: any) => {
        console.log(data);
        this.notes = data['result']
        this.notes.forEach(element => {
          if (element.isArchive == true && element.isTrash == false) {
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
