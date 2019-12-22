using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Services.Rules
{
    public class PaymentSchemeRestrictionRule : IPaymentRules
    {
        public bool Match(PaymentRuleInput paymentRule)
        {
            if (paymentRule == null) return false;

            var paymentScheme = this.MapToInternalPaymentScheme(paymentRule.CreditorPaymentScheme);

            return !paymentRule.DebtorPaymentSchemes.HasFlag(paymentScheme);
        }

        /*
         This would ideally be an injectable mapper available outside the context of this class.
         */
        private AllowedPaymentSchemes MapToInternalPaymentScheme(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return AllowedPaymentSchemes.Bacs;
                case PaymentScheme.Chaps:
                    return AllowedPaymentSchemes.Chaps;
                case PaymentScheme.FasterPayments:
                    return AllowedPaymentSchemes.FasterPayments;
                default:
                    return AllowedPaymentSchemes.Unknown;
            } 
        }
    }
}
