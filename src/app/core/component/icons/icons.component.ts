import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.css']
})
export class IconsComponent implements OnInit {
  selectedFile: File = null;

  onFileSelected(Event: any, card) {
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
        (data => { console.log(data, "image response") },
          err => {
            console.log(err);
          }
        )
    }
  }
}
