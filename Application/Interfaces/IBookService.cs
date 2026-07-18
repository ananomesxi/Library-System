using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IBookService
    {
        void ShowAllBooks();
        void FindABook();
        Book FindABookWithISBN();
        List<Book> FindABookWithTitle();
        List<Book> FindABookWithAuthor();
        void AddBook();
        void RemoveBook();
        void ManageBookQuantity();


    }
}
