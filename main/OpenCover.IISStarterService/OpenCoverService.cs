using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace OpenCover.IISStarterService
{
    public partial class OpenCoverService : ServiceBase
    {
        private Thread _executionThread;
        private volatile bool _isStopping;
        private ILog _logger;
        
        public OpenCoverService()
        {
            InitializeComponent();

            _isStopping = false;

            #region init event log
            var layout = new PatternLayout("%utcdate %-5level - %message%newline");
            layout.ActivateOptions();
            var appender = new EventLogAppender() { Layout = layout, ApplicationName = "OpenCover" };
            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);
            var root = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root;
            root.AddAppender(appender);

            var output = string.Format("log.txt", DateTime.Now);
            // Add a file Appender
            var fappender = new RollingFileAppender()
            {
                Layout = layout,
                AppendToFile = true,
                File = output,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                DatePattern = "dd.MM.yyyy'.log'",
                MaxSizeRollBackups = 5,
                MaxFileSize = 4096
            };
            root.AddAppender(fappender);

            _logger = LogManager.GetLogger(typeof(OpenCoverService));
            #endregion
        }

        protected override void OnStart(string[] args)
        {
            // Start WWWW
            ServiceController sc = new ServiceController("w3svc");
            if (sc.Status != ServiceControllerStatus.Stopped)
            {
                sc.Stop();
            }

            // Set the variables
            var coverageDir = Environment.GetEnvironmentVariable("CoverageDir") ?? @"C:\Coverage";
            Environment.CurrentDirectory = coverageDir;
            _logger.InfoFormat("Current directory set to: {0}", Environment.CurrentDirectory);

            // Work on the execution thread
            _executionThread = new Thread(new ThreadStart(() =>
            {
                //Debugger.Launch();

                while (!_isStopping)
                {
                    // Start the coverage
                    var output = string.Format("-output:coverage_{0:yyyy-MM-dd_hh-mm-ss-tt}.xml", DateTime.Now);
                    var par = new List<string>() { "-service", "-target:w3svc", output };

                    // start the action
                    _logger.InfoFormat("Starting OpenCover with arguments: {0} ", string.Join(" ", par));
                    OpenCover.Console.Program.Main(par.ToArray());
                    _logger.InfoFormat("OpenCover Stopped");
                }
            }));
            _executionThread.Start();
        }

        protected override void OnStop()
        {
            _logger.InfoFormat("Stop requested");
            _isStopping = true;

            // STOP WWW
            ServiceController sc = new ServiceController("w3svc");
            if (sc.Status != ServiceControllerStatus.Stopped)
            {
                sc.Stop();
            }

            // Wait for joining
            _executionThread.Join();
        }

        protected override void OnShutdown()
        {
            OnStop();
        }
    }
}
