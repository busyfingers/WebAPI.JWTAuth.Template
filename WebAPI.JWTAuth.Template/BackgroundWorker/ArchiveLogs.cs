using System.Threading;
using System.Threading.Tasks;
using WebAPI.JWTAuth.Template.BackgroundWorker.Scheduler;
using WebAPI.JWTAuth.Template.Helpers;
using Microsoft.Extensions.Logging;

namespace WebAPI.JWTAuth.Template.BackgroundWorker
{
    public class ArchiveLogs : IScheduledTask
    {
        private readonly ILogger _logger;
        public string Schedule => "";

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