using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CCIAutoCreateSOFromPDF.Model;
using System.Xml;

namespace CCI_CreateSO_DEV
{
    public static class CCIAutoCreateSOFromEmail
    {
        [FunctionName("CCI-AutoCreateSO")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string name = req.Headers["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //log.LogInformation(requestBody);
            //name = name ?? data?.name;
            //string pdfContentBytes = data?.contentBytes;

            MemoryStream stream = new MemoryStream(Convert.FromBase64String(requestBody));
            chrysanHeader chrysan = null;
            toyotaHeader toyota = null;
            salesLine salesLine = null;
            string custAccount = "";
            try
            {
                switch (int.Parse(req.Headers["DocumentType"]))
                {
                    case 1:
                        chrysan = ProcessPDF.processChrysan(stream);
                        salesLine = chrysan;
                        custAccount = "C313/01";
                        break;
                    case 2:
                        toyota = ProcessPDF.processToyota(stream);
                        salesLine = toyota;
                        custAccount = "C313/06";
                        break;

                }
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
                return new OkObjectResult(ex.Message);
            }

            if (salesLine == null || salesLine.SalesLines.Count == 0)
            {
                log.LogInformation("No Data");

                return new OkObjectResult("No Data" +
                    "" +
                    "");
            }

            #region create xml
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Document");
            xmlDoc.AppendChild(rootNode);
            XmlNode headerNode = xmlDoc.CreateElement("Header");
            rootNode.AppendChild(headerNode);
            XmlNode headerChildNode = xmlDoc.CreateElement("SenderId");
            headerChildNode.InnerText = "247267594";
            headerNode.AppendChild(headerChildNode);
            headerChildNode = xmlDoc.CreateElement("ReceiverId");
            headerChildNode.InnerText = "103312278";
            headerNode.AppendChild(headerChildNode);
            headerChildNode = xmlDoc.CreateElement("CCI_EDIDocumentType");
            headerChildNode.InnerText = "1";
            headerNode.AppendChild(headerChildNode);
            headerChildNode = xmlDoc.CreateElement("CustAccount");
            headerChildNode.InnerText = custAccount;
            headerNode.AppendChild(headerChildNode);
            headerChildNode = xmlDoc.CreateElement("DataExchangeNumber");
            headerChildNode.InnerText = "000010002";
            headerNode.AppendChild(headerChildNode);
            headerChildNode = xmlDoc.CreateElement("PurchOrderFormNum");
            headerChildNode.InnerText = "No Receiver";
            headerNode.AppendChild(headerChildNode);
            headerChildNode = xmlDoc.CreateElement("SalesType");
            headerChildNode.InnerText = "00";
            headerNode.AppendChild(headerChildNode);
            headerChildNode = xmlDoc.CreateElement("ReceiptDateRequested");
            headerChildNode.InnerText = "2021-10-10";
            headerNode.AppendChild(headerChildNode);

            foreach (var line in salesLine.SalesLines)
            {
                XmlNode itemNode = xmlDoc.CreateElement("Item");
                headerNode.AppendChild(itemNode);
                XmlNode itemChildNode = xmlDoc.CreateElement("ItemId");
                itemChildNode.InnerText = line.itemId.Trim();
                itemNode.AppendChild(itemChildNode);
                itemChildNode = xmlDoc.CreateElement("SalesQty");
                itemChildNode.InnerText = line.qty.ToString();
                itemNode.AppendChild(itemChildNode);
                itemChildNode = xmlDoc.CreateElement("Unit");
                itemChildNode.InnerText = line.unit.Trim();
                itemNode.AppendChild(itemChildNode);
            }
            #endregion

            string xml = xmlDoc.OuterXml;
            var requesetJson = new
            {
                fileName = $"{name.Split('.')[0]}.xml",
                type = req.Query["type"].ToString(),
                documenttype = req.Query["documentType"].ToString(),
                legalentity = req.Query["legalentity"].ToString(),
                xmlData = xml
            };

            //string json = JsonConvert.SerializeObject(requesetJson);
            //string result = SendXml.invoke(req.Query["url"], json);

            //log.LogInformation($"{name}; Result£º{result}");



            /*string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";*/

            return new OkObjectResult(requesetJson);
        }
    }
}
