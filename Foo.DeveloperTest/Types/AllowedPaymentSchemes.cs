using System;

namespace Foo.DeveloperTest.Types
{
    [Flags]
    public enum AllowedPaymentSchemes
    {
        Unknown,
        FasterPayments = 1 << 0,
        Bacs = 1 << 1,
        Chaps = 1 << 2
    }
}
