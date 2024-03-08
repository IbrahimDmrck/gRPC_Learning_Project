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

        ////unary type example
        //var greetClient = new Greeter.GreeterClient(channel);
        //Thread.Sleep(5000);
        //HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "gRPC ilk request" });

        ////unary type example
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

        ///////// UNARY //////////


        ///////// SERVER STREAM //////////

        ////server stream type example
        //var messageClient = new Message.MessageClient(channel);
        //var response = messageClient.SendServerStreamMessage(new MessageRequest
        //{
        //    Name = "İbrahim",
        //    Message = "Merhaba"
        //});
        //CancellationTokenSource cts = new();
        //while (await response.ResponseStream.MoveNext(cts.Token))
        //{
        //    Console.WriteLine(response.ResponseStream.Current.Message);
        //}

        ///////// SERVER STREAM //////////


        ///////// CLIENT STREAM //////////

        ////client stream type example
        //var messageClient = new Message.MessageClient(channel);
        //var request = messageClient.SendClientStreamMessage();
        //for (int i = 0; i < 10; i++)
        //{
        // await Task.Delay(2000);
        //    await request.RequestStream.WriteAsync(new MessageRequest
        //    {
        //        Message = $"{i} . İstek server'a gönderildi",
        //        Name="İbrahim Demircik"

        //    });
        //}

        ////stream datanın sonlandığınıı ifade eder.
        //await request.RequestStream.CompleteAsync();
        //await Console.Out.WriteLineAsync((await request.ResponseAsync).Message);

        ///////// CLIENT STREAM //////////


        ///////// BI-DIRECTİONAL STREAM //////////

        ////bi-directional stream type example
        var messageClient = new Message.MessageClient(channel);
        var request = messageClient.SendBiDirectionalStreamMessage();
        var task1= Task.Run(async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(2000);
                await request.RequestStream.WriteAsync(new MessageRequest
                {
                    Message = $"{i} . İstek server'a gönderildi",
                    Name = "İbrahim Demircik"

                });
            }
        });

        CancellationTokenSource cts = new();
        while (await request.ResponseStream.MoveNext(cts.Token))
        {
            Console.WriteLine(request.ResponseStream.Current.Message);
        }

        await task1;
        await request.RequestStream.CompleteAsync();

        ///////// BI-DIRECTIONAL STREAM //////////

        Console.ReadLine();
    }
}