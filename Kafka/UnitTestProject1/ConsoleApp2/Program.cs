using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitTestProject1.Model;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

           //  produce();
           consumption();

        }


        /// <summary>
        /// 消费
        /// </summary>
        private static void consumption()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "202.105.96.132:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,

                EnableAutoOffsetStore = false//<----this
            };
            using (var consumer = new ConsumerBuilder<Ignore, string>(conf).SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}")).Build())
            {

                consumer.Subscribe("campus-jm-face");
                //consumer.Subscribe("campus-user-info");

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {

                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);
                            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: ${consumeResult.Message.Value}");
                            consumer.StoreOffset(consumeResult);//<----this
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    consumer.Close();
                }
            }
        }

        /// <summary>
        /// 生产
        /// </summary>
        private static void produce()
        {
            var config = new ProducerConfig { BootstrapServers = "202.105.96.132:9092" };
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {

                    p.Produce("campus-user-info", new Message<Null, string> { Value = $"my message: {GetCampusUserInfo()}" }, handler);
                    p.Flush(TimeSpan.FromSeconds(10));

                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        public static void handler(DeliveryReport<Null, string> deliveryReport)
        {

            if (deliveryReport.Error.IsError)
            {

            }
            else
            {

            }

            //Console.WriteLine(!deliveryReport.Error.IsError
            //       ? $"Delivered message to {deliveryReport.TopicPartitionOffset}"
            //       : $"Delivery Error: {deliveryReport.Error.Reason}");
        }

        private static string GetCampusUserInfo()
        {
            CampusUserInfo campusUserInfo = new CampusUserInfo()
            {
                userType = "1",
                studentInfo = new StudentInfo()
                {
                    personalNo = "XJ309456",
                    address = "广东省深圳市宝安区恒安花园恒安阁XXX 楼XX 房",
                    cardNum = "",
                    cardType = 1,
                    className = "一年级一班",
                    code = "A123",
                    educateStatus = "1",
                    name = "pp test1",
                    orgId = 1320,
                    orgName = "上海金茂学校",
                    phone = "",
                    sex = 1
                },
                parentInfos = new List<ParentInfosItem>()
                {
                    new ParentInfosItem() {
                      cardNum= "440883195508030054",
                      cardType= 1,
                      name= "pp test1学生父亲",
                      parentType= 1,
                      phone= "18625520000",
                      sex= 1,
                      personalNo= "XJ304563"
                    },
                    new ParentInfosItem() {
                      cardNum= "440883195607040012",
                      cardType= 1,
                      name= "pp test1学生母亲",
                      parentType= 1,
                      phone= "18625520000",
                      sex= 0,
                      personalNo= "XJ30223"
                    }
                }
            };

            return JsonConvert.SerializeObject(campusUserInfo);
        }
    }
}
