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

        public override  Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            Console.WriteLine("grpc requesti alındı");
            return Task.FromResult(new MessageResponse
            {
                Message= "Hello " + request.Name
            });
        }


    }
}