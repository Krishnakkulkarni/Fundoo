import { Component, OnInit, EventEmitter, Output, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DataService } from '../../services/DataServices/data.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-edit-labels',
  templateUrl: './edit-labels.component.html',
  styleUrls: ['./edit-labels.component.css']
})
export class EditLabelsComponent implements OnInit {
  notesLabel: any;
  userId: any;
  @Output() AfterAddEvent = new EventEmitter();

  constructor(public dataServices: DataService, public notesService: NotesService,
    public dialogRef: MatDialogRef<EditLabelsComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }
    label = new FormControl('');

  ngOnInit() {
    this.userId = localStorage.getItem('UserID')
    this.notesLabel = this.data
  }

  close() {
    var data = {
      "Label": this.label.value,
      "UserId": this.userId
    }
    console.log(data);
    if (this.label.value != "") {
      this.notesService.AddLabels(data).subscribe(result =>
        console.log(data)

      )
      this.dialogRef.close(data);
      this.AfterAddEvent.emit({});
    }
  }

  update(label) {
    console.log(label.label, "mghxchj");
    this.notesService.updateLabel(label.id, label.label).subscribe(result =>
      console.log(result)
    )
  }
  delete(label) {
    this.notesService.deletelabel(label.id).subscribe(result =>
      console.log(result))
  }
}
