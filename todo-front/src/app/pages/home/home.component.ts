import { Component, OnInit } from "@angular/core";

import { Todo } from "../../models/todo";
import { AuthService } from "src/app/services/auth.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  private todos: Todo[];
  private errorMessage: any;

  constructor(private fetchTodo: AuthService) {}

  getTodos() {
    this.fetchTodo.getTodos().subscribe(
      (heroes) => (this.todos = heroes),
      (error) => (this.errorMessage = error as any)
    );
  }

  ngOnInit(): void {
    this.getTodos();
  }
}
