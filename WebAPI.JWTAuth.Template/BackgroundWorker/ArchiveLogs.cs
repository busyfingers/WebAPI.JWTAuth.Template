using System.Threading;
using System.Threading.Tasks;
using CrawlBack.BackgroundWorker.Scheduler;
using CrawlBack.Helpers;
using Microsoft.Extensions.Logging;

namespace CrawlBack.BackgroundWorker
{
    public class ArchiveLogs : IScheduledTask
    {
        private ILogger _logger;
        public string Schedule => "59 23 * * *";

        public ArchiveLogs(ILogger<ArchiveLogs> logger)
        {
            _logger = logger;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            FileUtils.CompressLogs(_logger);
            await Task.CompletedTask;
        }
    }
}