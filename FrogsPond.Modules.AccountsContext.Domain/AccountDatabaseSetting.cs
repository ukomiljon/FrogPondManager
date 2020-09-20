using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.AccountsContext.Domain
{

    public class AccountDatabaseSettings : IAccountDatabaseSettings
    {
        public string AccountCollectionName { get; set; }
        public string RefreshTokenCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAccountDatabaseSettings
    {
        string AccountCollectionName { get; set; }
        string RefreshTokenCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
