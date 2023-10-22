
using System.Text.Json.Serialization;

namespace JobSearch.Abstractions.Model
{
    public class Payment
    {
        public float Amount { get; private set; }
        public string Currency { get; private set; }
        public PaymentType Type { get; private set; }

        [JsonConstructor]
        public Payment(float amount, string currency, PaymentType type)
        {
            Amount = amount;
            Currency = currency;
            Type = type;
        }
    }
}
