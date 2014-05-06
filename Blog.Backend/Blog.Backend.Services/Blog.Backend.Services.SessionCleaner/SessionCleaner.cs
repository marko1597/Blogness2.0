using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.SessionCleaner
{
    public partial class SessionCleaner : ServiceBase
    {
        private readonly Timer _timer = new Timer();
        public SessionCleaner()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("BlogSessionCleaner"))
            {
                System.Diagnostics.EventLog.DeleteEventSource("BlogSessionCleaner");
                System.Diagnostics.EventLog.CreateEventSource("BlogSessionCleaner", "Application");
            }
            EventLogger.Source = "BlogSessionCleaner";
            EventLogger.Log = "Application";
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            Task.Run(() =>
                {
                    try
                    {
                        SessionFactory.GetInstance().CreateSession().CleanupExpiredSessions();
                        EventLogger.WriteEntry("Cleaned up old sessions.");
                    }
                    catch (Exception ex)
                    {
                        EventLogger.WriteEntry(ex.Message);
                    }   
                 });
        }

        protected override void OnStart(string[] args)
        {
            EventLogger.WriteEntry(string.Format("Blog Session Cleaner Started at {0}", DateTime.UtcNow));
            _timer.Interval = 20000;
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        protected override void OnContinue()
        {
            EventLogger.WriteEntry(string.Format("Blog Session Cleaner has resumed at {0}", DateTime.UtcNow));
            _timer.Start();
        }

        protected override void OnStop()
        {
            EventLogger.WriteEntry(string.Format("Blog Session Cleaner Stopped at {0}", DateTime.UtcNow));
            _timer.Stop();
        }
    }
}
