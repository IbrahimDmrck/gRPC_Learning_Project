using Grpc.Net.Client;
using gRPCServer;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7194");
        var greetClient = new Greeter.GreeterClient(channel);
        Thread.Sleep(5000);
        HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "gRPC ilk request" });
        Console.WriteLine(result.Message);
        Console.ReadLine();
    }
}