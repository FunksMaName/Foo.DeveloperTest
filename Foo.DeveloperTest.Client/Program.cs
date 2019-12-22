using System;
using System.Configuration;
using System.Linq;
using Foo.DeveloperTest.Data;
using Foo.DeveloperTest.Services;
using Foo.DeveloperTest.Services.Rules;
using Foo.DeveloperTest.Services.Rules.Evaluators;
using Foo.DeveloperTest.Types;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Foo.DeveloperTest.Client
{
    class Program
    {
        private static readonly Container Container;

        static Program()
        {
            Container = new Container { Options = { DefaultScopedLifestyle = new ThreadScopedLifestyle() }};
            RegisterDependencies();
        }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
           Start();
        }

        private static void Start()
        {
            PrintHeaders();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Enter an account number");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            var accountNumber = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nEnter a payment amount");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            var paymentAmount = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nEnter a payment scheme");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            var paymentScheme = Console.ReadLine();

            var account = StaticData.Accounts().FirstOrDefault(a => a.AccountNumber.Equals(accountNumber ?? ""));
            decimal.TryParse(paymentAmount, out var amount);
            Enum.TryParse(paymentScheme, true, out PaymentScheme scheme);

            if (amount <= 0 || account == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Invalid input data\n\n");
                Start();
            }

            using (ThreadScopedLifestyle.BeginScope(Container))
            {
                var paymentService = Container.GetInstance<IPaymentService>();
                // ReSharper disable once PossibleNullReferenceException
                var result = paymentService.MakePayment(new MakePaymentRequest { Amount = amount, DebtorAccountNumber = account.AccountNumber, CreditorAccountNumber = "230", PaymentScheme = scheme });

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n================================");
                Console.WriteLine($"Success: {result.Success}");
                Console.WriteLine("================================");
                Console.ReadLine();
            }
        }

        private static void PrintHeaders()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("================================");
            Console.WriteLine("Supported Account Configurations:");
            Console.WriteLine("================================");
            Console.WriteLine("No.\t\tScheme\t\tBalance\t\tStatus");
            Console.ForegroundColor = ConsoleColor.Cyan;

            foreach (var account in StaticData.Accounts())
            {
                Console.WriteLine($"{account.AccountNumber}\t\t{account.AllowedPaymentSchemes}\t\t{account.Balance}\t\t{account.Status}");
            }

            Console.WriteLine();
        }

        private static void RegisterDependencies()
        {
            RegisterDataStores();
            Container.Register<IPaymentService, PaymentService>(Lifestyle.Scoped);

            Container.Collection.Register<IPaymentRules>(
                typeof(AccountBalanceRestrictionRule),
                typeof(PaymentSchemeRestrictionRule),
                typeof(AccountStatusRestrictionRule));

            Container.Register<IPaymentRuleEvaluator, PaymentRuleEvaluator>(Lifestyle.Singleton);

            Container.Verify();
        }

        private static void RegisterDataStores()
        {
            var dataStoreKey = ConfigurationManager.AppSettings["DataStoreType"];

            if (Enum.TryParse(dataStoreKey, true, out DataStoreKind dataStoreKind))
            {
                switch (dataStoreKind)
                {
                    case DataStoreKind.AccountData:
                        Container.Register<IDataStore, AccountDataStore>(Lifestyle.Scoped);
                        return;
                    case DataStoreKind.Backup:
                        Container.Register<IDataStore, BackupAccountDataStore>(Lifestyle.Scoped);
                        return;
                }
            }

            throw new ArgumentException("Please configure a supported backing datastore in the application settings file");
        }
    }
}
