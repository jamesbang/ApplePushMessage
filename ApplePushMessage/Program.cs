using MoonAPNS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ApplePushMessage
{
    class Program
    {
        private static readonly bool isPushProd = false;

        private static string getPushToken()
        {
            string tokenProd = "put your prod token";
            string tokenSandbox = "put your sandbox token";
            return isPushProd ? tokenProd : tokenSandbox;
        }

        private static PushNotification genPushNotification()
        {
            if (isPushProd)
            {
                return new PushNotification(false, "prod.p12 file", "p12 password");
            }
            else
            {
                return new PushNotification(!isPushProd, "sandbox.p12 file", "p12 password");
            }
        }

        private static void Main(string[] args)
        {
            // var payload1 = new NotificationPayload("Device token","Message",Badge,"Sound");
            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;

            var culture = new CultureInfo("en-US");
            Console.WriteLine("{0}:", culture.NativeName);
            Console.WriteLine("   Local date and time: {0}, {1:G}", localDate.ToString(culture), localDate.Kind);

            string pushMsg = String.Format("Message from MoonAPNS {0} {1}", isPushProd, localDate.ToString(culture));

            var payload1 = new NotificationPayload(getPushToken(), pushMsg, 1, "default");
            payload1.AddCustom("RegionID", "IDQ10150");

            var p = new List<NotificationPayload> { payload1 };
            Console.WriteLine("Send Push Message Ready...\n");
            Console.WriteLine(String.Format("Message: {0}\n", pushMsg));
            Console.WriteLine(String.Format("isPushProd: {0}, Token: {1}\n", isPushProd, getPushToken()));
            var push = genPushNotification();
            var rejected = push.SendToApple(p);
            if (rejected.Count > 0)
            {
                Console.WriteLine("Send Push Message Rejected...\n");
                foreach (var item in rejected)
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Send Push Message Success...\n");
            }
            
        }
    }
}
