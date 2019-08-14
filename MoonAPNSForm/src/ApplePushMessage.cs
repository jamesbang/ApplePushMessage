using MoonAPNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoonAPNSForm.src
{
    class ApplePushMessage
    {
        private bool isSandbox;
        private string deviceToken;
        private string p12FileName;
        private string p12Key;
        private string message;
        private string response;

        public void SetSandBox(bool value) {
            this.isSandbox = value;
        }

        public void SetDeviceToken(string value) {
            this.deviceToken = RemoveSpace(value);
        }

        public void SetP12FileName(string value)
        {
            this.p12FileName = RemoveSpace(value);
        }

        public void SetP12Key(string value)
        {
            this.p12Key = RemoveSpace(value);
        }

        public void SetMessage(string value)
        {
            this.message = RemoveSpace(value);
        }

        public bool GetSandBox()
        {
            return this.isSandbox;
        }

        public string GetDeviceToken()
        {
            return this.deviceToken;
        }

        public string GetP12FileName()
        {
            return this.p12FileName;
        }

        public string GetP12Key()
        {
            return this.p12Key;
        }

        public string GetMessage()
        {
            return this.message;
        }

        public string GetResponse() {
            return this.response;
        }

        private string RemoveSpace(string value) {
            return value.Replace(" ","");
        }

        public void Send() {
            //MoonAPNS
            string sendMessage = message;// String.Format("{0} {1}", message, Utils.getNowTime());
            var payload1 = new NotificationPayload(deviceToken, sendMessage, 1, "default");
            //payload1.AddCustom("RegionID", "IDQ10150");

            var p = new List<NotificationPayload> { payload1 };
           
            Console.WriteLine("Send Push Message Ready...\n");
            Console.WriteLine(String.Format("Message: {0}\n", sendMessage));
            Console.WriteLine(String.Format("isPushProd: {0}, Token: {1}\n", isSandbox, deviceToken));
            try
            {
                var push = new PushNotification(isSandbox, p12FileName, p12Key);
                var rejected = push.SendToApple(p);
               
                StringBuilder sb = new StringBuilder();
                sb.Append(String.Format("{0}{1}", Utils.getNowTime(), Environment.NewLine));

                if (rejected.Count > 0)
                {
                    Console.WriteLine("Send Push Message Reject...\n");
                    sb.Append(String.Format("{0}{1}", "Send Push Message Reject...", Environment.NewLine));
                    foreach (var item in rejected)
                    {
                        sb.Append(item);
                    }
                }
                else
                {
                    Console.WriteLine("Send Push Message Success...\n");
                    sb.Append(String.Format("{0}{1}", "Send Push Message Success...", Environment.NewLine));
                }
                response = sb.ToString();
            }
            catch (Exception e) {
                response = e.Message;
            }
        }

    }
}
