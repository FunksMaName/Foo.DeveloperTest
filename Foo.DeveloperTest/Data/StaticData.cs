using System.Collections.Generic;
using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Data
{
    public static class StaticData
    {
        public static IEnumerable<Account> Accounts()
        {
            yield return new Account { AccountNumber = "001", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 320.63m, Status = AccountStatus.Disabled};
            yield return new Account { AccountNumber = "002", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 520.63m, Status = AccountStatus.InboundPaymentsOnly };
            yield return new Account { AccountNumber = "003", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 920.63m, Status = AccountStatus.Live };

            yield return new Account { AccountNumber = "004", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 320.63m, Status = AccountStatus.Disabled };
            yield return new Account { AccountNumber = "005", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 520.63m, Status = AccountStatus.InboundPaymentsOnly };
            yield return new Account { AccountNumber = "006", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 920.63m, Status = AccountStatus.Live };

            yield return new Account { AccountNumber = "007", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 320.63m, Status = AccountStatus.Disabled };
            yield return new Account { AccountNumber = "008", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 520.63m, Status = AccountStatus.InboundPaymentsOnly };
            yield return new Account { AccountNumber = "009", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 920.63m, Status = AccountStatus.Live };
        }
    }
}
