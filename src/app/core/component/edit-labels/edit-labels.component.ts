import { Component, OnInit, EventEmitter, Output, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DataService } from '../../services/DataServices/data.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LabelService } from '../../services/LabelServices/label.service';

@Component({
  selector: 'app-edit-labels',
  templateUrl: './edit-labels.component.html',
  styleUrls: ['./edit-labels.component.css']
})
export class EditLabelsComponent implements OnInit {
  notesLabel: any;
  userId: any;
  @Output() labelEvent = new EventEmitter();

  constructor(public dataServices: DataService, public labelService: LabelService,
    public dialogRef: MatDialogRef<EditLabelsComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }
  label = new FormControl('');

  /**
   * 
   */
  ngOnInit() {
    this.userId = localStorage.getItem('userid')
    this.notesLabel = this.data;
  }

  /**
   * 
   */
  close() {
    var data = {
      "Label": this.label.value,
      "UserId": this.userId
    }
    if (this.label.value != "") {
      this.labelService.AddLabels(data).subscribe(data =>
        console.log(data)
      )
      this.dialogRef.close(data);
      this.labelEvent.emit(data);
    }
    else {
      this.dialogRef.close(data);
    }
  }

  /**
   * 
   * @param label 
   */
  update(label) {
    console.log(label.label, "in label update");
    this.labelService.updateLabel(label.id, label).subscribe(result =>
      console.log(result)
    )
  }
  /**
   * 
   * @param id 
   */
  delete(label) {
    let index = this.notesLabel.indexOf(label);
    this.notesLabel.splice(index, 1);
    this.labelService.deletelabel(label.id).subscribe(result =>
      console.log(result))
  }
}
