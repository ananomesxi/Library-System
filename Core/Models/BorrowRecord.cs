using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class BorrowRecord
    {
        public string BorrowID { get; set; }
        public int UserID { get; set; }
        public string ISBN { get; set; }
        public DateTime ReturnDate { get; set; }
        public BorrowStatus Status { get; set; }

    }
}
