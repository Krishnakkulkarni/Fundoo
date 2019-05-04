import { Injectable } from '@angular/core';
import { HttpService } from '../HttpServices/http.service';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor(public httpService: HttpService) { }

  addNotes(data: { UserId: any; Title: string; Description: string; }) {
    return this.httpService.post('Notes/', data);
  }
  getNotesById(UserId: string) {
    return this.httpService.get('notes/'+UserId);
  }
}