using System;
using System.Collections.Generic;
using System.Linq;

namespace UnusualSpending
{
    public class DetermineHighSpending : IDetermineHighSpending
    {
        public List<HighSpendingStatus> _HighSpendingStatuses { get; } = new List<HighSpendingStatus>();
        public DetermineHighSpending() {
            
        }
        public List<HighSpendingStatus> Compute(List<Payment> payments) 
        {
            if (!payments.Any() || payments.Count == 1) {
                return _HighSpendingStatuses;
            }

            var currentMonth = DateTime.Now.Month;
            var previousMonth = currentMonth == 1 ? 12 : currentMonth - 1;

            var foodTotalsInCurrentMonth = payments
                .Where(p => p.TransactionDate.Month.Equals(currentMonth) && p.Category.Equals(Category.Food))
                .Sum(p => p.Amount);
            
            var foodTotalsInPreviousMonth =  payments
                .Where(p => p.TransactionDate.Month.Equals(previousMonth) && p.Category.Equals(Category.Food))
                .Sum(p => p.Amount);

            var difference = foodTotalsInCurrentMonth - foodTotalsInPreviousMonth;

            var percentageDifference = 0.0m;

            if (foodTotalsInPreviousMonth > 0) {
                percentageDifference = foodTotalsInCurrentMonth / foodTotalsInPreviousMonth;
            }

            if (percentageDifference > 1.5m) {
                _HighSpendingStatuses.Add(new HighSpendingStatus{
                    Category = Category.Food,
                    Total = difference
                });
            }

            return _HighSpendingStatuses;

        }
    }
}