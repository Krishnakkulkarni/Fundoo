import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-archive',
  templateUrl: './archive.component.html',
  styleUrls: ['./archive.component.css']
})
export class ArchiveComponent implements OnInit {

  public cardNotes = []
  public id: string;
  public more = 'isArchive'

  constructor(public notesService: NotesService) { }

  /**
   * Main method
   */
  ngOnInit() {
    this.id = localStorage.getItem("userid")
    this.getArchive();
  }

  getArchive() {
    this.notesService.GetArchiveNotes(this.id).subscribe(
      (data: any) => {
        console.log(data);
        data['result'].forEach(element => {
          if (element.isArchive == true && element.isTrash == false) {
            this.cardNotes.push(element)
            console.log(this.cardNotes, "notes");
          }
        });
      }
    ), (err: any) => {
      console.log(err);
    };
  }

  eventOccur($event){
    this.getArchive();
    this.cardNotes = []

  }

}
