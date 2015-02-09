using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenCover.IISStarterService;

namespace OpenCover.Test.IISStarterService
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new srv();
            service.OnStart();

            Thread.Sleep(10000);
            service.OnStop();
        }

    }

    class srv : OpenCoverService
    {

        public void OnStart()
        {
            base.OnStart(null);
        }

        public void OnStop()
        {
            base.OnStop();
        }
    }
}
