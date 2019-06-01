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

  /**
   * 
   */
  ngOnInit() {
    this.dataService.currentMessage.subscribe(data => {
      this.grid = data
    });
  }

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
      this.notesService.NoteUpdated(result.id, result).subscribe
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
}