using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsREST;
using REST_lib;

namespace productsREST
{
    /// <summary>
    /// 
    /// </summary>
    public class Receive
    {
        public Receive()
        {
			Messenger.Instance.SetupListener(
				(ProductUpdate update) => { SQL_Interface.Instance.ReduceItemQuantity(update); },
				Messenger.MessageType.ProductUpdates
			);


			/*
            string exchangeName = Messenger.Instance.ExchangeDeclare(Messenger.MessageType.ProductUpdates);

            string queueName = Messenger.Instance.Channel.QueueDeclare().QueueName;

            // This makes the queue actually receive messages, though it won't do anything with them yet.
            Messenger.Instance.Channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: "");

            EventingBasicConsumer consumer = new EventingBasicConsumer(Messenger.Instance.Channel);
            consumer.Received += (model, ea) => {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                // Debug Code
                Console.WriteLine(" [x] {0}", message);

                try
                {
                    ProductUpdate productUpdate = JsonConverter.FromJson<ProductUpdate>(message);


                    // Send to server
                    
                }
                catch(Exception e)
                {
                    #region execption debug code
                    Console.Out.WriteLine("\n\n**********************************************************************");
                    Console.Out.WriteLine(e.Message);
                    Console.Out.WriteLine(e.InnerException);
                    Console.Out.WriteLine(e.Source);
                    Console.Out.WriteLine("**********************************************************************\n\n");
                    #endregion
                }

            };

            Messenger.Instance.Channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);
			*/







			//var exchange = REST_lib.Messenger.Instance.ExchangeDeclare(Messenger.MessageType.ProductUpdates);


			//var factory = new ConnectionFactory() { HostName = "localhost" };
			//using (var connection = factory.CreateConnection())
			//using (var channel = connection.CreateModel())
			//{
			//    //channel.ExchangeDeclare(exchange: "logs", type: "fanout");

			//    var message = GetMessage(args);
			//    var body = Encoding.UTF8.GetBytes(message);
			//    channel.BasicPublish(exchange: "logs",
			//                         routingKey: "",
			//                         basicProperties: null,
			//                         body: body);
			//    Console.WriteLine(" [x] Sent {0}", message);
			//}

			//Console.WriteLine(" Press [enter] to exit.");
			//Console.ReadLine();

		}


	}
}
