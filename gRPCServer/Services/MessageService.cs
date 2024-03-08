using Grpc.Core;
using gRPCMessageServer;
using gRPCServer;
using System.IO;

namespace gRPCServer.Services
{
    public class MessageService : Message.MessageBase
    {
        private readonly ILogger<GreeterService> _logger;
        public MessageService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }
        //unary type
        public override Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            Console.WriteLine("grpc requesti alındı");
            return Task.FromResult(new MessageResponse
            {
                Message = "Hello " + request.Name
            });
        }
        //server stream type
        public override async Task SendServerStreamMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Message : {request.Message} | Name : {request.Name}");
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new MessageResponse
                {
                    Message = "Hello " + request.Name + "-response"
                });
            }
        }

        //client stream type
        public override async Task<MessageResponse> SendClientStreamMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine($"Message : {requestStream.Current.Message} | Name : {requestStream.Current.Name}");
            }
            return new MessageResponse
            {
                Message = "Veri Alınmıştır"
            };
        }

        //bi-directional stream type
        public override async Task SendBiDirectionalStreamMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            var task1 = Task.Run(async () =>
            {
                while (await requestStream.MoveNext(context.CancellationToken))
                {
                    Console.WriteLine($"Message : {requestStream.Current.Message} | Name : {requestStream.Current.Name}");
                }
            });

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new MessageResponse
                {
                    Message = "Mesaj " + i
                });
            }

            await task1;
        }

    }
}