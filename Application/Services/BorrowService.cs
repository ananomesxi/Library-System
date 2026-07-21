using Application.Interfaces;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;

namespace Application.Services
{
    public class BorrowService : IBorrowService
    {
        #region DI
        private readonly IBookService _bookService;
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        public BorrowService (IBookService bookService, IBorrowRepository borrowRepository, IBookRepository bookRepository, IUserRepository userRepository)
        {
            _bookService = bookService;
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }
        #endregion
        public void BorrowRequest(int userId)
        {
            User user = _userRepository.GetUserById(userId);

            if (user is ClientUser client && client.Fine > 0)
            {
                Console.WriteLine("You cannot borrow a book until your fine is paid.");
                return;
            }
            Book book = _bookService.FindABookWithISBN();
            if (book == null)
            {
                Console.WriteLine("No book found.");
                return;
            }
            if (book.Quantity <= 0)
            {
                Console.WriteLine("This book is currently unavailable.");
                return;
            }
            List<BorrowRecord> records = _borrowRepository.GetAllBorrowRecords();
            BorrowRecord borrowRecord = new BorrowRecord
            {
                BorrowID = records.Count == 0 ? 1 : records.Max(x => x.BorrowID) + 1,
                UserID = userId,
                ISBN = book.ISBN,
                ReturnDate = DateTime.Now.AddMonths(1),
                Status = BorrowStatus.Pending
            };
            _borrowRepository.AddBorrowRecord(borrowRecord);
            Console.WriteLine("Your borrow request is pending.");
        }
        
        public void ReturnABook(int userId)
        {
            List <BorrowRecord> borrowedBooks = _borrowRepository.GetAllBorrowRecords().Where(b => b.UserID == userId && 
                                                                                                   b.Status ==BorrowStatus.Approved).ToList();
            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("You have no borrowed books.");
                return;
            }
            ShowBorrowedBooks (userId);
            
            Console.WriteLine("Choose a book to return: ");

            bool isValid = int.TryParse(Console.ReadLine(), out int userChoice);
            
            if (!isValid || userChoice < 1 || userChoice > borrowedBooks.Count)
            {
                throw new InvalidChoice();
            }
            userChoice--;
            Book book = _bookRepository.GetBookByISBN(borrowedBooks[userChoice].ISBN);
            _bookRepository.UpdateBookQuantity(borrowedBooks[userChoice].ISBN, book.Quantity + 1);
            _borrowRepository.UpdateBorrowStatus(borrowedBooks[userChoice].BorrowID, BorrowStatus.Returned);

            Console.WriteLine("Your book has been returned.");
        }

        public void ShowBorrowedBooks(int userId)
        {
            List<BorrowRecord> borrowedBooks = _borrowRepository.GetAllBorrowRecords().Where(b => b.UserID == userId &&
                                                                                                  b.Status == BorrowStatus.Approved).ToList();
            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("You have no borrowed books.");
                return;
            }
            Console.WriteLine("Your borrowed books:");

            for (int i=0;  i<borrowedBooks.Count; i++) 
            {
                Book book = _bookRepository.GetBookByISBN(borrowedBooks[i].ISBN);
                Console.WriteLine($"{i+1}. ISBN: {book.ISBN} | Title: {book.Title} | Author: {book.Author} | Return date: {borrowedBooks[i].ReturnDate:d/M/yyyy} ");
            }
        }
    }
}
