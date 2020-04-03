using System.Collections.Generic;
using Moq;

namespace UnusualSpending.Tests
{
    public class MockPaymentRepository : IPaymentsRepository
    {
        private readonly Mock<IPaymentsRepository> _mock = new Mock<IPaymentsRepository>();

        public MockPaymentRepository() {
            _mock.Setup(f => f.FetchPayments(It.IsAny<int>())).Returns(new List<Payment>());
        }

        public MockPaymentRepository WithThese(List<Payment> payments) 
        {
            _mock.Setup(f => f.FetchPayments(It.IsAny<int>())).Returns(payments);
            return this;
        }
        
        public List<Payment> FetchPayments(int id) {
            return _mock.Object.FetchPayments(id);
        }

        public void VerifyFetchPaymentsWasCalledExactlyOnce() {
            _mock.Verify(c => c.FetchPayments(It.IsAny<int>()), Times.Once);
        }
    }
}