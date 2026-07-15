using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IBorrowRepository
    {
        void AddBorrowRecord(BorrowRecord borrowRecord);
        void DeleteBorrowRecord(int borrowID);
        List<BorrowRecord> GetAllBorrowRecords();
        void SaveChanges(List<BorrowRecord> borrowRecords);
        void UpdateBorrowStatus(int borrowID, BorrowStatus borrowStatus);
    }
}
