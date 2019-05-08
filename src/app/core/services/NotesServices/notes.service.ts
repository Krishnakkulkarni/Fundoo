import { Injectable } from '@angular/core';
import { HttpService } from '../HttpServices/http.service';
import { identifierModuleUrl } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor(public httpService: HttpService) { }

  addNotes(data) {
    return this.httpService.post('Notes/addNotes', data);
  }
  getNotesById(UserId: string) {
    console.log(UserId);
    return this.httpService.get('notes/'+UserId);
  }
  ImageUpload(data,id){
    // console.log(data,id);
  return this.httpService.post('Notes/image/'+id,data);
  }
  updateNotes(id,data){
    return this.httpService.update('Notes/'+id,data)
  }
  Delete(id){
    return this.httpService.deletenote('Notes/'+id,"")
  }
}