using Hangfire.HttpJob.Agent;
using Hangfire.HttpJob.Agent.Attribute;
using Mango.Scaffold.Service.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Scheduler
{
    [TransientJob]
    public class TestJob : JobAgent
    {
        private readonly ILogger<TestJob> _logger;
        private readonly IUserService _userService;

        public TestJob(ILogger<TestJob> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public override Task OnStart(JobContext jobContext)
        {
            _logger.LogInformation("执行调度成功");
            return Task.CompletedTask;
        }
    }
}
