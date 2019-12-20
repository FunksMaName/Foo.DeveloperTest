using System;
using System.Configuration;
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
            using (ThreadScopedLifestyle.BeginScope(Container))
            {
                var paymentService = Container.GetInstance<IPaymentService>();
                paymentService.MakePayment(new MakePaymentRequest());
            }
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
