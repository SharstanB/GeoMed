using EasyNetQ;
using GM.QueueService.IRepositories;
using GM.QueueService.QueueDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GM.QueueService.Repositories
{
    public class QueueService : IQueueService
    {
        private IBus Bus { get; }
        public QueueService(IBus bus)
        {
            Bus = bus;
        }
        public void Publish<TData>(TData data) where TData : class
        {
            var queueMessage = new QueueMessage()
                 .SetData(data)
                 .SetType(typeof(TData));
            Bus.PubSub.Publish(queueMessage);
        }

        public async Task PublishAsync<TData>(TData data) where TData : class
        {
            var queueMessage = new QueueMessage()
                .SetData(data)
                .SetType(typeof(TData));
            await Bus.PubSub.PublishAsync(queueMessage);
        }
    }
}
