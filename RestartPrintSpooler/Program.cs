using System;
using System.ServiceProcess;
using System.Runtime.InteropServices;

namespace RestartPrintSpooler
{
    class Program
    {
        static bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        static void Main(string[] args)
        {

            if (!isWindows)
            {
                Console.WriteLine("This program restarts the print spooler on windows and does not support this platform.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }

            // Stop the spooler.
            ServiceController service = new ServiceController("Spooler");
            if ((!service.Status.Equals(ServiceControllerStatus.Stopped)) && 
                (!service.Status.Equals(ServiceControllerStatus.StopPending))) {

                Console.WriteLine("Stopping print spooler...");

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }

            // Start the spooler
            Console.WriteLine("Restarting print spooler...");
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running);

            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
