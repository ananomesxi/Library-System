using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        void AddBook(Book book);
        void DeleteBook(string isbn);
        void UpdateBookQuantity(string isbn, int newQuantity);
        Book GetBookByISBN(string isbn);
        List<Book> GetBookByTitle(string title);
        List<Book> GetBookByAuthor (string author);
        bool BookExists (string isbn);
        public void SaveChanges(List<Book> books);

    }
}
