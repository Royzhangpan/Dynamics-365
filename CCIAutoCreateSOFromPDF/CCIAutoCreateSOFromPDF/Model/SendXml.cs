using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace CCIAutoCreateSOFromPDF.Model
{
    class SendXml
    {
        public static string invoke(string _url, string _xml)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("content-length", "761");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("Host", "prod-15.centralus.logic.azure.com:443");
            request.AddHeader("Postman-Token", "ec22c231-61f3-4451-a505-254e53a3d433,18953477-43fd-4b19-bd88-dd621038133e");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.13.0");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", _xml, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                dynamic returnMessage = JsonConvert.DeserializeObject(response.Content);
                if ((bool)returnMessage?.success)
                {
                    return "Order creation was successful.";
                }
                else
                {
                    return returnMessage?.errorMsg;
                }
            }
            else
            {
                return response.ErrorMessage;
            }
        }
    }
}
