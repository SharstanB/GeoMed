using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GM.QueueService.IRepositories
{
    public interface IQueueService
    {
        void Publish<TData>(TData data)
           where TData : class;
        Task PublishAsync<TData>(TData data)
            where TData : class;
    }
}
