namespace DbFactory.Contracts
{
    public interface IDBFactory
    {
        //INoSQLDB CreateNoSQLDB(string dbName);
        IRelationDB CreateRelationDB(string dbName);
    }
}
