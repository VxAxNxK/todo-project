import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo';
import { ApiResponse } from '../models/api-response';

@Injectable({ providedIn: 'root' })   // 等同於後端註冊 Singleton
export class TodoService {
  private http = inject(HttpClient);  // DI，注入 HttpClient
  private apiUrl = 'http://localhost:5000/api/todos'; // ← 改成你後端的實際 port 和路由

  getTodos(): Observable<ApiResponse<Todo[]>> {
    return this.http.get<ApiResponse<Todo[]>>(this.apiUrl);
  }

  getTodo(id: number): Observable<ApiResponse<Todo>> {
    return this.http.get<ApiResponse<Todo>>(`${this.apiUrl}/${id}`);
  }

  createTodo(todo: Partial<Todo>): Observable<ApiResponse<Todo>> {
    return this.http.post<ApiResponse<Todo>>(this.apiUrl, todo);
  }

  updateTodo(id: number, todo: Partial<Todo>): Observable<ApiResponse<Todo>> {
    return this.http.put<ApiResponse<Todo>>(`${this.apiUrl}/${id}`, todo);
  }

  deleteTodo(id: number): Observable<ApiResponse<null>> {
    return this.http.delete<ApiResponse<null>>(`${this.apiUrl}/${id}`);
  }
}
