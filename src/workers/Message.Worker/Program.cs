using Message.Worker.Business;
using Message.Worker.Business.Implements;
using Message.Worker.Infrastructure.Options;
using Message.Worker.Repositories;
using Message.Worker.Repositories.Implements;
using Message.Worker.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Message.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<ServiceSettings>(hostContext.Configuration);
                    services.AddSingleton<IDeviceBusiness, DeviceBusiness>();
                    services.AddSingleton<IDeviceRepository, DeviceRepository>();
                    services.AddHostedService<MessageTask>();
                });
    }
}
