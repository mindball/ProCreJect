using DbFactory.Contracts;
using System.Data.SqlClient;
using System;

namespace DbFactory
{
    public class SQLServer : IRelationDB
    {       
        public SQLServer()
        {
            using (this.Connection =
                new SqlConnection("Server=10.148.73.5;Database=SwiftDB;User=sa;Password=Q1w2e3r4;"))
            {
                Console.WriteLine("Db connected!!!");
            }           
        }

        public SqlConnection Connection { get; set; }

        //Failure App.Config 
    }
}
