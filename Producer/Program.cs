using System;
using System.Threading;
using System.Threading.Tasks;
using CommonNamespace;
using MassTransit;

namespace Producer
{
    public class Program
    {
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(configurator =>
            {
                {
                    configurator.Host("hawk.rmq.cloudamqp.com",
                        "ykziztbb",
                        h =>
                    {
                        h.Username("ykziztbb");
                        h.Password("oZaUpy2Sru1P0b04K9ghjx3MSFpXTMIU");
                    });
                }
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {       
                //PUBLISH
                await busControl.Publish(new MessageDto
                {
                    Content = "message!"
                });
                
                
                
                //SEND
                /*
                var queueName = "masstransit_event_queue_1";
                var sendEndpoint = await busControl.GetSendEndpoint(new Uri($"queue:{queueName}"));
                if (sendEndpoint == null)
                {
                    throw new Exception($"Не удалось найти очередь {queueName}");
                }
                await sendEndpoint.Send(new MessageDto
                {
                    Content = "message!"
                }, CancellationToken.None);                    
                */
                
                /*
                //REQUEST
                var response = await busControl.Request<Request, CommonNamespace.Response>( new Request
                {
                    Message = new MessageDto
                    {
                        Content = "message!"
                    }
                }, CancellationToken.None);
                Console.WriteLine($"Response success: {response.Message.IsSuccess}");
                */
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}