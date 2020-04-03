using System.Collections.Generic;

namespace UnusualSpending
{
    public interface IDetermineHighSpending
    {
        List<HighSpendingStatus> Compute(List<Payment> payments);
    }
}