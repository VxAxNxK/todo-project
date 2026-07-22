import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TodoService } from '../services/todo.service';
import { Todo } from '../models/todo';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';

@Component({
  selector: 'app-todo-list',
  imports: [FormsModule],           // 表單雙向繫結需要
  templateUrl: './todo-list.html',
  styleUrls: ['./todo-list.css'],
})
export class TodoList {
  private todoService = inject(TodoService);
  private categoryService = inject(CategoryService);

  todos = signal<Todo[]>([]);       // signal：響應式狀態，變動時畫面自動更新
  categories = signal<Category[]>([]); // signal：響應式狀態，變動時畫面自動更新  
  newTitle = '';
  selectedCategoryId: number | null = null;

  ngOnInit() {                      // 生命週期：元件初始化時執行
    this.loadTodos();
    this.loadCategories();
  }

  loadTodos() {
    this.todoService.getTodos().subscribe({
      next: (res) => this.todos.set(res.data),
      error: (err) => console.error('載入失敗', err),
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (res) => {
        this.categories.set(res.data ?? []);
        if (this.categories().length > 0) {
          this.selectedCategoryId = this.categories()[0].id;
        } 
      },
      error: (err) => console.error('載入分類失敗', err),
    });
  }

  addTodo() {
    if (!this.newTitle.trim()) return;
    this.todoService
      .createTodo({ title: this.newTitle, isCompleted: false, categoryId: this.selectedCategoryId ?? 1 })
      .subscribe(() => {
        this.newTitle = '';
        this.loadTodos();
      });
  }

  toggleComplete(todo: Todo) {
    this.todoService
      .updateTodo(todo.id, { ...todo, isCompleted: !todo.isCompleted })
      .subscribe(() => this.loadTodos());
  }

  deleteTodo(id: number) {
    this.todoService.deleteTodo(id).subscribe(() => this.loadTodos());
  }
}
