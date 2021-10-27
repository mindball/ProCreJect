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
        public const string ProcessedPath = @"C:\Data\MessageQueue\processed";
        private FileSystemWatcher fileSystemWatcher;

        public void Start()
        {
            fileSystemWatcher = new FileSystemWatcher(InPath, "*.txt");
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.Created += (sender, e) =>
            {
                Console.WriteLine($"Processing enqueued file {e.Name}");

                //var destFile = Path.Combine(@"C:\Data\MessageQueue\processed", e.FullPath);

                //if (File.Exists(e.FullPath))
                //{
                //    File.Move(e.FullPath, destFile);
                //}

                string rawMessage = File.ReadAllText(e.FullPath);
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
