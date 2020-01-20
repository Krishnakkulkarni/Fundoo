import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private searchMessage = new BehaviorSubject<string>('');
  currentSearchmsg = this.searchMessage.asObservable();

  private messageSrc = new BehaviorSubject<boolean>(false);
  currentMsg = this.messageSrc.asObservable();

  private message = new BehaviorSubject({type:''});
  current = this.message.asObservable();

  private messageSource = new BehaviorSubject(true);
  currentview = this.messageSource.asObservable();

  private image = new Subject<boolean>();
  currentImage = this.image.asObservable();

  private num = new Subject<Number>();
  currentNum = this.num.asObservable();

  constructor() { }

  changeNum(view: Number) {
    this.num.next(view)
  }
  
  changeImage(message:boolean){
    this.image.next(message)
  }
  
  changeView(view: boolean) {
    this.messageSource.next(view)
  }

  change(message: any) {
    this.message.next(message)
  }
  
  changeMessage(message: boolean) {
    this.messageSrc.next(message)
  }

  changeSearchMsg(view: string){
    this.searchMessage.next(view);
    
  }
}
