using System;
using Moq;
namespace UnusualSpending.Tests.Mocks
{
    public class MockIDateTimeProvider : IDateTimeProvider
    {
        private readonly Mock<IDateTimeProvider> _mock = new Mock<IDateTimeProvider>();

        public MockIDateTimeProvider()
        {
            _mock.Setup(d => d.getDateTime()).Returns(It.IsAny<DateTime>());
        }

        public MockIDateTimeProvider ToReturn(DateTime dateTime)
        {
            _mock.Setup(d => d.getDateTime()).Returns(dateTime);
            return this;
        }

        public DateTime getDateTime()
        {
            return _mock.Object.getDateTime();
        }
    }
}
