using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CosmoResearch.Services
{
    public class DataUploadService : DataUpload.DataUploadBase
    {
        private readonly ILogger<DataUploadService> _logger;
        public DataUploadService(ILogger<DataUploadService> logger)
        {
            _logger = logger;
        }

        public override async Task<DataReply> SendData(
            IAsyncStreamReader<DataRequest> requestStream, 
            ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var message = requestStream.Current;
                Console.WriteLine("SendData" + message);
            }
            return new DataReply
            {
                Success = true
            };
        }
    }
}
