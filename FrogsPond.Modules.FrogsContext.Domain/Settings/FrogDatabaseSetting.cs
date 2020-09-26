namespace FrogsPond.Modules.FrogsContext.Domain.Settings
{
    public class FrogDatabaseSettings : IFrogDatabaseSettings
    {
        public string FrogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IFrogDatabaseSettings
    {
        string FrogCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
