using Foo.DeveloperTest.Types;

namespace Foo.DeveloperTest.Data
{
    public interface IDataStore
    {
        /// <summary>
        /// Retrieves the details of an <see cref="Account"/> from the data store. 
        /// </summary>
        /// <param name="accountNumber">
        /// The uniquely assigned account identifier for the account that is to be retrieved.
        /// </param>
        /// <returns>
        ///  An instance of <see cref="Account"/>, <code>null</code> when not found.
        /// </returns>
        Account GetAccount(string accountNumber);

        /// <summary>
        /// Updates the details of an <see cref="Account"/>. 
        /// </summary>
        /// <param name="account">
        /// The <see cref="Account" /> instance that is to eb updated. 
        /// </param>
        void UpdateAccount(Account account);
    }
}
