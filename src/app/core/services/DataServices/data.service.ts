import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private messageSource = new BehaviorSubject(true);
  currentMessage = this.messageSource.asObservable();

  private message = new BehaviorSubject({type:''});
  current = this.message.asObservable();
  constructor() { }
  
  changeMessage(message: boolean) {
    this.messageSource.next(message)
  }


  change(message: any) {
    this.message.next(message)
  }
  

}
