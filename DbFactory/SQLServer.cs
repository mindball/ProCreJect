using DbFactory.Contracts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;

namespace DbFactory
{
    public class SQLServer : IRelationDB
    {       
        //public SQLServer()
        //{
        //    using (var connection =
        //        new SqlConnection("Server=10.148.73.5;Database=SwiftDB;User=sa;Password=Q1w2e3r4"))
        //    {
        //        Console.WriteLine("Db connected!!!");
        //        this.Connection = connection;
        //    }           
        //}

        public IDbConnection  Connection { get; set; }        

        //Fa
    }
}
