import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotesService } from '../../services/NotesServices/notes.service';
import { MatSnackBar } from '@angular/material';
import { DataService } from '../../services/DataServices/data.service';

@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.css']
})
export class IconsComponent implements OnInit {
  selectedFile: File = null;

  flag: boolean = false;
  // @Input() more
  @Output() setcolortoNote = new EventEmitter();

  onFileSelected(Event: any, card: any) {
    console.log(Event);
    this.selectedFile = <File>Event.path[0].files[0];
    console.log(this.selectedFile);
    this.Onupload(card)

  }
  constructor(public httpClient: HttpClient, public notesService: NotesService, public SnackBar: MatSnackBar, private service: DataService) { }
  @Input() card: any;

  ngOnInit() {
  }

  Onupload(card) {
    if (card.id != undefined) {
      const formdata = new FormData();
      formdata.append('file', this.selectedFile);
      this.notesService.ImageUpload(formdata, card.id).subscribe
        (data => {
          console.log(data)
          this.service.change({ type: 'image' })
          this.SnackBar.open("Image Uploaded", "close", { duration: 2000 });

        },
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
  Archive(card) {
    card.isArchive = true;
    console.log(card)
    this.notesService.ArchiveNote(card.id, card).subscribe(
      data => {
        console.log(data);
        this.SnackBar.open("Note Archived", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  unarchive(card) {
    card.isArchive = false;
    this.notesService.ArchiveNote(card.id, card).subscribe(
      data => {
        console.log(data);
        this.SnackBar.open("note unarchive", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  TrashNote(card) {
    console.log(card);
    card.isTrash = true;
    this.notesService.Trash(card.id, card).subscribe(
      data => {
        console.log(data);
        this.SnackBar.open("Note Trashed", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }
  Delete(card) {
    console.log(card);
    this.notesService.DeleteNote(card.id, card).subscribe(
      data => { console.log(data); },
      err => { console.log(err); }
    )
  }
}
