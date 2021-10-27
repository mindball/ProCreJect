using DbFactory.Contracts;
using System;

namespace DbFactory
{
    public class RelationDBFactory
    {
        public IRelationDB Create(string dbName)
        {
            if (dbName == "SQLServerDB")
                return new SQLServer();
           
            throw new ArgumentException("dbName is invalid");
        }
    }
}
