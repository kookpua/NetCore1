using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRDemo
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddMediatR(typeof(Program).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            // v1 v2都会执行
            await mediator.Publish(new MyEvent { EventName="event01"});

            //只执行最前边的方法
            //await mediator.Send(new MyCommand { CommandName = "cmd01" });

            Console.WriteLine("Hello World!");
        }

        internal class MyCommand : IRequest<long>
        {
            public string CommandName { get; set; }
        }

        internal class MyCommandHandlerV2 : IRequestHandler<MyCommand, long>
        {

            public Task<long> Handle(MyCommand request, CancellationToken cancellationToken)
            {
                Console.WriteLine($"MycommandHandleV2执行命令:{request.CommandName}");

                return Task.FromResult(10L);
            }
        }
        internal class MyCommandHandler : IRequestHandler<MyCommand,long>
        {
            public Task<long> Handle(MyCommand request, CancellationToken cancellationToken)
            {
                Console.WriteLine($"MycommandHandle执行命令:{request.CommandName}");

                return Task.FromResult(10L);
            }
          
        }
       


        internal class MyEvent : INotification
        {
            public string EventName { get; set; }
        }


        internal class MyEventHandler : INotificationHandler<MyEvent>
        {
            public Task Handle(MyEvent notification, CancellationToken cancellationToken)
            {
                Console.WriteLine($"MyEventG=Handler执行:{notification.EventName}");
                return Task.CompletedTask;
                throw new NotImplementedException();
            }
        }

        internal class MyEventHandlerV2 : INotificationHandler<MyEvent>
        {
            public Task Handle(MyEvent notification, CancellationToken cancellationToken)
            {
                Console.WriteLine($"MyEventG=HandlerV2执行:{notification.EventName}");
                return Task.CompletedTask;
                throw new NotImplementedException();
            }
        }

    }
}
