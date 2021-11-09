using System;
using System.IO;

using Spire.Pdf;
using Spire.Pdf.Texts;
using System.Web;
using Spire.Pdf.General.Find;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace CCIAutoCreateSOFromPDF.Model
{
    class chrysanHeader
    {
        public string headerLeft { get; set; }
        public string headerTel { get; set; }
        public string headerFax { get; set; }
        public string supplier { get; set; }
        public string attention { get; set; }
        public string shipTo { get; set; }
        public string purchId { get; set; }
        public string PurchDate { get; set; }
        public string shipDate { get; set; }
        public string revisionDate { get; set; }
        public string revision { get; set; }
        public string orderedBy { get; set; }
        public string supplierPhone { get; set; }
        public string suplierFax { get; set; }
        public string supplierCustCode { get; set; }
        public string supplierFax { get; set; }
        public string pymtTerms { get; set; }
        public string fob { get; set; }
        public string incoTerms { get; set; }
        public string freightTerms { get; set; }
        public string shipFrom { get; set; }
        public string note { get; set; }
        public Dictionary<string, chrysanDetails> line { get; set; }

        public List<SalesLine> salesLine { get; set; }
    }

    class chrysanDetails
    {
        public string part { get; set; }
        public string supplierPartNo { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string dueDate { get; set; }
        public string orderQty { get; set; }
        public string unitPrice { get; set; }
        public string setupCharge { get; set; }
        public string extendedPrice { get; set; }

    }

    class toyotaDetail
    {
        public string description { get; set; }
        public string orderNumber { get; set; }
        public string partNumber { get; set; }
        public string orderQty { get; set; }
        public string unit { get; set; }
        public string kanban { get; set; }
        public string dlvDate { get; set; }
        public string unitPrice { get; set; }
        public string familyId { get; set; }
        public string packingStyle { get; set; }
    }

    class toyotaHeader
    {
        public string supplier { get; set; }
        public string supplierCode { get; set; }
        public string receivingLocation { get; set; }
        public string supplierShortName { get; set; }
        public string printDateTime { get; set; }
        public List<SalesLine> salesLine { get; set; }
        public List<toyotaDetail> lines { get; set; }
    }

    class SalesLine
    {
        public string itemId { get; set; }
        public double qty { get; set; }
        public string unit { get; set; }
    }
    class ProcessPDF
    {
        public static chrysanHeader processChrysan(Stream _path)
        {
            PdfDocument document = new PdfDocument(_path);

            string headerTitleLeft = string.Empty;
            string headerTitleRight = string.Empty;
            Boolean newLine = false;
            chrysanHeader chrysanPO = new chrysanHeader();
            Dictionary<string, chrysanDetails> line = new Dictionary<string, chrysanDetails>();
            chrysanDetails details = null;
            bool firstPage = true;
            bool IgnoreLine = false;
            string itemRel = string.Empty;

            foreach (PdfPageBase page in document.Pages)
            {
                bool isLine = false;
                foreach (var pdfTextFind in page.FindAllText().Finds)
                {
                    if (pdfTextFind.Position.X == 294
                        && pdfTextFind.MatchText.Equals("Items"))
                    {
                        isLine = true;
                    }

                    if (pdfTextFind.MatchText.Contains("Sub Total:")
                        || pdfTextFind.MatchText.Contains("Items"))
                    {
                        IgnoreLine = true;
                    }
                    if (!isLine && firstPage)
                    {
                        if (pdfTextFind.Position.X == 306.75f)
                        {
                            headerTitleRight = pdfTextFind.MatchText;
                        }

                        if (pdfTextFind.Position.X == 60)
                        {
                            headerTitleLeft = pdfTextFind.MatchText;
                        }

                        if (pdfTextFind.Position.X == 198
                            && pdfTextFind.Position.Y <= 70)
                        {
                            if (string.IsNullOrEmpty(chrysanPO.headerLeft))
                            {
                                chrysanPO.headerLeft += pdfTextFind.MatchText;
                            }
                            else
                            {
                                chrysanPO.headerLeft += " " + pdfTextFind.MatchText;
                            }
                        }
                        if (pdfTextFind.Position.X == 198
                            && pdfTextFind.Position.Y == 79.65f)
                        {
                            chrysanPO.headerTel = pdfTextFind.MatchText;

                        }

                        if (pdfTextFind.Position.X == 198
                            && pdfTextFind.Position.Y == 89.4f)
                        {
                            chrysanPO.headerFax = pdfTextFind.MatchText;
                        }
                        #region headerleft
                        if (pdfTextFind.Position.X >= 108
                            && pdfTextFind.Position.X < 305)
                        {
                            switch (headerTitleLeft)
                            {
                                case "Supplier:":
                                    if (string.IsNullOrEmpty(chrysanPO.supplier))
                                    {
                                        chrysanPO.supplier += pdfTextFind.MatchText;
                                    }
                                    else
                                    {
                                        chrysanPO.supplier += "\n" + pdfTextFind.MatchText;
                                    }
                                    break;
                                case "Attention:":
                                    chrysanPO.attention += pdfTextFind.MatchText;
                                    break;
                                case "Ship To:":
                                    chrysanPO.shipTo += pdfTextFind.MatchText;
                                    break;
                            }
                        }
                        #endregion
                        #region headerRight
                        if (pdfTextFind.Position.X >= 384)
                        {
                            switch (headerTitleRight)
                            {
                                case "PO No:":
                                    chrysanPO.purchId = pdfTextFind.MatchText;
                                    break;
                                case "PO Date:":
                                    chrysanPO.PurchDate = pdfTextFind.MatchText;
                                    break;
                                case "Ship Date:":
                                    chrysanPO.shipDate = pdfTextFind.MatchText;
                                    break;
                                case "Revision:":
                                    chrysanPO.revision = pdfTextFind.MatchText;
                                    break;
                                case "Revision Date:":
                                    chrysanPO.revisionDate = pdfTextFind.MatchText;
                                    break;
                                case "Ordered By:":
                                    if (string.IsNullOrEmpty(chrysanPO.orderedBy))
                                    {
                                        chrysanPO.orderedBy = pdfTextFind.MatchText;
                                    }
                                    else
                                    {
                                        chrysanPO.orderedBy += "\n" + pdfTextFind.MatchText;
                                    }
                                    break;
                                case "Blanket Order":
                                    break;
                                case "Pymt Terms:":
                                    chrysanPO.pymtTerms = pdfTextFind.MatchText;
                                    break;
                                case "FOB:":
                                    chrysanPO.fob = pdfTextFind.MatchText;
                                    break;
                                case "INCO Terms:":
                                    chrysanPO.incoTerms = pdfTextFind.MatchText;
                                    break;
                                case "Ship From:":
                                    chrysanPO.shipFrom = pdfTextFind.MatchText;
                                    break;
                                case "Freight Terms:":
                                    chrysanPO.freightTerms = pdfTextFind.MatchText;
                                    break;
                                case "Note:":
                                    if (string.IsNullOrEmpty(chrysanPO.note))
                                    {
                                        chrysanPO.note = pdfTextFind.MatchText;
                                    }
                                    else
                                    {
                                        chrysanPO.note += " " + pdfTextFind.MatchText;
                                    }
                                    break;
                            }
                        }
                        #endregion
                    }
                    if (pdfTextFind.Position.X == 84
                        || (pdfTextFind.Position.X > 70
                        && pdfTextFind.Position.X < 82)
                        && System.Text.RegularExpressions.Regex.IsMatch(pdfTextFind.MatchText, @"\d+:\d+"))
                    {
                        if (string.IsNullOrEmpty(itemRel)
                            || !string.Equals(pdfTextFind.MatchText, itemRel))
                        {
                            newLine = true;
                            IgnoreLine = false;
                            if (details != null)
                            {
                                line.Add(itemRel, details);
                            }
                        }
                        itemRel = pdfTextFind.MatchText;
                    }
                    else
                    {
                        newLine = false;
                    }

                    if (newLine)
                    {
                        details = new chrysanDetails();
                    }
                    if (!string.IsNullOrEmpty(itemRel) && !IgnoreLine)
                    {
                        double x = pdfTextFind.Position.X;

                        switch (x)
                        {
                            case 123:
                                if (pdfTextFind.Size.Width < 100)
                                {
                                    details.part += pdfTextFind.MatchText;
                                }
                                break;
                            case 173.25f:
                                details.supplierPartNo += pdfTextFind.MatchText;
                                break;
                            case 217.5f:
                                details.description += pdfTextFind.MatchText;
                                break;
                        }

                        if (x >= 328
                            && x <= 339
                            && string.IsNullOrEmpty(details.status))
                        {
                            details.status = pdfTextFind.MatchText;
                        }
                        if (x > 349
                            && x < 398
                            && string.IsNullOrEmpty(details.dueDate))
                        {
                            details.dueDate = pdfTextFind.MatchText;
                        }
                        if (x > 399
                            && x < 435
                            && string.IsNullOrEmpty(details.orderQty))
                        {
                            details.orderQty = pdfTextFind.MatchText;
                        }
                        if (x > 431
                            && x < 458
                            && string.IsNullOrEmpty(details.unitPrice))
                        {
                            details.unitPrice = pdfTextFind.MatchText;
                        }
                        if (x > 460
                            && x < 505
                            && string.IsNullOrEmpty(details.setupCharge))
                        {
                            details.setupCharge = pdfTextFind.MatchText;
                        }

                        if (x > 505
                            && string.IsNullOrEmpty(details.extendedPrice))
                        {
                            details.extendedPrice = pdfTextFind.MatchText;
                        }
                    }

                    if (pdfTextFind.MatchText.Contains("Grand Total:") && details != null)
                    {
                        line.Add(itemRel, details);
                        chrysanPO.line = line;
                        break;
                    }
                }

                firstPage = false;
            }
            
            List<SalesLine> salesLines = new List<SalesLine>();
            SalesLine salesLine = null;
            foreach (var detail in line)
            {
                chrysanDetails salesDetail = detail.Value;

                if (System.Text.RegularExpressions.Regex.IsMatch(detail.Key, @"\d+:0"))
                {
                    if (salesLine != null)
                    {
                        salesLines.Add(salesLine);
                    }
                    salesLine = new SalesLine();
                    salesLine.itemId = salesDetail.supplierPartNo;
                    salesLine.unit = salesDetail.unitPrice.Split('/')[1];
                    salesLine.qty = 0;
                    continue;
                }
                salesLine.qty += double.Parse(salesDetail.orderQty);
            }
            chrysanPO.salesLine = salesLines;
            return chrysanPO;
        }

        public static toyotaHeader processToyota(Stream _path)
        {
            PdfDocument document = new PdfDocument(_path);
            string headerTitleLeft = string.Empty;
            toyotaHeader header = new toyotaHeader();
            bool isLine = false;
            bool isHeader = true;
            toyotaDetail detail = null;
            List<toyotaDetail> line = new List<toyotaDetail>();
            foreach (PdfPageBase page in document.Pages)
            {
                foreach (var pdfTextFind in page.FindAllText().Finds)
                {
                    if (pdfTextFind.Position.X == 9)
                    {
                        if (string.Equals(pdfTextFind.MatchText, "DESCRIPTION"))
                        {
                            isLine = true;
                        }
                        else
                        {
                            headerTitleLeft = pdfTextFind.MatchText;
                        }
                    }

                    if (!isLine && isHeader)
                    {
                        if (pdfTextFind.Position.X >= 128
                            && pdfTextFind.Position.X < 300)
                        {
                            switch (headerTitleLeft)
                            {
                                case "SUPPLIER: ":
                                    header.supplier = pdfTextFind.MatchText;
                                    break;
                                case "SUPPLIER CODE: ":
                                    header.supplierCode = pdfTextFind.MatchText;
                                    break;
                                case "SUPPLIER SHORT NAME: ":
                                    header.supplierShortName = pdfTextFind.MatchText;
                                    break;
                                case "PRINT DATE/TIME: ":
                                    header.printDateTime += pdfTextFind.MatchText + " ";
                                    if (System.Text.RegularExpressions.Regex.IsMatch(pdfTextFind.MatchText, @"^\d{2}:\d{2}:\d{2}"))
                                    {
                                        isHeader = false;
                                    }
                                    break;
                            }
                        }
                        if (pdfTextFind.Position.X > 308)
                        {
                            header.receivingLocation = pdfTextFind.MatchText;
                        }
                    }
                    else
                    {
                        if (pdfTextFind.Position.X == 9
                            && !pdfTextFind.MatchText.Equals("DESCRIPTION")
                            && !pdfTextFind.MatchText.Equals("MFG NAME:   "))
                        {
                            if (detail != null)
                            {
                                line.Add(detail);
                            }
                            detail = new toyotaDetail();
                            detail.description = pdfTextFind.MatchText;
                        }
                        if (detail != null &&
                            pdfTextFind.Position.X == 218)
                        {
                            if (!string.Equals(pdfTextFind.MatchText, "UNIT PRICE")
                                && !pdfTextFind.MatchText.StartsWith("$"))
                            {
                                detail.orderNumber = pdfTextFind.MatchText;
                            }
                            else
                            {
                                detail.unitPrice = pdfTextFind.MatchText;
                            }

                        }
                        if (detail != null &&
                            pdfTextFind.Position.X == 283)
                        {
                            detail.partNumber = pdfTextFind.MatchText;
                        }
                        if (detail != null &&
                            pdfTextFind.Position.X > 285
                            && pdfTextFind.Position.X < 447)
                        {
                            if (pdfTextFind.Position.X == 298)
                            {
                                if (!string.Equals(pdfTextFind.MatchText, "FAMILY ID"))
                                {
                                    detail.familyId = pdfTextFind.MatchText;
                                }
                            }
                            else
                            {
                                detail.orderQty += pdfTextFind.MatchText;
                            }
                        }
                        if (detail != null &&
                            pdfTextFind.Position.X == 448)
                        {
                            detail.unit = pdfTextFind.MatchText;
                        }
                        if (detail != null &&
                            pdfTextFind.Position.X == 458
                            && !pdfTextFind.MatchText.Equals("PACKING STYLE"))
                        {
                            detail.packingStyle = pdfTextFind.MatchText;
                        }
                        if (detail != null &&
                            pdfTextFind.Position.X > 490
                            && pdfTextFind.Position.X < 531)
                        {
                            detail.kanban = pdfTextFind.MatchText;
                        }
                        if (detail != null &&
                            pdfTextFind.Position.X > 535)
                        {
                            detail.dlvDate = pdfTextFind.MatchText;
                        }
                        if (pdfTextFind.MatchText.Equals("MFG NAME:   "))
                        {
                            if (detail != null)
                            {
                                line.Add(detail);
                            }
                            break;
                        }
                    }
                }
            }
            header.lines = line;

            List<SalesLine> salesLines = new List<SalesLine>();
            SalesLine salesLine = null;
            foreach (var detail1 in line)
            {
                salesLine = new SalesLine();
                salesLine.itemId = detail1.partNumber;
                salesLine.qty = double.Parse(detail1.orderQty);
                salesLine.unit = detail1.unit;
                salesLines.Add(salesLine);
            }
            header.salesLine = salesLines;
            return header;
        }

    }
}
