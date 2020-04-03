using System;
using System.Collections.Generic;

namespace UnusualSpending
{
    public interface IPaymentsRepository
    {
        List<Payment> FetchPayments(int id);
    }
}
