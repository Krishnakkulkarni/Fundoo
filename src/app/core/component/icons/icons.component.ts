import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.css']
})
export class IconsComponent implements OnInit {
  selectedFile: File = null;
  @Output() setcolortoNote = new EventEmitter();

  onFileSelected(Event: any, card: any) {
    console.log(Event);
    this.selectedFile = <File>Event.path[0].files[0];
    console.log(this.selectedFile);
    this.Onupload(card)

  }
  constructor(public httpClient: HttpClient, public notesService: NotesService) { }
  @Input() card: any;

  ngOnInit() {
  }

  Onupload(card) {
    if (card.id != undefined) {
      const formdata = new FormData();
      formdata.append('file', this.selectedFile);
      this.notesService.ImageUpload(formdata, card.id).subscribe
        (data => { console.log(data) },
          err => {
            console.log(err);
          }
        )
    }
  }
  setcolor(color: any, card) {
    if (card == undefined) {
      this.setcolortoNote.emit(color)
    }
    else {
      console.log(card, "card")
      card.color = color;
      this.notesService.updateNotes(card.id, card).subscribe(
        data => { console.log(data, "color update"); },
        err => { console.log(err); }
      )
    }
  }
  DeleteNote(card) {
    console.log(card);
      this.notesService.Delete(card.id).subscribe(
        data => { console.log(data); },
        err => { console.log(err); }
      )
    
  }
}
