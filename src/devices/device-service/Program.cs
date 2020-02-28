using Autofac;
using device_service.Ouput;
using device_service.Sensors;
using Microsoft.Azure.Devices.Client;
using System;
using System.Threading.Tasks;

namespace device_service
{
    class Program
    {
        private static string deviceConnectionString = Environment.GetEnvironmentVariable("IOTHUB_DEVICE_CONN_STRING");
        private static string deviceId = Environment.GetEnvironmentVariable("DEVICE_ID");
        private static TransportType transportType = TransportType.Amqp;

        private static Autofac.IContainer CompositionRoot()
        {
            deviceConnectionString = deviceConnectionString.Replace("#DEVICE_ID#", deviceId);
            var builder = new ContainerBuilder();
            builder.RegisterInstance(DeviceClient.CreateFromConnectionString(deviceConnectionString, transportType));
            builder.RegisterType<IHcSrHandler>().As<HcSrHandler>();
            builder.RegisterType<ILightOutput>().As<LightOutput>();
            builder.RegisterType<Application>();

            return builder.Build();
        }

        public static async Task Main(string[] args)
        {
            await CompositionRoot().Resolve<Application>().Run();
        }
    }
}
