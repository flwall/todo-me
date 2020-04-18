import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { AuthUser } from '../models/authUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private API_URL = 'https://localhost:9011/api/';
  constructor(private  http: HttpClient) { }

  loginUser(user, password) {
    let headers=new HttpHeaders().set("Content-Type", "application/json");
    let u:AuthUser={username:user, password:password};
    return this.http.post(this.API_URL + 'auth/login/', u, {headers, responseType:'text'}).subscribe(response => {
      console.log(response);
    });
  }
}
