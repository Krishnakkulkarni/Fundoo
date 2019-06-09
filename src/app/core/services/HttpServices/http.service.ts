import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  rooturl = environment.rooturl;

  constructor(public http: HttpClient) { }

  postwithoutToken(url: string, data: any) {
    return this.http.post(this.rooturl + url, data)
  }

  post(url: string, data: any) {
    var httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'bearer ' + localStorage.getItem('token')
      })
    };
    return this.http.post(this.rooturl + url, data, httpOptions)
  }
  Get(url: String) {
    var httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'bearer ' + localStorage.getItem('token')
      })
    };
    return this.http.get(this.rooturl + url, httpOptions)
  }

  GetString(url: String) {
    // var httpOptions = {
    //   headers: new HttpHeaders({
    //     'Authorization':'bearer '+ localStorage.getItem('token')
    //   })
    // };
    return this.http.get(this.rooturl + url, { responseType: 'text' })
  }

  update(url: string, data: any) {
    var httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'bearer' + localStorage.getItem('token')
      })
    };
    return this.http.put(this.rooturl + url, data, httpOptions)
  }
  deletenote(url: string, data) {
    return this.http.delete(this.rooturl + url, data)
    //   return this.http.request('DELETE', this.rooturl + url, {
    //     headers: new HttpHeaders({
    //         'Content-Type': 'application/json',
    //         'Authorization':'bearer '+ localStorage.getItem('token')
    //     }),
    //     body: data
    // });
  }
  postImage(url, data) {
    var httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'bearer ' + localStorage.getItem('token')
      })
    };
    return this.http.post(this.rooturl + url, data, httpOptions)
  }
  deletelabel(url) {
    var httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'bearer ' + localStorage.getItem('token')
      })
    };
    return this.http.delete(this.rooturl + url, httpOptions)
  }
}
