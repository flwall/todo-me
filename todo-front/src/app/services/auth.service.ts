import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { AuthUser } from '../models/authUser';

interface Data {
  sucess: boolean;
  message: string;
  responseType: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  loggedInStatus = false;

  private API_URL = 'https://localhost:9011/api/';
  constructor(private  http: HttpClient) { }

  loginUser(user, passwrd) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const u: AuthUser = {username: user, password: passwrd};
    return this.http.post(this.API_URL + 'auth/login/', u, {headers, responseType: 'text'});
  }

  setLoggedIn(b: boolean) {
    this.loggedInStatus = b;
  }

  get isLoggedIn() {
    return this.loggedInStatus;
  }
}
