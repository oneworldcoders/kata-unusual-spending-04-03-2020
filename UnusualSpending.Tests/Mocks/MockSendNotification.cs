using System.Collections.Generic;
using Moq;
using UnusualSpending.Model;

namespace UnusualSpending.Tests.Mocks
{
    public class MockSendNotification : ISendNotification
    {
        private readonly Mock<ISendNotification> _mock = new Mock<ISendNotification>();

        public MockSendNotification() {
            _mock.Setup(s => s.Send(It.IsAny<List<HighSpendingStatus>>()));
        }
        public void Send(List<HighSpendingStatus> highSpendingStatuses) {
           _mock.Object.Send(highSpendingStatuses); 
        }

        public void VerifySendNotificationWasCalledExactlyOnce() {
            _mock.Verify(s => s.Send(It.IsAny<List<HighSpendingStatus>>()), Times.Once);
        }
    }
}