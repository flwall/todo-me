import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private API_URL = 'https://localhost:9011/api/';
  constructor(private  http: HttpClient) { }

  loginUser(email, password) {
    return this.http.post(this.API_URL + 'auth/login/', {
      email,
      password
    }).subscribe(response => {
      console.log(response);
    });
  }
}
