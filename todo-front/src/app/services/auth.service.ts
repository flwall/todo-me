import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private API_URL = 'https://localhost:9011/api/';
  constructor(private  http: HttpClient) { }

  loginUser(user, password) {
    let u:AuthUser={username:user, password:password};
    return this.http.post(this.API_URL + 'auth/login/', u).subscribe(response => {
      console.log(response);
    });
  }
}
