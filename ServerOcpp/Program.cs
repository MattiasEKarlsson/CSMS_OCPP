using System;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using ServerOcpp.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace ServerOcpp
{
    class Program
    {
        
        public class Echo : WebSocketBehavior
        {
            List<string> boot = new List<string>();

            protected override void OnOpen()
            {
                base.OnOpen();
            }

            protected override void OnMessage(MessageEventArgs e)
            {
                
                
                
                dynamic jsonResponse = JsonConvert.DeserializeObject(e.Data);
                Console.WriteLine($"Recived messege: {jsonResponse}");

                switch (jsonResponse[2].ToString())
                {
                    case "BootNotification":
                        Console.WriteLine("Its a BootNotification");
                        string aa = "[3,\"19223201\",{\"currentTime\": \"2013-02-01T20:53:32.486Z\",\"interval\": 60,\"status\": \"Accepted\"}]";
                        Send(aa);
                        break;
                    case "Heartbeat":
                        
                        Console.WriteLine("Its a Heartbeat!!");
                        var ip = Context.UserEndPoint;
                        Console.WriteLine(ip);
                        break;
                    case "StatusNotification":
                        Console.WriteLine("Its a StatusNotification");
                        break;
                    case "TransactionEvent":
                        Console.WriteLine("Its a TransactionEvent");
                        break;
                    case "Authorize":
                        Console.WriteLine("Its a Authorize");
                        break;
                    
                    default:
                        Console.WriteLine("Något okänt meddelande!");
                        break;
                }
                
                
                boot.Clear();

                


                





            }

            protected override void OnClose(CloseEventArgs e)
            {
                Console.WriteLine("A client disconnected  :(");
            }
            protected override void OnError(WebSocketSharp.ErrorEventArgs e)
            {
                Console.WriteLine("Error!! :(");
            }
        }


        static void Main(string[] args)
        {
            string _url = "ws://127.0.0.1:80";
            WebSocketServer wssv = new WebSocketServer(_url);

            wssv.AddWebSocketService<Echo>("/Echo");
            

            wssv.Start();
            Console.WriteLine($"Server started on: {_url}");
            Console.ReadLine();
            wssv.Stop();

        }
        
    }
}
