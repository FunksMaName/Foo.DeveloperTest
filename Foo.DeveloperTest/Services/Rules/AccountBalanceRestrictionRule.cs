namespace Foo.DeveloperTest.Services.Rules
{
    public class AccountBalanceRestrictionRule : IPaymentRules
    {
        public bool Match(PaymentRuleInput paymentRule)
        {
            if (paymentRule == null) return false;

            return paymentRule.AccountBalance < paymentRule.PaymentAmount;
        }
    } 
}
