import { Component, OnInit } from '@angular/core';
import {FetchToDoService} from '../../services/fetch-to-do.service';
import {Todo} from '../../models/todo';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private todos: Todo[];
  private errorMessage: any;

  constructor(private fetchTodo: FetchToDoService) { }

  getTodos() {
    this.fetchTodo.getTodos()
      .subscribe(
        heroes => this.todos = heroes,
        error =>  this.errorMessage = error as any);
  }

  ngOnInit(): void {
  }

}
