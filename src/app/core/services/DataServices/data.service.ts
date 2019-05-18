import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private messageSource = new BehaviorSubject(true);
  currentMessage = this.messageSource.asObservable();

  private message = new BehaviorSubject({type:''});
  current = this.message.asObservable();




  private image = new Subject<boolean>();
  currentImage = this.image.asObservable();

  constructor() { }

  changeImage(message:boolean){
    console.log(message,"in data")
    this.image.next(message)
  }
  

  
  changeView(view: boolean) {
    this.messageSource.next(view)
  }

  change(message: any) {
    this.message.next(message)
  }
  

}
