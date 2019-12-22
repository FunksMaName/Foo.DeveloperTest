using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Services.Rules
{
    public class AccountStatusRestrictionRule : IPaymentRules
    {
        public bool Match(PaymentRuleInput paymentRule)
        {
            if (paymentRule == null) return false;

            return paymentRule.AccountStatus != AccountStatus.Live;
        }
    }
}
