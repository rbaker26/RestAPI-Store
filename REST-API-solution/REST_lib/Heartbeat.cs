using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace REST_lib
{
    public class Heartbeat
    {


        private static readonly Lazy<Heartbeat> m_lazy = new Lazy<Heartbeat>(() => new Heartbeat());

        public static Heartbeat Instance { get { return m_lazy.Value; } }
        public string ServiceName { get; set; }

        public void InitHeartbeat(string ServiceName)
        {
            this.ServiceName = ServiceName;
        }

        private Heartbeat()
        {
        }


        public void Start()
        {
            Console.Out.WriteLine("*************************************************");
            Console.Out.WriteLine("Heartbeat Manager Started:");
            Console.Out.WriteLine("*************************************************");

            Thread heartbeat_thread = new Thread(new ParameterizedThreadStart(Beat));
            heartbeat_thread.Start(ServiceName);
        }

           

        public static void Beat(object serviceName)
        {
            string serviceName_str = (string)serviceName;
            while (true)
            {
                Console.Out.WriteLine(DateTime.Now + "\t" + serviceName_str + " is alive");
                Thread.Sleep(60000);
            }
        }


    }
}
