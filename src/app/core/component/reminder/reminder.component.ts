import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../services/NotesServices/notes.service';

@Component({
  selector: 'app-reminder',
  templateUrl: './reminder.component.html',
  styleUrls: ['./reminder.component.css']
})
export class ReminderComponent implements OnInit {
  userId: any
  reminderCards: any;
  constructor(public notesService: NotesService) { }

  ngOnInit() {
    this.reminder();

  }
  reminder() {
    this.userId = localStorage.getItem('userid')
    this.notesService.reminders(this.userId).subscribe(data => {
      console.log(data);

      this.reminderCards = data["result"];
      console.log(this.reminderCards)
    }, err => {
      console.log(err);
    }
    )
  }

}