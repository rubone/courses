using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HangfireController : ControllerBase
{

    // Fire-and-forget Job
    [HttpPost]
    [Route("[action]")]
    public IActionResult Welcome()
    {
        var jobId = BackgroundJob.Enqueue(() => SendWelcomeEmail("Welcome to our app"));

        return Ok($"Job ID: {jobId}. Welcome email sent to user");
    }

    // Delayed Job
    [HttpPost]
    [Route("[action]")]
    public IActionResult Discount()
    {
        int timeInSeconds = 30;
        var jobId = BackgroundJob.Schedule(() => SendWelcomeEmail("Welcome to our app"), TimeSpan.FromSeconds(timeInSeconds));
        
        return Ok($"Job ID: {jobId}. Discount email will be sent in {timeInSeconds} seconds.");
    }
    
    // Recurring Job
    [HttpPost]
    [Route("[action]")]
    public IActionResult DatabaseUpdate()
    {
        RecurringJob.AddOrUpdate(() => Console.WriteLine("Database updated"), Cron.Minutely);
        
        return Ok($"Database check job initiated!");
    }
    
    // Continuous Job
    [HttpPost]
    [Route("[action]")]
    public IActionResult Confirm()
    {
        int timeInSeconds = 30;
        var parentJobId = BackgroundJob.Schedule(() => SendWelcomeEmail("You asked to be unsubscribed!"), TimeSpan.FromSeconds(timeInSeconds));

        BackgroundJob.ContinueJobWith(parentJobId, () => Console.WriteLine("You were unsubscribed!"));
        
        return Ok("Confirmation job created!");
    }

    public void SendWelcomeEmail(string message)
    {
        Console.WriteLine(message);
    }
}