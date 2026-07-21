using Application.Interfaces;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
namespace Application.Services
{
    public class ClientService : IClientService
    {
        #region DI
        private readonly IBorrowRepository _borrowRepository;
        private readonly IUserRepository _userRepository;
        public ClientService(IBorrowRepository borrowRepository, IUserRepository userRepository)
        {
            _borrowRepository = borrowRepository;
            _userRepository = userRepository;
        }
        #endregion
        public void UpdateClientFine(ClientUser client)
        {
            List<BorrowRecord> borrowedBooks = _borrowRepository.GetAllBorrowRecords().Where(b => b.UserID == client.Id 
                                                                                               && b.Status == BorrowStatus.Approved).ToList();
            decimal totalFine = 0;
            if (borrowedBooks.Count == 0)
            {
                return;
            }
            foreach (var borrowRecord in borrowedBooks)
            {
                if (DateTime.Now.Date > borrowRecord.ReturnDate.Date)
                {
                    int lateDays = (DateTime.Now.Date - borrowRecord.ReturnDate.Date).Days;
                    totalFine += lateDays;
                }
            }
            client.Fine = totalFine;
            _userRepository.UpdateUser(client);
        }
        public void ViewFine(ClientUser client)
        {
            Console.WriteLine($"Your fine: {client.Fine}");
        }
    }
}
