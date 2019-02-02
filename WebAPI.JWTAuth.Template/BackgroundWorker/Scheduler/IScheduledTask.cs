using System.Threading;
using System.Threading.Tasks;

namespace CrawlBack.BackgroundWorker.Scheduler
{
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}