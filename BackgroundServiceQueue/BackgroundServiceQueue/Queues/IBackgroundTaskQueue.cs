namespace BackroundTaskQueue.Queues
{
    public interface IBackgroundTaskQueue<T>
    {
        Task AddQueue(T workitem);

        ValueTask<T> DeQueue(CancellationToken cancellationToken);

    }
}
