using System.Threading.Channels;

namespace BackroundTaskQueue.Queues
{
    public class NamesQueue : IBackgroundTaskQueue<string>
    {
        private readonly Channel<string> channel;


        public NamesQueue(IConfiguration configuration)
        {
            int.TryParse(configuration["QueueCapacity"], out int capatiy);

            BoundedChannelOptions options = new(capatiy)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            channel = Channel.CreateBounded<string>(options);
        }

        public async Task AddQueue(string workitem)
        {
            ArgumentNullException.ThrowIfNull(workitem);

            await channel.Writer.WriteAsync(workitem);
        }

        public ValueTask<string> DeQueue(CancellationToken cancellationToken)
        {
            var workItem = channel.Reader.ReadAsync(cancellationToken);
            return workItem;
        }
    }
}
