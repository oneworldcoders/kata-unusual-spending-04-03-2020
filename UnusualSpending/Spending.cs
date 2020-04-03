namespace UnusualSpending
{
    public class Spending
    {
        public IPaymentsRepository PaymentsRepository { get; set; }
        public IDetermineHighSpending DetermineHighSpending { get; set; }
        public ISendNotification SendNotification { get; set; }
        public Spending(IPaymentsRepository paymentsRepository, IDetermineHighSpending determineHighSpending, ISendNotification sendNotification) 
        {
            PaymentsRepository = paymentsRepository;
            DetermineHighSpending = determineHighSpending;
            SendNotification = sendNotification;
        }
        public void Trigger(int id) {
            var payments = PaymentsRepository.FetchPayments(id);
            var highSpendingStatuses = DetermineHighSpending.Compute(payments);
            SendNotification.Send(highSpendingStatuses);
        }
    }
}