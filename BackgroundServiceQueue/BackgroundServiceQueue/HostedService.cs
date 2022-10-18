using BackroundTaskQueue.Queues;

namespace BackgroundServiceQueue
{
    public class HostedService : BackgroundService
    {
        private readonly ILogger<HostedService> logger;
        private readonly IBackgroundTaskQueue<string> backroundTaskQueue;

        public HostedService(ILogger<HostedService> logger, IBackgroundTaskQueue<string> backroundTaskQueue)
        {
            this.backroundTaskQueue = backroundTaskQueue;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //    await Task.Delay(5000);
                //    while (backroundTaskQueue.HasNext())
                //    {
                //        var name = backroundTaskQueue.DeQueue();
                //        logger.LogInformation($"ExecuteAsync  worked {name}");
                //    }

                var name = await backroundTaskQueue.DeQueue(stoppingToken);

                await Task.Delay(1000); //db insert

                logger.LogInformation($"ExecuteAsync worked for  {name}");


            }
        }
    }
}
