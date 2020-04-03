using System;
using System.Collections.Generic;
using Xunit;

namespace UnusualSpending.Tests
{
    public class UnusualSpendingTest
    {
        [Fact]
        public void VerifyFetchPaymentsIsCalledOnce() {
            var mockPaymentRepository = new MockPaymentRepository();

            var spending = new Spending(mockPaymentRepository, new MockDetermineHighSpending(), new MockSendNotification());
            spending.Trigger(1);

            mockPaymentRepository.VerifyFetchPaymentsWasCalledExactlyOnce();
        }
        
        [Fact]
        public void VerifyComputeIsCalledOnce() {
            var mockPaymentRepository = new MockPaymentRepository();
            var mockDetermineHighSpending = new MockDetermineHighSpending();

            var spending = new Spending(mockPaymentRepository, mockDetermineHighSpending, new MockSendNotification());
            spending.Trigger(1);

            mockPaymentRepository.VerifyFetchPaymentsWasCalledExactlyOnce();
            mockDetermineHighSpending.VerifyComputeWasCelledExactlyOnce();
        }
        
        [Fact]
        public void VerifySendIsCalledOnce() {
            var mockPaymentRepository = new MockPaymentRepository();
            var mockDetermineHighSpending = new MockDetermineHighSpending();
            var mockSendNotification = new MockSendNotification();

            var spending = new Spending(mockPaymentRepository, mockDetermineHighSpending, mockSendNotification);
            spending.Trigger(1);

            mockPaymentRepository.VerifyFetchPaymentsWasCalledExactlyOnce();
            mockDetermineHighSpending.VerifyComputeWasCelledExactlyOnce();
        }

        [Fact]
        public void given_two_payments_that_trigger_high_spending_verify_correct_message_set() 
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
            
            var mockPaymentRepository = new MockPaymentRepository().WithThese(payments);
            var sendNotification = new SendNotification();
            var spending = new Spending(mockPaymentRepository, new DetermineHighSpending(), sendNotification);
            
            spending.Trigger(1);

            var expectedMessage = "Hi, you spent $150.00 more on Food Category";
            
            Assert.Equal(expectedMessage, sendNotification.Message);


        }
    }
}
