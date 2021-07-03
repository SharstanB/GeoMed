using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GM.QueueService.QueueDTO
{
    public class QueueMessage
    {
        #region Properties

        public Type Type { get; set; }
        public string Data { get; set; }
        public DateTimeOffset PublishDateTime { get; set; } = DateTimeOffset.Now;
        public int Id { get; set; }

        #endregion



        #region Methods

        public TData GetData<TData>()
            where TData : class
        {
            return JsonConvert.DeserializeObject(Data, Type) as TData;
        }

        public QueueMessage SetData<TData>(TData obj)
        {
            Data = JsonConvert.SerializeObject(obj);
            return this;
        }

        public QueueMessage SetType(Type type)
        {
            if (!type.IsClass)
            {
                throw new ArgumentException("Please provide a class as a type");
            }
            Type = type;
            return this;
        }
        #endregion

    }
}