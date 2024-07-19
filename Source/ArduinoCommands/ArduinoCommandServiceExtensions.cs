using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SerialCommunication.Abstraction;
using SerialCommunication.SerialPort;

namespace ArduinoCommands
{
    public static class ArduinoCommandServiceExtensions
    {
        public static IServiceCollection AddArduinoCommandService(this IServiceCollection services)
        {
            services.AddSingleton<IArduinoCommandService, ArduinoCommandService>();
            services.TryAddTransient<ISerialPortFactory, SerialPortFactory>();
            return services;
        }
    }
}
