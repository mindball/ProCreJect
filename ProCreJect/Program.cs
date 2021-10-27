using DbFactory;
using DbFactory.Contracts;
using System;
using System.Collections.Generic;
using WinServiceHandleSwiftMessage;

namespace ProCreJect
{
    class Program
    {
        static void Main(string[] args)
        {     
            MessageQueueProcessor messageQueueProcessor = new MessageQueueProcessor();
                messageQueueProcessor.Start();

            Console.ReadKey();

            return;
        }        
    }
}
