using Foo.DeveloperTest.Data;
using Foo.DeveloperTest.Services.Rules.Evaluators;
using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Services
{
    /// <summary>
    /// An implementation of the <see cref="IPaymentService" /> contract.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly IDataStore _dataStore;
        private readonly IPaymentRuleEvaluator _paymentRuleEvaluator;

        /// <summary>
        /// Instantiates a new instance of the Payment Service.
        /// </summary>
        public PaymentService(IDataStore dataStore, IPaymentRuleEvaluator paymentRuleEvaluator)
        {
            _dataStore = dataStore;
            _paymentRuleEvaluator = paymentRuleEvaluator;
        }


        /// <summary>
        /// Actions a payment request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// An instance of <see cref="MakePaymentResult" /> indicating whether the payment was successful. 
        /// </returns>
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            if (request == null) return result;

            var account = _dataStore.GetAccount(request.DebtorAccountNumber);

            if (account == null) return result;
            if (!_paymentRuleEvaluator.ValidPaymentIntent(request, account)) return result;

            account.Balance -= request.Amount;
            _dataStore.UpdateAccount(account);
            result.Success = true;

            return result;
        }
    }
}
