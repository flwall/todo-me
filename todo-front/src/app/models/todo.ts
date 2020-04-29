export class Todo {
  private todoID: string;
  private title: string;
  private description: string;
  private createdAt: string;
  private done: boolean;

  constructor(todoID: string, title: string, description: string, createdAt: string, done: boolean) {
    this.todoID = todoID;
    this.title = title;
    this.description = description;
    this.createdAt = createdAt;
    this.done = done;
  }
}
