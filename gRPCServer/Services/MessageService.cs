using Grpc.Core;
using gRPCMessageServer;
using gRPCServer;

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
        //stream type
        public override async Task SendStreamMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Message : {request.Message} | Name : {request.Name}");
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new MessageResponse
                {
                    Message = "Hello " + request.Name + "-response"
                }) ;
            }
        }


    }
}