using BackroundTaskQueue.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServiceQueue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IBackgroundTaskQueue<string> backroundTaskQueue;

        public TestController(IBackgroundTaskQueue<string> backroundTaskQueue)
        {
            this.backroundTaskQueue = backroundTaskQueue;
        }


        [HttpPost]
        public async  Task<IActionResult> AddQueue(string[] names)
        {
            foreach (var name in names)
            {
                await backroundTaskQueue.AddQueue(name);
            }

            return Ok();
        }
    }
}
