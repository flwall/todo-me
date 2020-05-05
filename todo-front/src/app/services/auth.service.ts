import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AuthUser } from "../models/authUser";
import { Observable } from "rxjs";
import { Todo } from "../models/todo";
import "rxjs/add/operator/map";
import { delay } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(`${this.API_URL}todos`, {
      withCredentials: true,
    });
  }
  loggedInStatus = false;

  private API_URL = "https://localhost:9011/api/";

  constructor(private http: HttpClient) {}

  private cookie = "DEFAULT";

  loginUser(user, passwrd) {
    const headers = new HttpHeaders().set("Content-Type", "application/json");
    headers.set("Access-Control-Allow-Credentials", "true");
    headers.set(
      "Access-Control-Allow-Headers",
      "Origin,Content-Type,Accept,Access-Control-Allow-Origin"
    );
    const u: AuthUser = { username: user, password: passwrd };
    return this.http.post(this.API_URL + "auth/login/", u, {
      headers,
      responseType: "text",
      observe: "response",
      withCredentials: true,
    });
  }
  registerUser(user: AuthUser) {
    return this.http.post(this.API_URL + "auth/register", user, {
      withCredentials: true,
      observe: "response",
      responseType: "text",
    });
  }

  setLoggedIn(b: boolean) {
    this.loggedInStatus = b;
  }

  get isLoggedIn() {
    return this.loggedInStatus;
  }

  addTodo(todo: Todo): Observable<Todo> {
    return this.http.post<Todo>(this.API_URL + "todos", todo, {
      withCredentials: true,
    });
  }

  toggleTodo(todoid: number): Observable<Todo> {
    return this.http.put<Todo>(`${this.API_URL}todos/${todoid}/done`, null, {
      withCredentials: true,
    });
  }

  removeTodo(todoid: number): Observable<Todo> {
    return this.http.delete<Todo>(`${this.API_URL}todos/${todoid}`, {
      withCredentials: true,
    });
  }
}
