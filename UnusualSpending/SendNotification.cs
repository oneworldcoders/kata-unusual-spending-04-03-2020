using System.Collections.Generic;

namespace UnusualSpending
{
    public class SendNotification : ISendNotification
    {
        public string Message { get; private set; } = string.Empty;
        public SendNotification(){}
        public void Send(List<HighSpendingStatus> highSpendingStatuses) {
            foreach (var highSpendingStatus in highSpendingStatuses) {
                Message += $"Hi, you spent ${highSpendingStatus.Total} more on {highSpendingStatus.Category} Category";
            }    
        }
    }
}