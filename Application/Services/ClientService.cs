using Application.Interfaces;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ClientService
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IUserRepository _userRepository;
        public ClientService(IBorrowRepository borrowRepository, IUserRepository userRepository)
        {
            _borrowRepository = borrowRepository;
            _userRepository = userRepository;
        }
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
