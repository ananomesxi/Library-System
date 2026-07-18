using Application.Interfaces;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Text;
using System.Threading.Channels;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void ShowAllBooks()
        {
            Console.WriteLine("\nAvailable books:");
            List<Book> books = _bookRepository.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("No books found."); return;
            }
            int count = 1;

            foreach (Book book in books)
            {
                Console.WriteLine($"{count}. ISBN: {book.ISBN} | Title: {book.Title} | Author: {book.Author} | Quantity: {book.Quantity}");
                count++;
            }
            Console.WriteLine();
        }

        public void FindABook()
        {
            Console.WriteLine("Would you like to search with: ");
            Console.WriteLine("1. ISBN");
            Console.WriteLine("2. Title");
            Console.WriteLine("3. Author");

            string userChoice = Console.ReadLine();
            bool isbnI = false;
            bool isbnTA = false;
            Book book = new Book();
            List<Book> books = new List<Book>();

            switch (userChoice) 
            {
                case "1":
                    {
                        book = FindABookWithISBN();
                        isbnI = true;
                        break;
                    }
                case "2":
                    {
                        books = FindABookWithTitle();
                        isbnTA = true;
                        break;
                    }
                case "3":
                    {
                        books = FindABookWithAuthor();
                        isbnTA = true;
                        break;
                    }
                default: 
                    {
                        throw new InvalidChoice();
                    }
            }
            if (isbnI)
            {
                if (book == null)
                {
                    Console.WriteLine("No book found."); return;
                }
                Console.WriteLine($"ISBN: {book.ISBN} | Title: {book.Title} | Author: {book.Author} | Quantity: {book.Quantity}");
            }


            if (isbnTA)
            {
                if (books.Count == 0)
                {
                    Console.WriteLine("No book found."); return;
                }
                int count = 1;
                foreach (Book b in books)
                {
                    Console.WriteLine($"{count}. ISBN: {b.ISBN} | Title: {b.Title} | Author: {b.Author} | Quantity: {b.Quantity}");
                    count++;
                }
            }
        }

        public Book FindABookWithISBN()
        {
            Console.Write("Enter ISBN: "); 
            string isbn = Console.ReadLine();
            Book book = _bookRepository.GetBookByISBN(isbn);
            return book;
        }

        public List<Book> FindABookWithTitle()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            List<Book> validBooks = _bookRepository.GetBookByTitle(title);
            return validBooks;
            
        }

        public List<Book> FindABookWithAuthor()
        {
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            List<Book> validBooks = _bookRepository.GetBookByAuthor(author);
            return validBooks;
        }

        public void AddBook ()
        {
            List<Book> books = _bookRepository.GetAllBooks();
            string isbn = books.Count == 0 ? "1" : (int.Parse(books.Max(x => x.ISBN)) + 1).ToString();

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            bool isValid = int.TryParse(Console.ReadLine(), out int quantity);
            if (!isValid)
            {
                throw new FormatException();
            }
            if (_bookRepository.BookExists(isbn))
            {
                throw new BookExists();
            }
            Book book = new Book
            {
                ISBN = isbn,
                Title = title,
                Author = author,
                Quantity = quantity
            };

            _bookRepository.AddBook(book);
            Console.WriteLine("Book has been added.");
        }

        public void RemoveBook ()
        {
            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();
            if (!_bookRepository.BookExists(isbn))
            {
                throw new BookNotFound();
            }
            _bookRepository.DeleteBook(isbn);
            Console.WriteLine("The book has been removed.");
        }

        public void ManageBookQuantity()
        {
            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();
            if (!_bookRepository.BookExists(isbn))
            {
                throw new BookNotFound();
            }
            Console.WriteLine("Enter Updated quantity: ");
            bool isValid = int.TryParse(Console.ReadLine(), out int quantity);
            if (!isValid)
            {
                throw new FormatException();
            }
            _bookRepository.UpdateBookQuantity(isbn, quantity);
            Console.WriteLine("Book quantity has been updated.");
        }
    }
}
