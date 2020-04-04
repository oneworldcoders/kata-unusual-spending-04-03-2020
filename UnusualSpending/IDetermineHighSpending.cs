using System.Collections.Generic;
using UnusualSpending.Model;

namespace UnusualSpending
{
    public interface IDetermineHighSpending
    {
        List<HighSpendingStatus> Compute(List<Payment> payments);
    }
}