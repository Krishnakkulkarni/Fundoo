import { Component, OnInit, Input } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';
import { DataService } from '../../services/DataServices/data.service';
import { MatDialog } from '@angular/material/dialog';
import { EditnoteComponent } from '../editnote/editnote.component';

export interface DialogData {
  note: any;
}

@Component({
  selector: 'app-displaynotes',
  templateUrl: './displaynotes.component.html',
  styleUrls: ['./displaynotes.component.css']
})

export class DisplaynotesComponent implements OnInit {
  grid: boolean = true

  constructor(private notesService: NotesService, public dataService: DataService, public matDialog: MatDialog) { }

  @Input() cards: any;
  @Input() archived
  ngOnInit() {
    this.dataService.currentMessage.subscribe(data => {
      this.grid = data
    });
  }

  openDialog(note: { id: any; }) {
    console.log(note);
    const dialogRef = this.matDialog.open(EditnoteComponent, {
      width: '500px',
      data: { note }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed', result);
      this.notesService.NoteUpdated(result.id, result).subscribe
        (data => { console.log(data, note); },
          err => { console.log(err); })
    });
  }
}