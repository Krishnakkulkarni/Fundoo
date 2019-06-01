import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  rooturl = environment.rooturl;

  constructor(public http: HttpClient) { }

  post(url: string, data: any) {
    return this.http.post(this.rooturl + url, data)
  }
  Get(url: String) {
    return this.http.get(this.rooturl + url)
  }
 
  GetString(url: String) {
    return this.http.get(this.rooturl + url, {responseType: 'text'})
  }
  update(url: string, data: any) {
    return this.http.put(this.rooturl + url, data)
  }
  deletenote(url: string, data) {
    return this.http.delete(this.rooturl + url, data)
  }
  postImage(url,data) {
    // var http = {
    //   headers: new HttpHeaders({
    //     'Authorization': localStorage.getItem('result')
    //   })
    // };
    return this.http.post(this.rooturl + url,data)
  }
  deletelabel(url) {
    return this.http.delete(this.rooturl + url)
  }
}
