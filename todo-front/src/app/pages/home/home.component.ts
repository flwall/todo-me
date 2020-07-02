import { Component, OnInit } from '@angular/core';
import { Todo } from '../../models/todo';
import { AuthService } from 'src/app/services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public todos: Observable<Todo[]>;

  constructor(private authService: AuthService) {}

  getTodos() {
    return this.authService.getTodos();
  }

  ngOnInit(): void {
    this.todos = this.getTodos();
  }

  onAdd(itemTitle, itemDescription) {
    this.authService
      .addTodo({ title: itemTitle.value, description: itemDescription.value })
      .subscribe((d) => (this.todos = this.getTodos())); // not neccessary to fetch each todo every time

    itemTitle.value = null;
    itemDescription.value = null;
  }

  alterCheck(id: number) {
    this.authService.toggleTodo(id).subscribe((ob) => {
      this.todos = this.getTodos(); // not good
    });
  }

  onDelete(id: number) {
    this.authService.removeTodo(id).subscribe((t) => {
      this.todos = this.getTodos();                     // alter that
    });
  }
}
