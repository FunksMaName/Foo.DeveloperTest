using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Data
{
    /// <summary>
    /// An implementation of <see cref="IDataStore"/> utilizing Sql server/Cosmos/.. repository.
    /// </summary>
    public class AccountDataStore : IDataStore
    {
        /// <inheritdoc cref="IDataStore" />.
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        /// <inheritdoc cref="IDataStore" />.
        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
