import { Component, OnInit } from '@angular/core';
import { Todo } from '../../models/todo';
import { AuthService } from 'src/app/services/auth.service';
import {Observable} from 'rxjs';
import { Http, Response } from '@angular/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public todos: Observable<Todo[]>;

  constructor(private authService: AuthService, public http: Http) { }

  getTodos() {
    return this.http.get('https://localhost:9011/api/todos', {withCredentials: true}).map((res: Response) => res.json());
        }

  ngOnInit(): void {
    this.todos = this.getTodos();
  }

  onAdd(itemTitle, itemDescription) {
    this.authService.addTodo(itemTitle.value, itemDescription.value);
    itemTitle.value = null;
    itemDescription.value = null;
  }

  alterCheck(id: string, isChecked) {
    console.log('done triggered');
    this.authService.checkOrUnCheckTodo(id, !isChecked);
  }

  onDelete(id : string) {
    console.log('ID:' + id + ' gelöscht');
    this.authService.removeTodo(id);
  }
}
