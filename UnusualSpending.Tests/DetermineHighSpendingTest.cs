using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnusualSpending.Tests
{
    public class DetermineHighSpendingTest
    {
        [Fact]
        public void given_no_payments_exist_for_an_id() 
        {
            var payments = new List<Payment>();

            var determineHighSpending = new DetermineHighSpending();
            var highSpendingStatuses = determineHighSpending.Compute(payments);
           
            Assert.Empty(highSpendingStatuses);
        }
        
        [Fact]
        public void given_one_payment_exists_for_an_id() 
        {
            var payments = new List<Payment>{
                new Payment()
            };

            var determineHighSpending = new DetermineHighSpending();
            var highSpendingStatuses = determineHighSpending.Compute(payments);
           
            Assert.Empty(highSpendingStatuses);
        }
        
       
        [Fact]
        public void given_two_payments_exist_for_an_id_that_trigger_high_spending() 
        {
            var payments = new List<Payment>
            {
                new Payment
                {
                    Id = 1,
                    TransactionDate = new DateTime(2020, 04, 03),
                    Category = Category.Food,
                    Amount = 200.00m,
                },
               
                new Payment
                {
                    Id = 1,
                    TransactionDate = new DateTime(2020, 03, 03),
                    Category = Category.Food,
                    Amount = 50.00m,
                }
            };

            var determineHighSpending = new DetermineHighSpending();
            var highSpendingStatuses = determineHighSpending.Compute(payments);

            var expectedHighSpendingStatus = new HighSpendingStatus {
                Category = Category.Food,
                Total = 150.00m
            };

            Assert.Single(highSpendingStatuses);
            Assert.Equal(expectedHighSpendingStatus.Category, highSpendingStatuses.ElementAt(0).Category);
            Assert.Equal(expectedHighSpendingStatus.Total, highSpendingStatuses.ElementAt(0).Total);
        }
    }
}