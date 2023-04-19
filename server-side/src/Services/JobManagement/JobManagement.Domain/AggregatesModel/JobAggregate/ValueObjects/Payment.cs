using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects
{
    public class Payment : ValueObject
    {
        public float Amount { get; private set; }
        public string Currency { get; private set; }
        public PaymentType PaymentType { get; private set; }

        public Payment() { }

        public Payment(float amount, string currency, PaymentType paymentType)
        {
            Amount = amount;
            Currency = currency;
            PaymentType = paymentType;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
            yield return PaymentType;
        }
    }
}
