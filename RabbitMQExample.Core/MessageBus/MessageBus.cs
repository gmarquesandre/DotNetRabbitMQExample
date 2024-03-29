﻿using System;
using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;
using RabbitMQExample.Core.RequestResponse;

namespace RabbitMQExample.Core.MessageBus
{
	public class MessageBus : IMessageBus
	{
		private IBus _bus;
        public readonly string _connectionString;

        public bool IsConnected => _bus != null? _bus.Advanced.IsConnected : false;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }
        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQResponderException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() => { _bus = RabbitHutch.CreateBus(_connectionString); });

        }
        public void Dispose()
        {
            _bus.Dispose();
        }

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.PubSub.Publish(message);            
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.PubSub.PublishAsync(message);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Respond<TRequest, TResponse>(responder);
        }

        public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await _bus.Rpc.RespondAsync<TRequest, TResponse>(responder);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.Subscribe<T>(subscriptionId, onMessage);         
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.SubscribeAsync<T>(subscriptionId, onMessage);
        }
    }
}

