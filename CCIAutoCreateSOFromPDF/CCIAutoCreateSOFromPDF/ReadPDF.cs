using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CCIAutoCreateSOFromPDF.Model;
using Newtonsoft.Json;
using System.Xml;
namespace CCIAutoCreateSOFromPDF
{
    
    public static class ReadPDF
    {
        [FunctionName("ReadPDF")]
        public static void Run([BlobTrigger("samples-workitems/{name}.pdf", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            chrysanHeader header = ProcessPDF.processChrysan(myBlob);
            string url = "https://prod-15.centralus.logic.azure.com:443/workflows/2b8fc149e144403ab399151b023d6ce2/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=2wAWRfUvR9suosI-m2e5L-Y7SRjjowiWDL6KA9IvbAk";

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
            headerChildNode.InnerText = "C313/01";
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
            foreach (var line in header.salesLine)
            {
                XmlNode itemNode = xmlDoc.CreateElement("Item");
                headerNode.AppendChild(itemNode);
                XmlNode itemChildNode = xmlDoc.CreateElement("ItemId");
                itemChildNode.InnerText = line.itemId;
                itemNode.AppendChild(itemChildNode);
                itemChildNode = xmlDoc.CreateElement("SalesQty");
                itemChildNode.InnerText = line.qty.ToString();
                itemNode.AppendChild(itemChildNode);
                itemChildNode = xmlDoc.CreateElement("Unit");
                itemChildNode.InnerText = line.unit;
                itemNode.AppendChild(itemChildNode);
            }

            string xml = xmlDoc.OuterXml;
            var requesetJson = new 
            {
                fileName =  $"{name}.xml",
                type = "830",
                documenttype = "1",
                legalentity = "06",
                xmlData = xml
            };
            
            string json = JsonConvert.SerializeObject(requesetJson);
            string result = SendXml.invoke(url, json);
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} Purchase number: {header.purchId};Result :{result} Json:{json}");
        }

        
    }
}
