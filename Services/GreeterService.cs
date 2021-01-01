using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CosmoResearch
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<HelloReply> UnaryCall(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "UnaryCall " + request.Name
            });
        }

        public override async Task StreamingFromServer(
            HelloRequest request,
            IServerStreamWriter<HelloReply> responseStream, 
            ServerCallContext context)
        {
            for (var i = 0; i < 5; i++)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = "StreamingFromServer " + i
                });
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public override async Task<HelloReply> StreamingFromClient(
            IAsyncStreamReader<HelloRequest> requestStream, 
            ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var message = requestStream.Current;
                Console.WriteLine("StreamingFromClient" + message);
            }
            return new HelloReply
            {
                Message = "StreamingFromClient"
            };
        }

        public override async Task StreamingBothWays(
            IAsyncStreamReader<HelloRequest> requestStream,
            IServerStreamWriter<HelloReply> responseStream, 
            ServerCallContext context)
        {
            await foreach (var message in requestStream.ReadAllAsync())
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = "StreamingBothWays" + message.Name
                });
            }
        }
    }
}
