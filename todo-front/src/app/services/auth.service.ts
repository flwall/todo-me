import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {AuthUser} from '../models/authUser';
import {Observable} from 'rxjs';
import {Todo} from '../models/todo';
import 'rxjs/add/operator/map';
import {delay} from 'rxjs/operators';

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

  constructor(private  http: HttpClient) {
  }

  private cookie = 'DEFAULT';

  async loginUser(user, passwrd) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const u: AuthUser = {username: user, password: passwrd};
    const sth = this.http.post(this.API_URL + 'auth/login/', u, {headers, responseType: 'text', observe: 'response'});

    sth.map((response) => {
      this.cookie = response.headers.get('Set-Cookie');

    });
    await delay(3000);

    return sth;

  }

  setLoggedIn(b: boolean) {
    this.loggedInStatus = b;
  }

  get isLoggedIn() {
    return this.loggedInStatus;
  }

  getTodos(): Observable<Todo[]> {
    console.log(this.cookie);
    const headers = new HttpHeaders();
    headers.set('Set-Cookie', this.cookie);
    return this.http.get<Todo[]>(this.API_URL + 'todos', {headers});
    /*
      .toPromise()
      .then(response => response.map(i => new Todo(i.todoID, i.title, i.description, i.createtAt, i.done )))
      .catch(error => {
        console.log(error);
      });*/
  }
}
