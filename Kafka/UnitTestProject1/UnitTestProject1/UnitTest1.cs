using Confluent.Kafka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnitTestProject1.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var bootstrapServers = new string[] { "202.105.96.132:9092" };
            var group1 = "group.1";
            //var group2 = "group.2";
            var topic = "campus-user-info";

            {
                KafkaConsumer consumer = new KafkaConsumer(group1, bootstrapServers);
                consumer.EnableAutoCommit = false;
                //consumer.ListenAsync(new KafkaSubscriber[] { new KafkaSubscriber() { Topic = topic, Partition = 0 } }, result =>
                //{
                //    Console.WriteLine($"{group1} recieve message:{result.Message}");
                //    result.Commit();//手动提交，如果上面的EnableAutoCommit=true表示自动提交，则无需调用Commit方法
                //}).Wait();


                RecieveResult recieveResult = consumer.ListenOnce(topic);
            }



            //{
            //    KafkaConsumer consumer = new KafkaConsumer(group2, bootstrapServers);
            //    consumer.EnableAutoCommit = false;
            //    consumer.ListenAsync(new KafkaSubscriber[] { new KafkaSubscriber() { Topic = topic, Partition = 1 } }, result =>
            //    {
            //        Console.WriteLine($"{group2} recieve message:{result.Message}");
            //        result.Commit();//手动提交，如果上面的EnableAutoCommit=true表示自动提交，则无需调用Commit方法
            //    }).Wait();
            //}

            //KafkaProducer producer = new KafkaProducer(bootstrapServers);



            //int index = 0;
            //while (true)
            //{
            //    Console.Write("请输入消息:");
            //    //var line = Console.ReadLine();
            //    var line = "";
            //    //var line = GetCampusUserInfo();
            //    int partition = index % 3;
            //    producer.Publish(topic, partition, "campus-user-info", line);
            //    index++;
            //}
        }

        public string GetCampusUserInfo()
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

