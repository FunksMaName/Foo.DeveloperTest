using Foo.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Builders
{
    internal class MakePaymentRequestBuilder
    {
        private readonly MakePaymentRequest  _request = new MakePaymentRequest();

        public MakePaymentRequestBuilder WithPaymentScheme(PaymentScheme paymentScheme)
        {
            _request.PaymentScheme = paymentScheme;

            return this;
        }

        public MakePaymentRequestBuilder Amount(decimal amount)
        {
            _request.Amount = amount;

            return this;
        }

        public MakePaymentRequestBuilder WithDebtorAccount(string debtorAccountNumber)
        {
            _request.DebtorAccountNumber = debtorAccountNumber;

            return this;
        }

        public MakePaymentRequest Create()
        {
            return _request;
        }
    }
}
