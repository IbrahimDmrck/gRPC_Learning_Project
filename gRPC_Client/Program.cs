using Grpc.Core;
using Grpc.Net.Client;
using gRPCMessageClient;
using gRPCServer;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7194");
        ///////// UNARY //////////

        //unary type example
        //var greetClient = new Greeter.GreeterClient(channel);
        //Thread.Sleep(5000);
        //HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "gRPC ilk request" });

        //unary type example
        //var messageClient = new Message.MessageClient(channel);
        //for (int i = 0; i < 5; i++)
        //{
        //    MessageResponse result = await messageClient.SendMessageAsync(new MessageRequest
        //    {
        //        Name = "İbrahim",
        //        Message = "Merhaba"
        //    });
        //    Thread.Sleep(5000);
        //    Console.WriteLine(result.Message);
        //}

        ///////// STREAM //////////

        //stream type example
        var messageClient = new Message.MessageClient(channel);
        var response = messageClient.SendStreamMessage(new MessageRequest
        {
            Name = "İbrahim",
            Message = "Merhaba"
        });
        CancellationTokenSource cts = new();
        while (await response.ResponseStream.MoveNext(cts.Token))
        {
            Console.WriteLine(response.ResponseStream.Current.Message);
        }

        Console.ReadLine();
    }
}