import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private API_URL = 'https://localhost:9011/api/';
  constructor(private  http: HttpClient) { }

  loginUser(user, pass) {
     this.http.post(this.API_URL + 'auth/login/', {
      username: user,
      password: pass
    }).subscribe( error => console.log('oops', error),
    data => console.log('success', data)
    );
  }
}
