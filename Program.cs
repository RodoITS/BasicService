using BasicService;
using BasicService.Configuration;
using BasicService.Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using Newtonsoft.Json;
using Quartz;
using System.Diagnostics;

var builder = Host.CreateApplicationBuilder(args);

//Add Logger
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();

//Aggiungiamo il supporto per l'event logger
builder.Services.AddLogging(l => {
    l.AddEventLog(eventLogSettings =>
    {
        eventLogSettings.SourceName = "BasicService";
        eventLogSettings.LogName = "BasicService";
    });
});

//Indichiamo che si tratta di un servizio Windows
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "BasicService";
});

//Recuperiamo la configurazione
IConfiguration configuration = builder.Configuration;
var syncSettings = new SyncSettings();
configuration.GetSection("SyncSettings").Bind(syncSettings);
//recupero la stringa di connessione
string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMySql");

var WooCommerceSettings = new WooCommerceSettings();
configuration.GetSection("WooCommerceSettings").Bind(WooCommerceSettings);

int data_seconds = 0;
//Add scheduling
builder.Services.AddQuartz(q =>
{
    q.CheckConfiguration = true;
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
    q.UseDefaultThreadPool(tp =>
    {
        tp.MaxConcurrency = 10;
    });

//SCHEDULE SYNC DATA - WEBSERVER
int ScheduleCron = syncSettings.SyncData_Schedule_Min;
int ScheduleCron_Secs = syncSettings.SyncData_Schedule_Secs;
data_seconds = ScheduleCron_Secs + ScheduleCron * 60;
if (data_seconds > 0)
{
    var jobKey = new JobKey("jobdata");
    q.AddJob<SyncDataScheduler>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("jobdata-trigger")
            .UsingJobData("connectionString", connectionString)
            .UsingJobData("syncsettings", JsonConvert.SerializeObject(syncSettings))
            .UsingJobData("wcsettings", JsonConvert.SerializeObject(WooCommerceSettings))
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(data_seconds)
            .RepeatForever()));
}
});

builder.Services.Configure<QuartzOptions>(options =>
{
    options.Scheduling.IgnoreDuplicates = true;
    options.Scheduling.OverWriteExistingData = true;
});

//Add the Quartz.NET hosted service
builder.Services.AddQuartzHostedService(q =>
{
    q.WaitForJobsToComplete = true;
});

var serviceProvider = builder.Services.BuildServiceProvider(); 
var logger = serviceProvider.GetService<ILogger<Program>>(); 
builder.Services.AddSingleton(typeof(ILogger), logger);

IHost host = builder.Build();

#if DEBUG
    while (!Debugger.IsAttached) Thread.Sleep(10000);
#endif

logger.LogInformation("Application starting");
host.Run();
