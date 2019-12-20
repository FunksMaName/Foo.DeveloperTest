using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.DeveloperTest.Services.Rules
{
    public interface IPaymentRules
    {
        bool Match(PaymentRuleInput paymentRule);
    }
}
