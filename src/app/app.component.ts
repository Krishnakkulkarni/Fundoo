import { Component } from '@angular/core';
import { MessagingService } from './core/services/MessagingService/messaging.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Fundoo';
  constructor(private messagingService: MessagingService) { }
  message;
  ngOnInit() {
  this.messagingService.getPermission();
  this.messagingService.receiveMessage()
  this.message = this.messagingService.currentMessage
  }
  
}
