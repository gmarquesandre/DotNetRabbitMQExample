using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Exceptions;

namespace RabbitMQExample.Core.MessageBus
{
    public static class DependecyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {

            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection));
            
            return services;
        }
    }
}

