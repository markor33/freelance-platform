namespace Web.Bff.Models
{
    public class Payment
    {
        public float Amount { get; private set; }
        public string Currency { get; private set; }
        public PaymentType Type { get; private set; }

        public Payment(GrpcJobManagement.Payment payment)
        {
            Amount = payment.Amount;
            Currency = payment.Currency;
            Type = (PaymentType)payment.Type;
        }
    }

    public enum PaymentType
    {
        FIXED_RATE,
        HOURLY_RATE
    }
}
