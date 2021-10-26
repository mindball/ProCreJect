using DbFactory.Contracts;

namespace DbFactory
{
    public class DBFactory : IDBFactory
    {
        public IRelationDB CreateRelationDB(string dbName)
        {
            RelationDBFactory factory = new RelationDBFactory();
            return factory.Create(dbName);
        }
    }
}
