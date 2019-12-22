using FluentAssertions;
using Foo.DeveloperTest.Services.Rules;
using Foo.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Rules
{
    public class AccountBalanceRestrictionRuleTests
    {
        private readonly AccountBalanceRestrictionRule _sut;

        public AccountBalanceRestrictionRuleTests()
        {
           _sut = new AccountBalanceRestrictionRule();
        }

        [Fact]
        public void Map_WhenRuleObjectNull_NoMatch()
        {
            // Act / Assert.
            _sut.Match(null).Should().BeFalse("A null evaluation object was supplied");
        }

        [Fact]
        public void Match_WhenAccountHasFasterPaymentFlagAndInsufficientBalance_Match()
        {
            // Arrange.
            var rule = new PaymentRuleInput
            {
                DebtorPaymentSchemes = AllowedPaymentSchemes.FasterPayments, 
                CreditorPaymentScheme = PaymentScheme.FasterPayments, 
                AccountBalance = 60.00m,
                PaymentAmount = 80.45m
            };

            // Act / Assert.
            _sut.Match(rule).Should().BeTrue("Insufficient balance on debtor account to fulfill payment");
        }

        [Fact]
        public void Match_WhenAccountHasFasterPaymentFlagAndSufficientBalance_DoNotMatch()
        {
            // Arrange.
            var rule = new PaymentRuleInput
            {
                DebtorPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                CreditorPaymentScheme = PaymentScheme.FasterPayments,
                AccountBalance = 80.45m,
                PaymentAmount = 60.00m
            };

            // Act / Assert.
            _sut.Match(rule).Should().BeFalse("Sufficient balance on debtor account to fulfill payment");
        }
    }
}
