namespace Foo.DeveloperTest.Services.Rules
{
    public interface IPaymentRules
    {
        bool Match(PaymentRuleInput paymentRule);
    }
}
