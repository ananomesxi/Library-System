using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly string _booksPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..", "..", "..", "..","Repository","Data","Books.txt"));

        public void AddBook(Book book)
        {
            if (BookExists(book.ISBN))
            {
                throw new BookExists();
            }
            string line = JsonSerializer.Serialize(book); 
            File.AppendAllLines(_booksPath, new[] { line }); 
        }

        public bool BookExists(string isbn)
        {
            List<Book> books = GetAllBooks();

            return books.Any(b => b.ISBN == isbn);
        }

        public void DeleteBook(string isbn)
        {
            List<Book> books = GetAllBooks();
            Book book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
            {
                throw new BookNotFound();
            }
            books.Remove(book);
            SaveChanges(books);
        }

        public List<Book> GetAllBooks()
        {
            if (!File.Exists(_booksPath))
            {
                return new List<Book>(); // თუ ფაილი ვერ იპოვა ახალს შექმნის და დააბრუნებს
            }
            string[] lines = File.ReadAllLines(_booksPath);


            List<Book> books = new List<Book>();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) // ხაზი შეიძლება გამოტოვებული/ცარიელი იყოს
                {
                    continue;
                }
                Book book = JsonSerializer.Deserialize<Book>(line); 
                books.Add(book);
            }
            return books;
        }

        public Book GetBookByISBN(string isbn)
        {
            List<Book> books = GetAllBooks();
            Book book = books.FirstOrDefault(b => b.ISBN == isbn);
            
            return book;
        }

        public List<Book> GetBookByTitle(string title)
        {
            List<Book> books = GetAllBooks();

            List<Book> validBooks = books.Where(book => book.Title.Equals(title)).ToList();

            return validBooks;
        }

        public List<Book> GetBookByAuthor(string author)
        {
            List<Book> books = GetAllBooks();

            List<Book> validBooks = books.Where(book => book.Author.Equals(author)).ToList();
           
            return validBooks;
        }

        public void SaveChanges(List<Book> books)
        {
            File.Delete(_booksPath);
            File.AppendAllLines(_booksPath, books.Select(b => JsonSerializer.Serialize(b)));
        }

        public void UpdateBookQuantity(string isbn, int newQuantity)
        {
            List<Book> books = GetAllBooks();
            Book book = books.FirstOrDefault(b => b.ISBN == isbn);

            if (book == null)
            {
                throw new BookNotFound();
            }
            book.Quantity = newQuantity;
            SaveChanges(books);
        }
    }
}
