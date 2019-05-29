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
    this.userId = localStorage.getItem('userid')
    this.notesLabel = this.data
  }

  close() {
    var data = {
      "Label": this.label.value,
      "UserId": this.userId
    }
    if (this.label.value != "") {
      this.notesService.AddLabels(data).subscribe(data =>
        console.log(data)
      )
      this.dialogRef.close(data);
      this.AfterAddEvent.emit({});
    }
  }

  update(label) {
    console.log(label.label, "in label update");
    this.notesService.updateLabel(label.id, label).subscribe(result =>
      console.log(result)
    )
  }
  delete(id) {
    console.log(id);
    this.notesService.deletelabel(id).subscribe(result =>
      console.log(id))
  }
}
