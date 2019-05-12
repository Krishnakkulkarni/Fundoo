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
    return this.httpService.Get('Notes/' + UserId);
  }
  ImageUpload(data, id) {
    return this.httpService.post('Notes/image/' + id, data);
  }
  updateNotes(id, data) {
    return this.httpService.update('Notes/' + id, data)
  }
  Trash(id, card) {
    return this.httpService.update('Notes/' + id, card)
  }
  ArchiveNote(id, card) {
    return this.httpService.update('Notes/' + id, card)
  }
  GetArchiveNotes(UserId) {
    return this.httpService.Get('Notes/archive/' + UserId)
  }
  ViewInTrash(UserId) {
    return this.httpService.Get('Notes/trash/' + UserId)
  }
  DeleteNote(id, card) {
    return this.httpService.deletenote('Notes/' + id, card)
  }
  NoteUpdated(id, result) {
    return this.httpService.update('Notes/' + id, result)
  }
}