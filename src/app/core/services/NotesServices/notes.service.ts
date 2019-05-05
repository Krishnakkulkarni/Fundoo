import { Injectable } from '@angular/core';
import { HttpService } from '../HttpServices/http.service';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor(public httpService: HttpService) { }

  addNotes(data) {
    return this.httpService.post('Notes/addNotes', data);
  }
  getNotesById(UserId: string) {
    return this.httpService.get('notes/' + UserId);
  }
}