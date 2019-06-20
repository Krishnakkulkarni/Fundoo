import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-reminder',
  templateUrl: './reminder.component.html',
  styleUrls: ['./reminder.component.css']
})
export class ReminderComponent implements OnInit {
  userId: any
  reminderCards = [];
  constructor(public notesService: NotesService) { }

  ngOnInit() {
    this.reminder();

  }

  reminder() {
    this.userId = localStorage.getItem('userid')
    this.notesService.reminders(this.userId).subscribe(data => {
      console.log(data);
      var array = data["result"]
      this.reminderCards = [];
      for (let i = 0; i < array.length; i++) {
        if (array[i].reminder != "0001-01-01T00:00:00-08:00") {
          this.reminderCards.push(array[i])
        }
      }
      console.log(this.reminderCards)
    }, err => {
      console.log(err);
    }
    )
  }

}
