using Topshelf;

namespace Blog.Services.LogCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<LogCleaner>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(() => new LogCleaner());
                    serviceConfigurator.WhenStarted(lc => lc.Start());
                    serviceConfigurator.WhenStopped(lc => lc.Stop());
                });

                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.SetDisplayName("Bloggity Log Cleaner");
                hostConfigurator.SetServiceName("Blog.Service.LogCleaner");
                hostConfigurator.SetDescription("Don't mind me. I'm completely harmless. " +
                                                "I just delete some old bloggity log files you won't need. " +
                                                "You can set-up the time range of logs I can clean up. " +
                                                "Like older than two weeks or maybe older than a day. " +
                                                "It's up to you bruh! :)");
            });
        }
    }
}
