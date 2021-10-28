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

        //Consider a case where two files are downloaded at the same time
        public void Start()
        {
            fileSystemWatcher = new FileSystemWatcher(InPath, "*.txt");            
            fileSystemWatcher.Created += OnCreated;

            fileSystemWatcher.EnableRaisingEvents = true;            
        }

        public void Dispose()
        {
            if (fileSystemWatcher != null)
            {
                fileSystemWatcher.Dispose();
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            this.ProceedSwiftMsg(e.FullPath);
        }

        private void ProceedSwiftMsg(string path)
        {
            string rawMessage = File.ReadAllText(path);
            var shreder = new ShreddingFile(rawMessage);
            var message = shreder.ShrederSWIFTFile();

            string textBody;
            Dictionary<string, string> textBlocks = new Dictionary<string, string>();
            if (message.TryGetValue("body", out textBody))
            {
                textBlocks = shreder.ShrederBody(textBody);
            }

            IDBFactory factory = new DBFactory();
            var relationDb = factory.CreateRelationDB("SQLServerDB");
            var dbInsert = new DataInsert(relationDb);

            dbInsert.InsertMessage(message, textBlocks);
        }
    }
}
