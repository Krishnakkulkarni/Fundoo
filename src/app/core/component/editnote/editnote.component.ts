import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { DialogData } from '../displaynotes/displaynotes.component';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-editnote',
  templateUrl: './editnote.component.html',
  styleUrls: ['./editnote.component.css']
})
export class EditnoteComponent implements OnInit {

  color: string = "#ffffff";
  
  constructor(public notesService: NotesService, public dialogRef: MatDialogRef<EditnoteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    console.log(data);
  }

  ngOnInit() {
  }

  /**
   * 
   */
  onNoClick(): void {
    this.dialogRef.close();
  }
  /**
   * 
   * @param $event 
   */
  SetColor($event: string) {
    this.color = $event
  }
}
