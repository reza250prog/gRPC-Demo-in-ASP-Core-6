using Grpc.Net.Client;
using MyServer;

Console.WriteLine("Hello gRPC !");

using var channel = GrpcChannel.ForAddress("https://localhost:7039");
var client = new Greeter.GreeterClient(channel);

var reply = await client.SayHelloListAsync(new HelloRequest
{
    Name = "Reza"
},
null,
DateTime.UtcNow.AddSeconds(2)
);

Console.WriteLine(reply.ToString());

await channel.ShutdownAsync();