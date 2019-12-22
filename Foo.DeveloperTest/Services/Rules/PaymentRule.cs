using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Services.Rules
{
    public class PaymentRuleInput
    {
        public  decimal AccountBalance { get; set; }

        public  AccountStatus AccountStatus { get; set; }

        public AllowedPaymentSchemes DebtorPaymentSchemes { get; set; }

        public PaymentScheme CreditorPaymentScheme { get; set; }

        public decimal PaymentAmount { get; set; }
    }
}
