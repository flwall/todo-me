import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private  http: HttpClient) { }

  loginUser(email, password) {
    return this.http.post('', {
      email,
      password
    }).subscribe(response => {
      console.log(response);
    });
  }
}
