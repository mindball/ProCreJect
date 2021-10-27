using DbFactory;
using DbFactory.Contracts;
using System;
using System.Collections.Generic;
using Topshelf;
using WinServiceHandleSwiftMessage;
using System.Linq;

namespace ProCreJect
{
    class Program
    {
        static void Main(string[] args)
        { 
            var rc = HostFactory.Run(x =>                                   
            {
                x.Service<MessageQueueProcessor>(s =>
                {
                    s.ConstructUsing(name => new MessageQueueProcessor());
                    s.WhenStarted(ms => ms.Start());
                    //Check if dispose execute remove when service not working
                    s.WhenStopped(ms => ms.Dispose());
                });               

                x.RunAsLocalSystem();
                x.StartAutomatically();
                //try it
                //x.RunAs("username", "password");

                x.SetDescription("ProCreJect collect swift message");
                x.SetDisplayName("swift service");
                x.SetServiceName("swiftService");
            });                                                             

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  
            Environment.ExitCode = exitCode;           
        }        
    }
}
