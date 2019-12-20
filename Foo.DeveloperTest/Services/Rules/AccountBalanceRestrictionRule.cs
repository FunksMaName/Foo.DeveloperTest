using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Services.Rules
{
    public class AccountBalanceRestrictionRule : IPaymentRules
    {
        public bool Match(PaymentRuleInput paymentRule)
        {
            if (paymentRule.AllowedPaymentSchemes != AllowedPaymentSchemes.FasterPayments) return false;

            return paymentRule.AccountBalance < paymentRule.PaymentAmount;
        }
    } 
}
