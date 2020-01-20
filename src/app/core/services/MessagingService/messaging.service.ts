import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs'
import * as firebase from 'firebase/app'
import 'firebase/messaging';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class MessagingService {
  currentMessage = new BehaviorSubject(null);
  messaging;
  constructor(private router: Router) {
    try {
      firebase.initializeApp({
        'messagingSenderId': '250115212184'
      });
      this.messaging = firebase.messaging();
    } catch (err) {
      console.error('Firebase initialization error', err.stack);
    }
  }
  getPermission() {
    this.messaging.requestPermission()
      .then(() => {
        return this.messaging.getToken()
      })
      .then(token => {
        console.log(token)
        localStorage.setItem("notiToken", token)
        this.router.navigateByUrl('home');

      })
      .catch((err) => {
      });
  }

  receiveMessage() {
    this.messaging.onMessage((payload) => {
      console.log("Message received. ", payload);
      this.currentMessage.next(payload)
    });
  }
}

