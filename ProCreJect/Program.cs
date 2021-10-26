using DbFactory;
using DbFactory.Contracts;
using System;
using System.Collections.Generic;

namespace ProCreJect
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = @"C:\Users\oilaripi\source\repos\ProCreJect\ProCreJect\bin\Debug\netcoreapp3.1\swift\t.txt";

            string rawMessage = System.IO.File.ReadAllText(fileName);

            var shreder = new Shredding();

            var message = shreder.ShrederSWIFTFile(rawMessage);

            string textBody;
            Dictionary<string, string> textBlocks = new Dictionary<string, string>();
            if (message.TryGetValue("TextBlock", out textBody))
            {
                textBlocks = shreder.ShrederBody(textBody);
            }

            IDBFactory factory = new DBFactory();
            var relationDb = factory.CreateRelationDB("SQLServerDB");
            var dbInsert = new DataInsert(relationDb);

            dbInsert.InsertMessage(message, textBlocks);
            return;
        }        
    }
}
