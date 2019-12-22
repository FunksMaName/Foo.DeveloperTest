using FluentAssertions;
using Foo.DeveloperTest.Services.Rules;
using Foo.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Rules
{
    public class PaymentSchemeRestrictionRuleTests
    {
        private readonly PaymentSchemeRestrictionRule _sut;

        public PaymentSchemeRestrictionRuleTests()
        {
            _sut = new PaymentSchemeRestrictionRule();
        }

        [Fact]
        public void Map_WhenRuleObjectNull_NoMatch()
        {
            // Act / Assert.
            _sut.Match(null).Should().BeFalse("A null evaluation object was supplied");
        }

        [Theory]
        [InlineData(PaymentScheme.Chaps)]
        [InlineData(PaymentScheme.Bacs)]
        public void Map_WheAccountDoesNotSupportPaymentScheme_Match(PaymentScheme creditorPaymentScheme)
        {
            // Arrange.
            var input = new PaymentRuleInput
            {
                CreditorPaymentScheme = creditorPaymentScheme,
                DebtorPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            // Act / Assert.
            _sut.Match(input).Should().BeTrue($"{creditorPaymentScheme} is not enabled for the debtor account");
        }

        [Theory]
        [InlineData(PaymentScheme.FasterPayments, AllowedPaymentSchemes.FasterPayments)]
        [InlineData(PaymentScheme.Bacs, AllowedPaymentSchemes.Bacs)]
        [InlineData(PaymentScheme.Chaps, AllowedPaymentSchemes.Chaps)]
        public void Map_WheAccountSupportsPaymentScheme_DoNotMatch(
            PaymentScheme creditorPaymentScheme, 
            AllowedPaymentSchemes debtorAllowedPaymentSchemes)
        {
            // Arrange.
            var input = new PaymentRuleInput
            {
                CreditorPaymentScheme = creditorPaymentScheme,
                DebtorPaymentSchemes = debtorAllowedPaymentSchemes
            };

            // Act / Assert.
            _sut.Match(input).Should().BeFalse($"{creditorPaymentScheme} is enabled for the debtor account");
        }

        [Theory]
        [InlineData(PaymentScheme.Bacs, AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs)]
        [InlineData(PaymentScheme.FasterPayments, AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Chaps)]
        public void Map_WheAccountSupportsPaymentSchemeCombination_DoNotMatch(
            PaymentScheme creditorPaymentScheme,
            AllowedPaymentSchemes debtorAllowedPaymentSchemes)
        {
            // Arrange.
            var input = new PaymentRuleInput
            {
                CreditorPaymentScheme = creditorPaymentScheme,
                DebtorPaymentSchemes = debtorAllowedPaymentSchemes
            };

            // Act / Assert.
            _sut.Match(input).Should().BeFalse($"{creditorPaymentScheme} is enabled for the debtor account");
        }
    }
}
