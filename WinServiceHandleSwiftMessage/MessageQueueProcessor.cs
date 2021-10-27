using DbFactory;
using DbFactory.Contracts;
using Shredding;
using System;
using System.Collections.Generic;
using System.IO;


namespace WinServiceHandleSwiftMessage
{
    public class MessageQueueProcessor : IDisposable
    {
        public const string InPath = @"C:\Data\MessageQueue\in";        
        private FileSystemWatcher fileSystemWatcher;

        public void Start()
        {
            fileSystemWatcher = new FileSystemWatcher(InPath, "*.txt");
            fileSystemWatcher.EnableRaisingEvents = true;
            //Look at when file is changed
            fileSystemWatcher.Created += (sender, e) =>
            {
                Console.WriteLine($"Processing enqueued file {e.Name}");
                var path = e.FullPath;
                string rawMessage = File.ReadAllText(path);
                var shreder = new ShreddingFile();
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
            };

            //this.Dispose();
        }

        public void Dispose()
        {
            if (fileSystemWatcher != null)
            {
                fileSystemWatcher.Dispose();
            }
        }
    }
}
