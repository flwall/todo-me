import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Todo} from '../../models/todo';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private todos: Todo[];
  private errorMessage: any;

  constructor(private fetchTodo: AuthService) { }

  getTodos() {
    this.fetchTodo.getTodos()
      .subscribe(
        todo => {this.todos = todo; console.log(todo); },
        error =>  this.errorMessage = error as any);
  }

  ngOnInit(): void {
    this.getTodos();
    console.log(this.todos);
  }


}
