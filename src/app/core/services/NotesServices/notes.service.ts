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
    console.log(id, data, "check");

    return this.httpService.updateAll('Notes/' + id, data);
  }

  Trash(id, card) {
    return this.httpService.updateAll('Notes/' + id, card)
  }
  ArchiveNote(id, card) {
    return this.httpService.updateAll('Notes/' + id, card)
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
  reminders(userid) {
    return this.httpService.Get('Notes/reminder/' + userid)
  }

  AddNotesLabels(data) {
    return this.httpService.post('', data)
  }
  addcollaborator(data) {
    return this.httpService.post('Notes/addCollaborator', data)
  }
  getCollaboratorNote(UserId) {
    return this.httpService.Get('Notes/' + UserId)
  }
  removeCollaborator(id){
    return this.httpService.deletelabel('Notes/removeCollaborator/'+ id)
  }
}