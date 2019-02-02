using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.JWTAuth.Template.BackgroundWorker.Scheduler
{
    /// <summary>
    /// Code kindly provided by Maarten Balliauw
    /// https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html
    /// </summary>
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}