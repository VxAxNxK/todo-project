import { Category } from "./category";

export interface Todo {

    id: number;
    title: string;
    isCompleted: boolean;
    categoryId: number;
    category?: Category;

}
