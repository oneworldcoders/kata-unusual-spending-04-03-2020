using System.Collections.Generic;

namespace UnusualSpending
{
    public interface ISendNotification
    {
        void Send(List<HighSpendingStatus> highSpendingStatuses);
    }
}