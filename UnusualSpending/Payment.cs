using System;

namespace UnusualSpending
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; } 
        public Category Category { get; set; }
        public decimal Amount { get; set; }
    }
}