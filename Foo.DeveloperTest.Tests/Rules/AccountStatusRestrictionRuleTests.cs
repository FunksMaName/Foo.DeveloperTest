using FluentAssertions;
using Foo.DeveloperTest.Services.Rules;
using Foo.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Rules
{
    public class AccountStatusRestrictionRuleTests
    {
        private readonly AccountStatusRestrictionRule _sut;

        public AccountStatusRestrictionRuleTests()
        {
           _sut = new AccountStatusRestrictionRule();
        }

        [Fact]
        public void Map_WhenRuleObjectNull_NoMatch()
        {
            // Act / Assert.
            _sut.Match(null).Should().BeFalse("A null evaluation object was supplied");
        }

        [Theory]
        [InlineData(AccountStatus.Disabled)]
        [InlineData(AccountStatus.InboundPaymentsOnly)]
        public void Match_WhenAccountStatusIsNotLive_Match(AccountStatus accountStatus)
        {
            // Arrange.
            var rule = new PaymentRuleInput
            {
                AccountStatus = accountStatus
            };

            // Act / Assert.
            _sut.Match(rule).Should().BeTrue($"The {nameof(AccountStatus)} value of '{accountStatus}' is not in a supported status of {AccountStatus.Live}");
        }

        [Fact]
        public void Match_WhenAccountStatusIsLive_DoNotMatch()
        {
            // Arrange.
            var rule = new PaymentRuleInput
            {
                AccountStatus = AccountStatus.Live
            };

            // Act / Assert.
            _sut.Match(rule).Should().BeFalse($"The {nameof(AccountStatus)} value of '{AccountStatus.Live}' is in a supported status");
        }
    }
}
