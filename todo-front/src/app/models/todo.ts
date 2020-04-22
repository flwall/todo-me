export class Todo {
  todoID: number;
  title: string;
  description: string;
  createdAt: string;
  done: boolean;

  constructor(todoID: number, title: string, description: string, createdAt: string, done: boolean) {
    this.todoID = todoID;
    this.title = title;
    this.description = description;
    this.createdAt = createdAt;
    this.done = done;
  }
}
