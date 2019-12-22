using System.Collections.Generic;
using System.Linq;
using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Services.Rules.Evaluators
{
    public sealed class PaymentRuleEvaluator : IPaymentRuleEvaluator
    {
        private readonly IEnumerable<IPaymentRules> _rules;

        public PaymentRuleEvaluator(IEnumerable<IPaymentRules> rules)
        {
            _rules = rules;
        }

        public bool ValidPaymentIntent(MakePaymentRequest paymentRequest, Account account)
        {
            var input = new PaymentRuleInput
            {
                AccountStatus = account.Status,
                DebtorPaymentSchemes = account.AllowedPaymentSchemes,
                PaymentAmount = paymentRequest.Amount,
                CreditorPaymentScheme = paymentRequest.PaymentScheme,
                AccountBalance = account.Balance
            };

            return !_rules.Any(r => r.Match(input));
        }
    }
}
