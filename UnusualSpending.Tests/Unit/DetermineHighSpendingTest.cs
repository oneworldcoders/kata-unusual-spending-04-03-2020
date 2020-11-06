using System;
using System.Collections.Generic;
using System.Linq;
using UnusualSpending.Model;
using UnusualSpending.Tests.Mocks;
using Xunit;

namespace UnusualSpending.Tests.Unit
{
    public class DetermineHighSpendingTest
    {
        [Fact]
        public void given_no_payments_exist_for_an_id() 
        {
            var payments = new List<Payment>();

            var determineHighSpending = new DetermineHighSpending(new MockIDateTimeProvider());
            var highSpendingStatuses = determineHighSpending.Compute(payments);
           
            Assert.Empty(highSpendingStatuses);
        }
        
        [Fact]
        public void given_one_payment_exists_for_an_id() 
        {
            var payments = new List<Payment>{
                new Payment()
            };

            var determineHighSpending = new DetermineHighSpending(new MockIDateTimeProvider());
            var highSpendingStatuses = determineHighSpending.Compute(payments);
           
            Assert.Empty(highSpendingStatuses);
        }
        
       
        [Fact]
        public void given_two_payments_exist_for_an_id_that_trigger_high_spending() 
        {
            var mockDateTimeProvider = new MockIDateTimeProvider();
            var mockCurrentDate = mockDateTimeProvider.ToReturn(new DateTime(2020, 04, 03));
            var currentDate = mockDateTimeProvider.getDateTime();

            var payments = new List<Payment>
            {
                new Payment
                {
                    Id = 1,
                    TransactionDate = currentDate,
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

            var determineHighSpending = new DetermineHighSpending(mockCurrentDate);
            var highSpendingStatuses = determineHighSpending.Compute(payments);

            var expectedHighSpendingStatus = new HighSpendingStatus {
                Category = Category.Food,
                Total = 150.00m
            };

            Assert.Single(highSpendingStatuses);
            Assert.Equal(expectedHighSpendingStatus.Category, highSpendingStatuses.ElementAt(0).Category);
            Assert.Equal(expectedHighSpendingStatus.Total, highSpendingStatuses.ElementAt(0).Total);
        }

        [Fact]
        public void given_four_payments_with_two_different_categories_for_an_id_then_trigger_high_spending_for_both_categories()
        {
            var mockDateTimeProvider = new MockIDateTimeProvider();
            var mockCurrentDate = mockDateTimeProvider.ToReturn(new DateTime(2020, 04, 03));
            var currentDate = mockDateTimeProvider.getDateTime();

            var mockDateTimeProviderTwo = new MockIDateTimeProvider();
            var mockCurrentDateTwo = mockDateTimeProviderTwo.ToReturn(new DateTime(2020, 03, 03));
            var previousDate = mockDateTimeProviderTwo.getDateTime();
            var payments = new List<Payment>
            {
                new Payment
                {
                    Id = 1,
                    TransactionDate = currentDate,
                    Category = Category.Food,
                    Amount = 200.00m,
                },
                new Payment
                {
                    Id = 1,
                    TransactionDate = currentDate,
                    Category = Category.Transport,
                    Amount = 50.00m,
                },
                new Payment
                {
                    Id = 1,
                    TransactionDate = previousDate,
                    Category = Category.Food,
                    Amount = 50.00m,
                },

                new Payment
                {
                    Id = 1,
                    TransactionDate = previousDate,
                    Category = Category.Transport,
                    Amount = 5.00m,
                }
            };

            var determineHighSpending = new DetermineHighSpending(mockCurrentDate);
            var highSpendingStatuses = determineHighSpending.Compute(payments);

            var expectedHighSpendingStatus = new List<HighSpendingStatus>
            {
                new HighSpendingStatus
                {
                    Category = Category.Food,
                    Total = 150.00m
                },
                new HighSpendingStatus
                {
                    Category = Category.Transport,
                    Total = 45.00m
                }
            };


            Assert.Equal(2, highSpendingStatuses.Count);
            Assert.Equal(expectedHighSpendingStatus[0].Category, highSpendingStatuses.ElementAt(0).Category);
            Assert.Equal(expectedHighSpendingStatus[0].Total, highSpendingStatuses.ElementAt(0).Total);
            Assert.Equal(expectedHighSpendingStatus[1].Category, highSpendingStatuses.ElementAt(1).Category);
            Assert.Equal(expectedHighSpendingStatus[1].Total, highSpendingStatuses.ElementAt(1).Total);
        }
    }
}