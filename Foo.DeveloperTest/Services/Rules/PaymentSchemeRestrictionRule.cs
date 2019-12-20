namespace Foo.DeveloperTest.Services.Rules
{
    public class PaymentSchemeRestrictionRule : IPaymentRules
    {
        public bool Match(PaymentRuleInput paymentRule)
        {
            if (paymentRule == null) return false;

            return !paymentRule.AllowedPaymentSchemes.HasFlag(paymentRule.AllowedPaymentSchemes);
        }
    }
}
