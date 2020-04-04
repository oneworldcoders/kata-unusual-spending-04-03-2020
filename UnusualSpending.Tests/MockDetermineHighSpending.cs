using System.Collections.Generic;
using Moq;
using UnusualSpending.Model;

namespace UnusualSpending.Tests
{
    public class MockDetermineHighSpending : IDetermineHighSpending
    {
        private readonly Mock<IDetermineHighSpending> _mock = new Mock<IDetermineHighSpending>();

        public MockDetermineHighSpending() {
            _mock.Setup(c => c.Compute(It.IsAny<List<Payment>>())).Returns(new List<HighSpendingStatus>());
        }
        public List<HighSpendingStatus> Compute(List<Payment> payments) {
            return _mock.Object.Compute(payments);
        }

        public void VerifyComputeWasCelledExactlyOnce() {
            _mock.Verify(c => c.Compute(It.IsAny<List<Payment>>()), Times.Once);
        }
    }
}