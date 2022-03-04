using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using MyServer;
using MyWebClient.Models;
using Polly;
using System.Diagnostics;

namespace MyWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Hello()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7039");
            var client = new Greeter.GreeterClient(channel);

            var maxRetryAttempts = 3;
            var pauseBetweenFailure = TimeSpan.FromSeconds(3);

            var retryPolicy = Policy
                .Handle<RpcException>()
                .RetryAsync(maxRetryAttempts);

            await retryPolicy.ExecuteAsync(async () =>
            {
                return Ok(await MakeRPC(client));
            });

            //try
            //{
            //    return Ok(await MakeRPC(client));
            //}
            //catch (Exception)
            //{


            //}

            return BadRequest("ERROR !");
        }

        [NonAction]
        private async Task<string> MakeRPC(Greeter.GreeterClient client)
        {
            var reply = await client.SayHelloAsync(new HelloRequest
            {
                Name = "asp core 6"
            },
             null,
             DateTime.UtcNow.AddSeconds(2));

            return reply.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}