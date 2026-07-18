using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Repository
{
    public class BorrowRepository : IBorrowRepository
    {
        // აქ დიდი ალბათობით შევცვლი/ჩავამატებ რაღაცეებს. მთავარი ფუნქციონალის აგების პროცესში მივხვდები რა არის საჭირო
        // თუმცა ჯერჯერობით იყოს ბაზად ეს

        private readonly string _borrowPath = "D:\\Library System\\Repository\\Data\\BorrowRecord.txt";
        public void AddBorrowRecord(BorrowRecord borrowRecord)
        {
            string line = JsonSerializer.Serialize(borrowRecord);
            File.AppendAllLines(_borrowPath, new[] { line });
        }

        public void DeleteBorrowRecord(int borrowID)
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            BorrowRecord borrowRecord = borrowRecords.FirstOrDefault(b => b.BorrowID == borrowID);
            if (borrowRecord == null)
            {
                throw new BorrowRecordNotFound();
            }
            borrowRecords.Remove(borrowRecord);
            SaveChanges(borrowRecords);
        }

        public List<BorrowRecord> GetAllBorrowRecords()
        {
            if (!File.Exists(_borrowPath))
            {
                return new List<BorrowRecord>(); 
            }
            string[] lines = File.ReadAllLines(_borrowPath);


            List<BorrowRecord> borrowRecords = new List<BorrowRecord>();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) 
                {
                    continue;
                }
                BorrowRecord borrowRecord = JsonSerializer.Deserialize<BorrowRecord>(line);
                borrowRecords.Add(borrowRecord);
            }
            return borrowRecords;
        }

        public void SaveChanges(List<BorrowRecord> borrowRecords)
        {
            File.Delete(_borrowPath);
            File.AppendAllLines(_borrowPath, borrowRecords.Select(b => JsonSerializer.Serialize(b)));
        }

        public void UpdateBorrowStatus(int borrowID, BorrowStatus borrowStatus)
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            BorrowRecord borrowRecord = borrowRecords.FirstOrDefault(b => b.BorrowID == borrowID);
            if (borrowRecord == null)
            {
                throw new BorrowRecordNotFound();
            }
            borrowRecord.Status = borrowStatus;
            SaveChanges(borrowRecords);
        }

        public BorrowRecord GetBorrowRequestByBorrowID(int borrowId)
        {
            List<BorrowRecord> borrowRecords = GetAllBorrowRecords();
            BorrowRecord borrowRecord = borrowRecords.FirstOrDefault(b => b.BorrowID == borrowId);

            return borrowRecord;
        }

        public List<BorrowRecord> GetOverdueBooks()
        {
            List<BorrowRecord> overdue = GetAllBorrowRecords().Where(o => o.Status == BorrowStatus.Approved && o.ReturnDate.Date < DateTime.Now.Date).ToList();
            return overdue;
        }
    }
}
