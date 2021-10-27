﻿using DbFactory.Contracts;
using System.Configuration;
using System.Data;

namespace DbFactory
{
    public class SQLServer : IRelationDB
    {    
        public IDbConnection Connection { get; set; }

        public string GetConnectionString()
        {
            var a = ConfigurationManager.ConnectionStrings;

            return a[1].ConnectionString;
        }
    }
}
