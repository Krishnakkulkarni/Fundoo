import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';
import { MatDialog } from '@angular/material/dialog';
import { EditnoteComponent } from '../editnote/editnote.component';
import { MatChipInputEvent } from '@angular/material';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Observable } from 'rxjs';

export interface DialogData {
  note: any;
  description: any;
  title: any;
}

@Component({
  selector: 'app-displaynotes',
  templateUrl: './displaynotes.component.html',
  styleUrls: ['./displaynotes.component.css']
})

export class DisplaynotesComponent implements OnInit {
  grid: boolean = true
  // notes:Notes[];

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes = [ENTER, COMMA];

  constructor(private notesService: NotesService, public dataService: DataService, public matDialog: MatDialog) { }

  @Input() search;

  @Input() cards: any;
  @Input() archived;
  @Input() trash;

  @Output() messageEvent = new EventEmitter<any>();

  unrchive: boolean;
  archive: boolean;
  trashNote: boolean;
  title: any;
  description: any;

  collaborator: any;
  receiverEmail: string;
  userId: any

  /**
   * 
   */
  ngOnInit() {
    this.dataService.currentMessage.subscribe(data => {
      this.grid = data
    });
    this.userId = localStorage.getItem("userid")
    this.receiverEmail = localStorage.getItem('receiverEmail');
    this.notesService.getCollaboratorNote(this.userId).subscribe(response => {
      this.collaborator = response;
      console.log(this.collaborator);

    }, err => {
      console.log(err);
    })

  }

  // getAllNotes()
  // {
  //    this.userId=localStorage.getItem("userid")
  //   this.notesService.getNotesById(this.userId).subscribe(  
  //     data => {
  //       this.notes=data;
  //       this.cards=[];
  //     this.cards=data;
  //     this.cards.forEach(element => {
  //       if(element.isArchive || element.isTrash){
  //         return;
  //       }
  //       else
  //       this.cards.push(element);       
  //     });
  //     console.log(this.cards);
  //     }
  // ),err=>{
  //          console.log(err);         
  //        };
  // }
  // updateCome(event) {

  //   this.getAllNotes();
  // }

  /**
   * 
   * @param note 
   */
  openDialog(note: { id: any; }) {
    console.log(note);
    const dialogRef = this.matDialog.open(EditnoteComponent, {
      width: '500px',
      data: { note }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.notesService.updateNotes(result.id, result).subscribe
        (data => { console.log(data, note); },
          err => { console.log(err); })
    });
  }

  /**
   * 
   * @param event 
   */
  Archive(event) {
    console.log('event');
    this.messageEvent.emit(event)
  }

  /**
   * 
   * @param event 
   */
  Trash(event) {
    console.log('trash in');
    this.messageEvent.emit(event);
  }

  removeReminder(cards) {
    console.log(this.cards);
    cards.reminder = '0001-01-01T00:00:00-08:00';
    console.log(this.cards);

    this.notesService.updateNotes(cards.id, cards).subscribe(
      data => {
        console.log(data);
      },
      err => {
        console.log(err);
      }
    )
  }

  remove(id, label) {
    console.log(this.cards);
    label.label = null;
    console.log(this.cards);

    this.notesService.updateNotes(id, label).subscribe(
      data => {
        console.log(data);
      },
      err => {
        console.log(err);
      }
    )
  }

}