export class Todo {
  private todoID: number;
  private title: string;
  private description: string;
  private createdAt: string;
  private done: boolean;

  constructor(todoID: number, title: string, description: string, createdAt: string, done: boolean) {
    this.todoID = todoID;
    this.title = title;
    this.description = description;
    this.createdAt = createdAt;
    this.done = done;
  }
}
