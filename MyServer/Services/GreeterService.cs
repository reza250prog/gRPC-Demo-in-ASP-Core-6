using Grpc.Core;

namespace MyServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            await Task.Delay(500);

            return new HelloReply
            {
                Message = "Hello from gRPC Server : Hi " + request.Name
            };
        }

        public override async Task<HelloReplyList> SayHelloList(HelloRequest request, ServerCallContext context)
        {
            var list = new HelloReplyList();

            await Task.Delay(500);

            for (int i = 0; i < 5; i++)
            {
                list.List.Add(new HelloReply
                {
                    Message = $"Hello {i}"
                });
            }

            return list;
        }
    }
}