using System;

public static void Run(TimerInfo myTimer, ILogger log, ICollector<string> outputQueueItem)
{
    log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    outputQueueItem.Add($"ZAD6 {DateTime.Now}");
}
