import { Component, OnInit } from '@angular/core';
import { BookService } from './book.service';
import {Books} from './book.model';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {
  bookData: Books[] = [];

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.bookData = this.bookService.obtenerLibros();
  }

}
