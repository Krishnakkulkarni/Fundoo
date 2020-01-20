import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotesService } from '../../services/NotesServices/notes.service';
import { MatSnackBar, MatDialog } from '@angular/material';
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
  public selectedFile: File = null;
  public flag: boolean = false;
  public labels;

  @Input() archivedicon
  @Input() trashed //to get trashed notes

  @Output() setNote = new EventEmitter();


  @Output() update = new EventEmitter();

  public trash: boolean = true;
  public archive: boolean = true;
  public unarchive: boolean = true;
  public userId: string;
  public data;

  onFileSelected(Event: any, card: any) {
    this.selectedFile = <File>Event.path[0].files[0];
    this.Onupload(card)
  }

  constructor(public httpClient: HttpClient, public notesService: NotesService, public labelService: LabelService,
    public SnackBar: MatSnackBar, private service: DataService, public dialog: MatDialog) { }

  @Input() card: any;

  /**
   * Main Method
   */
  ngOnInit() {
    this.userId = localStorage.getItem('userid');
    this.labelService.getlabels(this.userId).subscribe(
      data => { this.labels = data['result']; }
    )
  }

  /**
   * Method to Upload image on card
   * @param card 
   */
  Onupload(card) {
    if (card.id != undefined) {
      const formdata = new FormData();

      formdata.append('file', this.selectedFile);
      this.notesService.ImageUpload(formdata, card.id).subscribe
        (data => {
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
   * Method to set color on card
   * @param color 
   * @param card 
   */
  setcolor(color: any, card) {
    if (card == undefined) {
      this.setNote.emit(color)
    }
    else {
      card.color = color;
      this.notesService.updateNotes(card.id, card).subscribe(
        data => {  },
        err => { console.log(err); }
      )
    }
  }

  /**
   * Method to Archive the card 
   * @param card 
   */
  Archive(card) {
    card.pin = false
    card.isArchive = true;
    this.notesService.ArchiveNote(card.id, card).subscribe(
      data => {
        this.setNote.emit()
        this.SnackBar.open("Note Archived", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * Method to Unarchive the card
   * @param card 
   */
  Unarchive(card) {
    card.isArchive = false;
    this.notesService.ArchiveNote(card.id, card).subscribe(
      data => {
        this.setNote.emit()

        this.SnackBar.open("note unarchive", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * Method to Trash the card
   * @param card 
   */
  trashNote(card) {
    card.pin = false
    card.isTrash = true;
    this.notesService.Trash(card.id, card).subscribe(
      data => {
        this.setNote.emit()
        this.SnackBar.open("Note Trashed", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * Method to restore the card to dashboard
   * @param card 
   */
  restore(card) {
    card.isTrash = false;
    this.notesService.Trash(card.id, card).subscribe(
      data => {
        this.setNote.emit()
        this.SnackBar.open("Note restored", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * Method to add labels on card
   * @param label
   */
  checkList(label) {
    this.card.label = label.label
    this.notesService.updateNotes(this.card.id, this.card).subscribe(data => {
    }, err => {
      console.log(err);
    }
    )
  }

  /**
   * Method to delete the card
   * @param card 
   */
  delete(card) {
    this.notesService.DeleteNote(card.id, card).subscribe(
      data => {
        this.setNote.emit()
      },
      err => { console.log(err); }
    )
  }

  /**
   * Method to add collaborator
   * @param note 
   */
  collaborator(card): void {
    localStorage.setItem('noteId', card.id);
    let dialogRef = this.dialog.open(CollaborationComponent,
      { data: { card } });
  }

  /**
   * Method to set reminder for Today
   * @param card 
   */
  today(card) {
    var date = new Date();
    date.setHours(8, 0, 0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    this.notesService.updateNotes(card.id, card).subscribe(data => {
      this.update.emit({});
    }, err => {
      console.log(err);
    })
  }

  /**
   * Method to set reminder for Tomorrow
   * @param card 
   */
  tomorrow(card) {
    var date = new Date();
    date.setHours(8, 0, 0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (date.getDate() + 1) + " " + date.getHours() + ":" + date.getMinutes();
    this.notesService.updateNotes(card.id, card).subscribe(data => {
      this.update.emit({});
    }, err => {
      console.log(err);
    })
  }

  /**
   * Method to set reminder for nextWeek
   * @param card 
   */
  nextWeek(card) {
    var date = new Date();
    date.setHours(8, 0, 0)
    card.reminder = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (date.getDate() + 7) + " " + date.getHours() + ":" + date.getMinutes();
    this.notesService.updateNotes(card.id, card).subscribe(data => {
      this.update.emit({});
    }, err => {
      console.log(err);
    })
  }

  stopPropogation() {

  }
  myFilter = (d: Date): boolean => {
    const day = d.getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
  }
}
