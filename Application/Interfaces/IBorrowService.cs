using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IBorrowService
    {
        void BorrowRequest(int userId); 
        void ReturnABook(int userId);
        void ShowBorrowedBooks(int userId, bool hasBooks);

    }
}
