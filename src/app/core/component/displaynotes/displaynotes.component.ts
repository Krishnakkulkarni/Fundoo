import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EditnoteComponent } from '../editnote/editnote.component';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatSnackBar } from '@angular/material';
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
  public grid: boolean = true
  public users;
  public visible = true;
  public selectable = true;
  public removable = true;
  public addOnBlur = true;
  public separatorKeysCodes = [ENTER, COMMA];

  constructor(private notesService: NotesService, public dataService: DataService, public matDialog: MatDialog,
    public SnackBar: MatSnackBar) { }

  @Input() search;
  @Input() collnotes;
  @Input() cards: any; // getting all notes
  @Input() pinnedNote;
  @Input() archived;
  @Input() trash;

  @Output() messageEvent = new EventEmitter<any>();

  public title: any;
  public description: any;
  public collaborator: any;
  public receiverEmail: string;
  public userId: any

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
  }

  /**
   * Method to Edit Note 
   * @param note 
   */
  openDialog(note: { id: any; }) {
    const dialogRef = this.matDialog.open(EditnoteComponent, {
      panelClass: "editDailog",
      data: { note }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.notesService.updateNotes(result.id, result).subscribe
        (data => { },
          err => { console.log(err); })
    });
  }

  /**
   * Method to add collaborator
   * @param note 
   */
  colDialog(card): void {
    const dialogConfig = new MatDialogConfig();
    let dialogRef = this.matDialog.open(CollaborationComponent,
      { data: { card } });
  }

  /**
   * Method for archive
   * @param event 
   */
  noteUpdate(event) {
    let index = this.cards.indexOf(event)
    this.cards.splice(index, 1)
  }

  /**
   * Method to remove Reminder
   * @param cards 
   */
  removeReminder(cards) {
    cards.reminder = '0001-01-01T00:00:00';
    this.notesService.updateNotes(cards.id, cards).subscribe(
      data => { },
      err => { console.log(err); }
    )
  }

  /**
   * Method to remove label
   * @param id 
   * @param label 
   */
  remove(id, label) {
    label.label = null;
    this.notesService.updateNotes(id, label).subscribe(
      data => { },
      err => { console.log(err); }
    )
  }


  /**
   * Method to Archive the card 
   * @param card 
   */
  pin(card) {
    card.pin = true;
    this.notesService.pinnedNote(card.id, card).subscribe(
      data => {
        this.SnackBar.open("Note pinned", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }

  /**
   * Method to Unarchive the card
   * @param card 
   */
  unPin(card) {
    card.pin = false;
    this.notesService.pinnedNote(card.id, card).subscribe(
      data => {
        this.SnackBar.open("note unpinned", "close", { duration: 2000 });
      },
      err => { console.log(err); }
    )
  }
}