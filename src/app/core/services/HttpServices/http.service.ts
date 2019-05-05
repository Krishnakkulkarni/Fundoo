import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  rooturl = environment.rooturl;

  constructor(public http: HttpClient) { }

  post(url: string, data: any) {
    return this.http.post(this.rooturl + url, data)
  }
  get(url: String) {
  
    return this.http.get(this.rooturl + url)
  }
}
