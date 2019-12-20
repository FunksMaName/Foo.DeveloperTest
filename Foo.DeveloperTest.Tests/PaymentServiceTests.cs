using ClearBank.DeveloperTest.Tests.Builders;
using FluentAssertions;
using Foo.DeveloperTest.Data;
using Foo.DeveloperTest.Services;
using Foo.DeveloperTest.Services.Rules.Evaluators;
using Foo.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _sut;

        private readonly Mock<IDataStore> _dataStoreMock;

        private readonly Mock<IPaymentRuleEvaluator> _rulesEvaluatorMock;

        public PaymentServiceTests()
        {
            var mockRepository = new MockRepository(MockBehavior.Strict);

            _rulesEvaluatorMock = mockRepository.Create<IPaymentRuleEvaluator>();
            _dataStoreMock = mockRepository.Create<IDataStore>();

            _sut = new PaymentService(_dataStoreMock.Object, _rulesEvaluatorMock.Object);
        }

        [Fact]
        public void MakePayment_WhenDataStoreReturnsNullAccount_ReturnInvalid()
        {
            // Arrange. 
            var request = this.Create();
            _dataStoreMock.Setup(d => d.GetAccount(request.DebtorAccountNumber)).Returns((Account) null);
            
            // Act.
            var result = _sut.MakePayment(request);

            // Assert.
            result.Success.Should().BeFalse("No account retrieved for the debtor account");
        }

        [Fact]
        public void MakePayment_WhenPaymentRequestIsNull_ReturnInvalid()
        {
            // Arrange / Act.
            var result = _sut.MakePayment(null);

            // Assert.
            result.Success.Should().BeFalse("The payment request was null");
        }

        private MakePaymentRequest Create()
        {
            return new MakePaymentRequestBuilder()
                .Amount(34.2m)
                .WithDebtorAccount("4019258876543")
                .WithPaymentScheme(PaymentScheme.Bacs).Create();
        }
    }
}
