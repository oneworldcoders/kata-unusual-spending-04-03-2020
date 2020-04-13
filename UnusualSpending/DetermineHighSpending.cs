using System;
using System.Collections.Generic;
using System.Linq;
using UnusualSpending.Model;

namespace UnusualSpending
{
    public class DetermineHighSpending : IDetermineHighSpending
    {
        private List<HighSpendingStatus> _highSpendingStatuses { get; } = new List<HighSpendingStatus>();
        private List<Payment> _payments { get; set; } = new List<Payment>();
        private int _currentMonth { get; } = DateTime.Now.ToLocalTime().Month;
        private int _previousMonth => _currentMonth == (int) Month.Dec ? (int) Month.Jan : _currentMonth - 1; 

        public DetermineHighSpending() {
        }

        public List<HighSpendingStatus> Compute(List<Payment> payments) 
        {
            _payments = payments;

            if (!payments.Any() || payments.Count == 1) 
            {
                return _highSpendingStatuses;
            }
            
            return GetHighSpendingStatuses();
        }

        private List<HighSpendingStatus> GetHighSpendingStatuses() 
        {
            var allCategories = Enum.GetValues(typeof(Category));
            foreach (var category in allCategories) 
            {
                var categoryTotalsThisMonth = ComputeMonthTotalsFor((Category) category, _currentMonth);
                var categoryTotalsLastMonth = ComputeMonthTotalsFor((Category) category, _previousMonth);

                if (categoryTotalsLastMonth > 0 && (categoryTotalsThisMonth / categoryTotalsLastMonth) > 1.5m) 
                {
                    _highSpendingStatuses.Add(new HighSpendingStatus
                    {
                        Category = (Category) category,
                        Total = categoryTotalsThisMonth - categoryTotalsLastMonth
                    });
                }
            }
            return _highSpendingStatuses;
        }
        private decimal ComputeMonthTotalsFor(Category category, int month) 
        {
            return _payments
                .Where(p => p.TransactionDate.Month.Equals(month) && p.Category.Equals(category))
                .Sum(p => p.Amount); 
        }
    }
}