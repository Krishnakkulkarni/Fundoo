import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EditnoteComponent } from '../editnote/editnote.component';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { CollaborationComponent } from '../collaboration/collaboration.component';

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
  users;
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes = [ENTER, COMMA];

  constructor(private notesService: NotesService, public dataService: DataService, public matDialog: MatDialog) { }

  @Input() search;
  @Input() Collnotes;
  @Input() cards: any;
  @Input() archived;
  @Input() trash;

  @Output() messageEvent = new EventEmitter<any>();

  title: any;
  description: any;

  collaborator: any;
  receiverEmail: string;
  userId: any

  /**
   * Main Method 
   */
  ngOnInit() {
    this.dataService.currentview.subscribe(data => {
      this.grid = data
    });

    this.users = {
      user: localStorage.getItem("username")
    }
    // this.receiverEmail = localStorage.getItem('receiverEmail');
    // this.notesService.getCollaboratorNote(this.userId).subscribe(response => {
    //   this.collaborator = response['note'];

    // }, err => {
    //   console.log(err);
    // })

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
   * Method to Edit Note 
   * @param note 
   */
  openDialog(note: { id: any; }) {
    console.log(note);
    const dialogRef = this.matDialog.open(EditnoteComponent, {
      panelClass: "editDailog",
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
   * Method to add collaborator
   * @param note 
   */
  colDialog(card): void {
    // localStorage.setItem('noteId', card.id);
    const dialogConfig = new MatDialogConfig();
    let dialogRef = this.matDialog.open(CollaborationComponent,
      { data: { card } });
  }

  /**
   * Method for archive
   * @param event 
   */
  Archive(event) {
    console.log('event');
    this.messageEvent.emit(event)
  }

  /**
   * Method for trash
   * @param event 
   */
  Trash(event) {
    console.log('trash in');
    this.messageEvent.emit(event);
  }



  /**
   * Method to remove Reminder
   * @param cards 
   */
  removeReminder(cards) {
    console.log(this.cards);
    cards.reminder = '0001-01-01T00:00:00';
    console.log(this.cards.reminder);

    this.notesService.updateNotes(cards.id, cards).subscribe(
      data => {
        console.log(data);
      },
      err => {
        console.log(err);
      }
    )
  }

  /**
   * Method to remove label
   * @param id 
   * @param label 
   */
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