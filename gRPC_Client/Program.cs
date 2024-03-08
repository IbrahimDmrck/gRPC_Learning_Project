using Grpc.Net.Client;
using gRPCMessageClient;
using gRPCServer;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7194");
        //var greetClient = new Greeter.GreeterClient(channel);
        //Thread.Sleep(5000);
        //HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "gRPC ilk request" });

        var messageClient = new Message.MessageClient(channel);
       
        
        for (int i = 0; i < 5; i++)
        {
            MessageResponse result = await messageClient.SendMessageAsync(new MessageRequest
            {
                Name = "İbrahim",
                Message = "Merhaba"
            });
            Thread.Sleep(5000);
            Console.WriteLine(result.Message);
        }
        
        Console.ReadLine();
    }
}