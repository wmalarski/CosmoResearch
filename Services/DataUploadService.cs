using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmoResearch.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CosmoResearch.Services
{
    public class DataUploadService : DataUpload.DataUploadBase
    {
        private readonly ILogger<DataUploadService> _logger;

        private readonly DataService _tableService;

        public DataUploadService(ILogger<DataUploadService> logger, DataService tableService)
        {
            _logger = logger;
            _tableService = tableService;
        }

        public override async Task<DataReply> SendData(
            IAsyncStreamReader<DataRequest> requestStream,
            ServerCallContext context)
        {
            var taskList = new List<Task<DataEntity>>();

            while (await requestStream.MoveNext())
            {
                var message = requestStream.Current;

                var nodeType = ToNodeType(message.DataType);

                var nodeEntity = new DataEntity(message.Path, message.Key) 
                {
                    DoubleData = message.Double32Data.ToArray(),
                    IntData = message.Int32Data.ToArray(),
                    LongData = message.Int64Data.ToArray(),
                    StringData = message.StringData.ToArray(),
                    Size = message.Dim.Select(n => Convert.ToInt32(n)).ToArray(),
                    Type = nodeType,
                };

                taskList.Add(_tableService.InsertOrMergeEntityAsync(nodeEntity));
            }

            await Task.WhenAll(taskList);

            return new DataReply
            {
                Success = true
            };
        }

        private static DataType ToNodeType(DataDType dType) {
            switch (dType) {
                case DataDType.Double:
                    return DataType.Double;
                case DataDType.Int32:
                    return DataType.Int32;
                case DataDType.Int64:
                    return DataType.Int64;
                default:
                    return DataType.String;
            }
        }
    }
}
