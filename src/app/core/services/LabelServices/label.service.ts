import { Injectable } from '@angular/core';
import { HttpService } from '../HttpServices/http.service';

@Injectable({
  providedIn: 'root'
})
export class LabelService {

  constructor(public httpService:HttpService) { }

  getlabels(userId) {
    return this.httpService.Get('Labels/getLabel/' + userId);
  }
  AddLabels(data) {
    return this.httpService.post('Labels/addLabel', data)
  }
  updateLabel(id, data) {
    return this.httpService.updateAll('Labels/updateLabel/' + id, data)
  }
  deletelabel(id) {
    return this.httpService.deletelabel('Labels/' + id)
  }
}
