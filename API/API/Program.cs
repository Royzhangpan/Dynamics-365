using AuthenticationUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace API
{
    class Program
    {
        // In the AOT you will find UserSessionService in Service Groups and AifUserSessionService under Services.
        public static string sessionUrl = "/api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";
        //public static string sessionUrl = "/api/services/HSO_SharePointServiceGroup/BudgetManagement/GetJson";
        //public static string sessionUrl1 = "/api/services/HSO_SharePointServiceGroup/BudgetManagement/UpdateBudget";
        public static string callService(string _sessionUrl, string _value, bool _type = true)
        {
            string GetUserSessionOperationPath = string.Format("{0}{1}", ClientConfiguration.Default.UriString, _sessionUrl);
            string responseString;
            //string requestString;
            /*var requestContract = new
            {
                _templateId = _value,
            };
            var requestContract1 = new
            {
                _message = _value
            };
            Encoding encoding = Encoding.UTF8;

            byte[] byteArray;
            if (_type)
            {
                requestString = string.Format("{0}\"_templateId\":\"{1}\"{2}", "{", _value, "}");
            }
            else
            {
                //requestString = JsonConvert.SerializeObject(requestContract1);
                requestString = string.Format("{0}\"_message\":{1}{2}", "{", _value, "}");
            }
            byteArray = Encoding.UTF8.GetBytes(requestString);*/
            var request = HttpWebRequest.Create(GetUserSessionOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader(true);
            request.Method = "Post";
            request.ContentLength = 0;
            //request.ContentLength = byteArray.Length;

            /*Stream respStream = request.GetRequestStream();
            respStream.Write(byteArray, 0, byteArray.Length);//写入参数
            respStream.Close();*/
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        responseString = streamReader.ReadToEnd();
                    }
                }
            }
            return responseString;
        }
        static void Main(string[] args)
        {
            string outtr = callService(sessionUrl, callService(sessionUrl, "CWE-000003"), false);
            byte[] buffer = Encoding.Default.GetBytes(outtr);
            outtr = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(outtr);
            Console.ReadKey();
        }
    }
}
