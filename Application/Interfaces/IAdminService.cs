using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        void ShowBorrowRequests();
        void ManageBorrowRequests();
        void ApproveRequest(int borrowId);
        void RejectRequest(int borrowId);
        void ViewOverdueBooks();
        void SendOverdueNotifications();


    }
}
