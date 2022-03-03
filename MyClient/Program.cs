using Grpc.Net.Client;
using MyServer;

Console.WriteLine("Hello gRPC !");

using var channel = GrpcChannel.ForAddress("https://localhost:7039");
var client = new Greeter.GreeterClient(channel);

var reply = await client.SayHelloAsync(new HelloRequest
{
    Name = "Reza"
});

Console.WriteLine(reply.ToString());
Console.WriteLine(reply.Message);