using System;
using System.Collections.Generic;
using Autofac;
using Theorem.Models;
using Theorem.Providers;

namespace Theorem.Middleware
{
    public class MiddlewarePipeline
    {
        private SlackProvider _slackProvider { get; set; }
        private IEnumerable<IMiddleware> _middleware { get; set; }
        
        public MiddlewarePipeline(SlackProvider slackProvider, IEnumerable<IMiddleware> middleware)
        {
            _slackProvider = slackProvider;
            _slackProvider.NewMessage += NewMessage;
            _middleware = middleware;
        }

        private void NewMessage(object sender, MessageEventModel message)
        {
            foreach (var middleware in _middleware)
            {
                if (middleware.ProcessMessage(message) == MiddlewareResult.Stop)
                {
                    break;
                }
            }
        }
    }
}