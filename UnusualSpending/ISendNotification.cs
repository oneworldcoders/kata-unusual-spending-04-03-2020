using System.Collections.Generic;
using UnusualSpending.Model;

namespace UnusualSpending
{
    public interface ISendNotification
    {
        void Send(List<HighSpendingStatus> highSpendingStatuses);
    }
}