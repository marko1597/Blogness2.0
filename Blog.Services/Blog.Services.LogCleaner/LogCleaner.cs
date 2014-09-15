using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;

namespace Blog.Services.LogCleaner
{
    public class LogCleaner
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;
        private readonly Task _task;
        private readonly double _hoursThreshold;

        private IConfigurationHelper _configurationHelper;
        public IConfigurationHelper ConfigurationHelper
        {
            get { return _configurationHelper ?? new ConfigurationHelper(); }
            set { _configurationHelper = value; }
        }

        public LogCleaner()
        {
            _hoursThreshold = Convert.ToDouble(ConfigurationHelper.GetAppSettings("TimeRangeInHours")) * (-1);
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _task = new Task(DoWork, _cancellationToken);
        }

        public void Start()
        {
            _task.Start();
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _task.Wait(_cancellationToken);
        }

        private void DoWork()
        {
            try
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var logFiles = Directory.GetFiles(ConfigurationHelper.GetAppSettings("LogFilePath"));
                    foreach (var logFile in logFiles)
                    {
                        var dateRangeDelete = DateTime.Now.AddHours(_hoursThreshold);
                        var creationTime = File.GetLastWriteTime(logFile);

                        if (creationTime <= dateRangeDelete)
                        {
                            File.Delete(logFile);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e);
            }
        }
    }
}
