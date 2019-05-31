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
  currentMessage = this.messageSource.asObservable();

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
  
  changeMessage(message: boolean) {
    console.log('message in data service',message);
    this.messageSrc.next(message)
  }

  changeSearchMsg(view: string){
    console.log("in dataService search",view);
    this.searchMessage.next(view);
    
  }
}
