import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotesService } from '../../services/NotesServices/notes.service';
import { MatSnackBar, MatDialogConfig, MatDialog } from '@angular/material';
import { DataService } from '../../services/DataServices/data.service';
import { CollaborationComponent } from '../collaboration/collaboration.component';
import { LabelService } from '../../services/LabelServices/label.service';

@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.css']
})
export class IconsComponent implements OnInit {
  [x: string]: any;
  selectedFile: File = null;
  flag: boolean = false;
  label;

  @Input() archivedicon
  @Input() trashed
  @Output() setNote = new EventEmitter();

  trash: boolean = true;
  archive: boolean = true;
  unarchive: boolean = true;
  userId: string;

  onFileSelected(Event: any, card: any) {
    console.log(Event);
    this.selectedFile = <File>Event.path[0].files[0];
    console.log(this.selectedFile);
    this.Onupload(card)

  }
  constructor(public httpClient: HttpClient, public notesService: NotesService, public labelService: LabelService,
    public SnackBar: MatSnackBar, private service: DataService, public dialog: MatDialog) { }
  @Input() card: any;

  ngOnInit() {
    this.userId = localStorage.getItem('userid');
    this.labelService.getlabels(this.userId).subscribe(
      data => {
        this.label = data['result'];
      }
    )
  }

  /**
   * 
   * @param card 
   */
  Onupload(card) {
    if (card.id != undefined) {
      const formdata = new FormData();
      console.log(formdata);

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

  /**
   * 
   * @param color 
   * @param card 
   */
  setcolor(color: any, card) {
    if (card == undefined) {
      this.setNote.emit(color)
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

  /**
   * 
   * @param card 
   */
  Archive(card) {
    card.isArchive = true;
    console.log(card)
    this.notesService.ArchiveNote(card.id, card).subscribe(
      data => {
        console.log(data);
        this.setNote.emit(this.archive)
        this.SnackBar.open("Note Archived", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * 
   * @param card 
   */
  Unarchive(card) {
    card.isArchive = false;
    this.notesService.ArchiveNote(card.id, card).subscribe(
      data => {
        console.log(data);
        this.setNote.emit(this.unarchive)

        this.SnackBar.open("note unarchive", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * 
   * @param card 
   */
  TrashNote(card) {
    console.log(card);
    card.isTrash = true;
    this.notesService.Trash(card.id, card).subscribe(
      data => {
        console.log(data);
        this.setNote.emit(this.TrashNote)
        this.SnackBar.open("Note Trashed", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * 
   * @param card 
   */
  Restore(card) {
    console.log(card);
    card.isTrash = false;
    this.notesService.Trash(card.id, card).subscribe(
      data => {
        console.log(data);
        this.SnackBar.open("Note restored", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * 
   * @param labels
   */
  checkList(label) {
    console.log(label.id);
    this.userId = localStorage.getItem('userid')
    var notesLabel = {
      "LableId": label.id,
      "NoteId": this.card.id,
      "UserId": this.userId
    }
    console.log(notesLabel);
    this.notesService.updateNotes(label.id, notesLabel).subscribe(data => {
      console.log(data);
    }, err => {
      console.log(err);
    }
    )
  }

  /**
   * 
   * @param card 
   */
  Delete(card) {
    this.setNote.emit(this.Delete)
    console.log(card);
    this.notesService.DeleteNote(card.id, card).subscribe(
      data => { console.log(data); },
      err => { console.log(err); }
    )
  }

  /**
   * 
   * @param note 
   */
  Collaborator(note): void {
    // localStorage.setItem('noteId', note.id);
    const dialogConfig = new MatDialogConfig();
    let dialogRef = this.dialog.open(CollaborationComponent, {
      // data: { note }
    });
  }
}
