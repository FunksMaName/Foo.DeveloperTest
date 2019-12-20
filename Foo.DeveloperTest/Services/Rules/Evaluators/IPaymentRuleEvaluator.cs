using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Services.Rules.Evaluators
{
    public interface IPaymentRuleEvaluator
    {
        bool ValidPaymentIntent(MakePaymentRequest paymentRequest, Account account);
    }
}
