using System;
using System.Collections.Generic;
using UnusualSpending.Model;

namespace UnusualSpending
{
    public interface IPaymentsRepository
    {
        List<Payment> FetchPayments(int id);
    }
}
