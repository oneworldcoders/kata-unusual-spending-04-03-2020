using System;
using System.Collections.Generic;
using System.Linq;

namespace UnusualSpending
{
    public class DetermineHighSpending : IDetermineHighSpending
    {
        private List<HighSpendingStatus> _highSpendingStatuses { get; } = new List<HighSpendingStatus>();
        private List<Payment> _payments { get; set; } = new List<Payment>();
        private int _currentMonth { get; } = DateTime.Now.ToLocalTime().Month;
        private int _previousMonth { get; set; }

        public DetermineHighSpending() {
            _previousMonth = SetPreviousMonth();
        }

        public List<HighSpendingStatus> Compute(List<Payment> payments) 
        {
            _payments = payments;
        
            if (!payments.Any() || payments.Count == 1) {
                return _highSpendingStatuses;
            }
            
            var foodTotalsInCurrentMonth = ComputeMonthTotalsFor(Category.Food, _currentMonth);
            var foodTotalsInPreviousMonth = ComputeMonthTotalsFor(Category.Food, _previousMonth);

            var difference = foodTotalsInCurrentMonth - foodTotalsInPreviousMonth;

            var percentageDifference = 0.0m;

            if (foodTotalsInPreviousMonth > 0) {
                percentageDifference = foodTotalsInCurrentMonth / foodTotalsInPreviousMonth;
            }

            if (percentageDifference > 1.5m) {
                _highSpendingStatuses.Add(new HighSpendingStatus{
                    Category = Category.Food,
                    Total = difference
                });
            }

            return _highSpendingStatuses;
        }

        private int SetPreviousMonth() 
        {
            return _currentMonth == (int) Month.Dec
                ? (int) Month.Jan
                : _currentMonth - 1;
        }

        private decimal ComputeMonthTotalsFor(Category category, int month) 
        {
           return _payments
                .Where(p => p.TransactionDate.Month.Equals(month) && p.Category.Equals(category))
                .Sum(p => p.Amount); 
        }
        
        
    }
}