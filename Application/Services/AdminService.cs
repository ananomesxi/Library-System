using Application.Interfaces;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        #region DI
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public AdminService(IBorrowRepository borrowRepository, IBookRepository bookRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        #endregion
        public void ShowBorrowRequests()
        {
            List<BorrowRecord> borrowRequests = _borrowRepository.GetAllBorrowRecords().Where(x => x.Status == BorrowStatus.Pending).ToList();
            if (borrowRequests.Count == 0)
            {
                Console.WriteLine("There are no pending borrow requests."); return;
            }
            for (int i = 0; i < borrowRequests.Count; i++)
            {
                Console.WriteLine($"Borrow ID: {borrowRequests[i].BorrowID} | User ID: {borrowRequests[i].UserID} | " +
                                  $"ISBN: {borrowRequests[i].ISBN} | Return date: {borrowRequests[i].ReturnDate:d/M/yyyy} | Status: Pending");

            }
        }

        public void ManageBorrowRequests()
        {
            

            while (true)
            {
                ShowBorrowRequests();
                
                Console.Write("Enter Borrow ID (or 0 to exit): ");
                int borrowId = int.Parse(Console.ReadLine());
                if (borrowId == 0)
                {
                    break;
                }
                BorrowRecord borrowRecord = _borrowRepository.GetBorrowRequestByBorrowID(borrowId);
                if (borrowRecord == null)
                {
                    Console.WriteLine("Borrow record not found."); continue;
                }
                if (borrowRecord.Status != BorrowStatus.Pending)
                {
                    Console.WriteLine("This request has already been processed.");
                    continue;
                }
                Console.WriteLine("1. Approve");
                Console.WriteLine("2. Reject");
                Console.WriteLine("3. Leave pending");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            ApproveRequest(borrowId);
                            break;
                        }
                    case 2:
                        {
                            RejectRequest(borrowId);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Request left pending.");
                            break;
                        }
                    default:
                        {
                            throw new InvalidChoice();
                            
                        }
                }

            }
        }

        public void ApproveRequest(int borrowId)
        {
            _borrowRepository.UpdateBorrowStatus(borrowId, BorrowStatus.Approved);
            BorrowRecord borrowRecord = _borrowRepository.GetBorrowRequestByBorrowID(borrowId);
            Book book = _bookRepository.GetBookByISBN(borrowRecord.ISBN);
            _bookRepository.UpdateBookQuantity(book.ISBN, book.Quantity - 1);
            Console.WriteLine("Request has been approved.");
        }
        public void RejectRequest(int borrowId)
        {
            _borrowRepository.UpdateBorrowStatus(borrowId, BorrowStatus.Rejected);
            Console.WriteLine("Request has been rejected.");
        }

        public void ViewOverdueBooks()
        {
            List<BorrowRecord> overdue = _borrowRepository.GetOverdueBooks();
            if (overdue.Count == 0)
            {
                Console.WriteLine("There are no overdue books"); return;
            }
            foreach (var record in overdue)
            {
                User user = _userRepository.GetUserById(record.UserID);
                Book book = _bookRepository.GetBookByISBN(record.ISBN);

                int lateDays = (DateTime.Now.Date - record.ReturnDate.Date).Days;

                Console.WriteLine($"User: {user.Username} | Book: {book.Title} | ISBN: {book.ISBN} | Due date: {record.ReturnDate:d/M/yyyy} | Late days: {lateDays}");
            }
        }

        public void SendOverdueNotifications()
        {
            List<BorrowRecord> overdue = _borrowRepository.GetOverdueBooks();
            if (overdue.Count == 0)
            {
                Console.WriteLine("There are no overdue books.");
                return;
            }

            foreach (var record in overdue)
            {
                User user = _userRepository.GetUserById(record.UserID);
                Book book = _bookRepository.GetBookByISBN(record.ISBN);
                if (user == null || book == null)
                {
                    continue;
                }
                int lateDays = (DateTime.Now.Date - record.ReturnDate.Date).Days;
                _emailService.SendEmail(user.Email, "Overdue Book", $"The book '{book.Title}' is overdue by {lateDays} days. Please return it.");
                Console.WriteLine($"Notification sent to {user.Username}.");
            }
        }



    }
}
