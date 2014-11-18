using System;
using System.Configuration;
using System.Globalization;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using org.xml.sax;
using org.apache.fop.apps;
using TractInc.Expense.Domain;
using TractInc.Expense.Entity;

namespace TractInc.Expense
{

class InvoiceProcessor
{

    private const int DAILY_INVOICE_ITEM_TYPE_ID = 1;

    private const String EXPENSE_DIR_CONFIG_KEY = "ExpenseAppDir";

    private const String PDFSAM_EXE_CONFIG_KEY = "PDFSAMExePath";

    private const String STORAGE_DIR_CONFIG_KEY = "ExpenseFileStorageDir";

    private InvoiceDataMapper m_invoiceDM;
    private InvoiceItemTypeDataMapper m_itemTypeDM;
    private AfeDataMapper m_afeDM;
    private SubAfeDataMapper m_subafeDM;
    private AssetDataMapper m_assetDM;
    private InvoiceItemDataMapper m_itemDM;
    private AssetAssignmentDataMapper m_assignmentDM;
    private WorkLogDataMapper m_workLogDM;

    public static String getInvoiceUrl(int invoiceId) {
        return Uploader.StorageUrl + invoiceId.ToString() + " invoice.pdf";
    }

    public static String getCoverUrl(int invoiceId)
    {
        return Uploader.StorageUrl + invoiceId.ToString() + " cover.pdf";
    }

    public static String getWorkLogUrl(int invoiceId)
    {
        return Uploader.StorageUrl + invoiceId.ToString() + " worklog.pdf";
    }

    public static String getAttachmentsUrl(int invoiceId)
    {
        return Uploader.StorageUrl + invoiceId.ToString() + " attachments.pdf";
    }

    public String ProcessInvoice(int invoiceId)
    {
        String invoiceName = Uploader.StorageDir + invoiceId.ToString() + " invoice";

        buildInvoiceXML(invoiceName, invoiceId);
        applyStyle(invoiceName, "invoice.xsl");

        return getInvoiceUrl(invoiceId);
    }

    public String ProcessCover(int invoiceId)
    {
        String coverName = Uploader.StorageDir + invoiceId.ToString() + " cover";

        buildCoverXML(coverName, invoiceId);
        applyStyle(coverName, "cover.xsl");

        return getCoverUrl(invoiceId);
    }

    public String ProcessWorkLog(int invoiceId)
    {
        String workLogName = Uploader.StorageDir + invoiceId.ToString() + " worklog";

        buildWorkLogXML(workLogName, invoiceId);
        applyStyle(workLogName, "worklog.xsl");

        return getWorkLogUrl(invoiceId);
    }

    private void applyStyle(String invoiceName, String styleName)
    {
        String xmlFile = invoiceName + ".xml";
        String foFile = xmlFile + ".fo";
        String xslFile = ConfigurationManager.AppSettings[EXPENSE_DIR_CONFIG_KEY] + styleName;
        String pdfFile = invoiceName + ".pdf";

        System.Xml.Xsl.XslCompiledTransform xslt = new System.Xml.Xsl.XslCompiledTransform();
        xslt.Load(new XmlTextReader(xslFile));
        xslt.Transform(xmlFile, foFile);

        java.io.FileInputStream input = new java.io.FileInputStream(foFile);
        InputSource source = new InputSource(input);
        java.io.FileOutputStream output = new java.io.FileOutputStream(pdfFile);
        Driver driver = new Driver(source, output);
        driver.setRenderer(Driver.RENDER_PDF);
        driver.run();
        output.close();
    }

    private void buildInvoiceXML(String fileName, int invoiceId) {
        int i;
        XmlTextWriter writer;
        Invoice invoice;
        List<InvoiceItem> items;
        List<InvoiceItemType> types;
        String afeKey;
        Hashtable afesHash;
        String landmanKey;
        Hashtable landmansHash;
        String typeKey;
        Hashtable typesHash;
        FloatValue amount;
        Afe afe;
        Asset asset;
        AssetAssignment assignment;
        float totalAmount;
        IFormatProvider culture = new CultureInfo("en-US", true);

        invoice = InvoiceDM.findByPrimaryKey(invoiceId);
        if (null == invoice) {
            throw new Exception("Invoice not found");
        }

        items = (List<InvoiceItem>)ItemDM.findBySql("select * from InvoiceItem where InvoiceId = " + invoiceId.ToString()).Result;

        types = (List<InvoiceItemType>)ItemTypeDM.findAll(new Hashtable(0)).Result;

        afesHash = new Hashtable();
        for (i = 0; i < items.Count; i++) {
            if (!items[i].IsSelected) {
                continue;
            }

            assignment = AssignmentDM.findByPrimaryKey(items[i].AssetAssignmentId);
            afeKey = assignment.AFE;
            if (null == afesHash[afeKey]) {
                afesHash[afeKey] = new Hashtable();
            }

            landmanKey = assignment.AssetId.ToString();
            landmansHash = (Hashtable)afesHash[afeKey];
            if (null == landmansHash[landmanKey]) {
                landmansHash[landmanKey] = new Hashtable();
            }

            typesHash = (Hashtable)landmansHash[landmanKey];
            if (0 == typesHash.Count) {
                foreach (InvoiceItemType type in types) {
                    typesHash[type.InvoiceItemTypeId.ToString()] = new FloatValue(0);
                }
            }

            typeKey = items[i].InvoiceItemTypeId.ToString();

            ((FloatValue)typesHash[typeKey]).add((float)(items[i].InvoiceRate * items[i].Qty));
            ((FloatValue)typesHash[typeKey]).addQuantity((int)items[i].Qty);
        }

        writer = new XmlTextWriter(fileName + ".xml", Encoding.UTF8);
        writer.WriteStartDocument();
        writer.WriteStartElement("invoice");
        writer.WriteAttributeString("dateFrom", invoice.StartDate);
        writer.WriteAttributeString("dateTo", getDateTo(invoice.StartDate));
        writer.WriteAttributeString("invoice", invoice.InvoiceNumber.ToString());

        writer.WriteStartElement("types");
        for (i = 0; i < types.Count; i++) {
            writer.WriteStartElement("type");
            writer.WriteAttributeString("id", types[i].InvoiceItemTypeId.ToString());
            writer.WriteAttributeString("name", types[i].Name);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteStartElement("afes");
        foreach (Object afeKeyObject in afesHash.Keys) {
            writer.WriteStartElement("afe");

            afe = AfeDM.findByPrimaryKey((String)afeKeyObject);
            writer.WriteAttributeString("code", afe.AFE);
            writer.WriteAttributeString("name", afe.AFEName);

            writer.WriteStartElement("landmans");
            landmansHash = (Hashtable)afesHash[afeKeyObject];
            foreach (Object landmanKeyObject in landmansHash.Keys) {
                writer.WriteStartElement("landman");
                Asset a = new Asset();
                
                asset = AssetDM.findByPrimaryKey(Int32.Parse((String)landmanKeyObject));
                writer.WriteAttributeString("firstName", asset.FirstName);
                writer.WriteAttributeString("lastName", asset.LastName);

                typesHash = (Hashtable)landmansHash[landmanKeyObject];
                amount = (FloatValue)typesHash[DAILY_INVOICE_ITEM_TYPE_ID.ToString()];
                totalAmount = amount.value;

                foreach (Object typeKeyObject in typesHash.Keys) {
                    if (DAILY_INVOICE_ITEM_TYPE_ID.ToString() == (String)typeKeyObject) {
                        continue;
                    }
                    totalAmount += ((FloatValue)typesHash[typeKeyObject]).value;
                }
                
                writer.WriteAttributeString("days", ((float) amount.quantity / 8).ToString());
                writer.WriteAttributeString("cost", (Math.Round(amount.value * 100) / 100).ToString("C", culture));
                writer.WriteAttributeString("total", (Math.Round(totalAmount * 100) / 100).ToString("C", culture));

                foreach (Object typeKeyObject in typesHash.Keys) {
                    if (DAILY_INVOICE_ITEM_TYPE_ID.ToString() == (String)typeKeyObject) {
                        continue;
                    }

                    amount = (FloatValue)typesHash[typeKeyObject];

                    writer.WriteStartElement("item");
                    writer.WriteAttributeString("typeId", (String)typeKeyObject);
                    writer.WriteAttributeString("cost", (Math.Round(amount.value * 100) / 100).ToString("C", culture));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteEndDocument();
        writer.Flush();
        writer.Close();
    }

    private void buildCoverXML(String fileName, int invoiceId) {
        int i;
        XmlTextWriter writer;
        Invoice invoice;
        List<InvoiceItem> items;
        List<InvoiceItemType> types;
        Hashtable landmansHash;
        Asset asset;

        invoice = InvoiceDM.findByPrimaryKey(invoiceId);
        if (null == invoice) {
            throw new Exception("Invoice not found");
        }

        items = (List<InvoiceItem>)ItemDM.findBySql("select * from InvoiceItem where InvoiceId = " + invoiceId.ToString()).Result;

        types = (List<InvoiceItemType>)ItemTypeDM.findAll(new Hashtable(0)).Result;
        Hashtable typesHash = new Hashtable();

        landmansHash = new Hashtable();
        int totalDays = 0;
        float totalRate = 0;
        float totalAmount = 0;
        float totalExpense = 0;
        int itemsProcessed = 0;
        for (i = 0; i < items.Count; i++) {
            if (!items[i].IsSelected) {
                continue;
            }

            AssetAssignment assignment = AssignmentDM.findByPrimaryKey(items[i].AssetAssignmentId);

            LandmanInfo info;
            if (DAILY_INVOICE_ITEM_TYPE_ID == items[i].InvoiceItemTypeId) {
                if (null == landmansHash[assignment.AssetId]) {
                    info = new LandmanInfo();
                    asset = AssetDM.findByPrimaryKey(assignment.AssetId);
                    info.firstName = asset.FirstName;
                    info.lastName = asset.LastName;
                    landmansHash[assignment.AssetId] = info;
                } else {
                    info = (LandmanInfo)landmansHash[assignment.AssetId];
                }

                float amt = (float)(items[i].InvoiceRate * items[i].Qty);
                info.days += (int)items[i].Qty;
                info.rate += (float)items[i].InvoiceRate;
                info.amount += amt;
                totalDays += (int)items[i].Qty;
                totalRate += (float)items[i].InvoiceRate;
                totalAmount += amt;
                itemsProcessed++;
            } else {
                int typeKey = items[i].InvoiceItemTypeId;
                if (null == typesHash[typeKey]) {
                    typesHash[typeKey] = new FloatValue(0);
                }
                float amt = (float)(items[i].InvoiceRate * items[i].Qty);
                totalExpense += amt;
                ((FloatValue)typesHash[typeKey]).add(amt);
            }
        }

        writer = new XmlTextWriter(fileName + ".xml", Encoding.UTF8);
        writer.WriteStartDocument();
        writer.WriteStartElement("cover");
        writer.WriteAttributeString("dir", ConfigurationManager.AppSettings[EXPENSE_DIR_CONFIG_KEY]);
        CultureInfo culture = new CultureInfo("en-US", true);
        writer.WriteAttributeString("dateFrom", DateTime.Parse(invoice.StartDate, culture).ToString("d"));
        writer.WriteAttributeString("dateTo", getDateTo(invoice.StartDate));
        writer.WriteAttributeString("date", DateTime.Now.ToString("d"));
        writer.WriteAttributeString("invoice", invoice.InvoiceNumber.ToString());
        writer.WriteAttributeString("address", invoice.ClientAddress);
        writer.WriteAttributeString("companyName", invoice.ClientName);

        writer.WriteStartElement("landmans");
        writer.WriteAttributeString("days", ((float)totalDays / 8).ToString());
        writer.WriteAttributeString("rate", (totalRate / itemsProcessed).ToString("###,###,###,###.000"));
        writer.WriteAttributeString("amount", (Math.Round(totalAmount * 100) / 100).ToString("C", culture));
        foreach (LandmanInfo info in landmansHash.Values) {
            writer.WriteStartElement("landman");
            writer.WriteAttributeString("firstName", info.firstName);
            writer.WriteAttributeString("lastName", info.lastName);
            writer.WriteAttributeString("days", ((float)info.days / 8).ToString());
            writer.WriteAttributeString("rate", info.rate.ToString("###,###,###,###.000", culture));
            writer.WriteAttributeString("amount", (Math.Round(info.amount * 100) / 100).ToString("C", culture));
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteStartElement("types");
        writer.WriteAttributeString("totalExpense", (Math.Round(totalExpense * 100) / 100).ToString("C", culture));
        writer.WriteAttributeString("landmanTotals", (Math.Round(totalAmount * 100) / 100).ToString("C", culture));
        writer.WriteAttributeString("grandTotal", (Math.Round((totalExpense + totalAmount) * 100) / 100).ToString("C", culture));
        foreach (Object key in typesHash.Keys) {
            writer.WriteStartElement("type");
            InvoiceItemType type = ItemTypeDM.findByPrimaryKey((int) key);
            writer.WriteAttributeString("description", type.Name);
            writer.WriteAttributeString("amount", (Math.Round(((FloatValue)typesHash[key]).value * 100) / 100).ToString("C", culture));
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteEndElement();

        writer.WriteEndDocument();
        writer.Flush();
        writer.Close();
    }

    private void buildWorkLogXML(String fileName, int invoiceId)
    {
        int i;
        XmlTextWriter writer;
        Invoice invoice;
        List<InvoiceItem> items;
        List<WorkLog> logItems;
        String subafeKey;
        Hashtable subafesHash;
        String landmanKey;
        Hashtable landmansHash;
        SubAfe subafe;
        Asset asset;
        AssetAssignment assignment;
        IFormatProvider culture = new CultureInfo("en-US", true);

        invoice = InvoiceDM.findByPrimaryKey(invoiceId);
        if (null == invoice)
        {
            throw new Exception("Invoice not found");
        }

        Hashtable logItemsHash = new Hashtable();

        items = (List<InvoiceItem>)ItemDM.findBySql("select * from InvoiceItem where InvoiceId = " + invoiceId.ToString()).Result;
        logItems = (List<WorkLog>)WorkLogDM.findBySql("select * from WorkLog where BillItemId in (select BillItemId from InvoiceItem where InvoiceId = " + invoiceId.ToString() + ")").Result;

        foreach (WorkLog logItem in logItems)
        {
            logItemsHash[logItem.BillItemId] = logItem;
        }

        subafesHash = new Hashtable();
        for (i = 0; i < items.Count; i++)
        {
            if (!items[i].IsSelected || (1 != items[i].InvoiceItemTypeId))
            {
                continue;
            }

            assignment = AssignmentDM.findByPrimaryKey(items[i].AssetAssignmentId);
            subafeKey = assignment.SubAFE;
            if (null == subafesHash[subafeKey])
            {
                subafesHash[subafeKey] = new Hashtable();
            }

            landmanKey = assignment.AssetId.ToString();
            landmansHash = (Hashtable)subafesHash[subafeKey];
            if (null == landmansHash[landmanKey])
            {
                landmansHash[landmanKey] = new ArrayList();
            }

            ((ArrayList)landmansHash[landmanKey]).Add(items[i]);
        }

        writer = new XmlTextWriter(fileName + ".xml", Encoding.UTF8);
        writer.WriteStartDocument();
        writer.WriteStartElement("invoice");
        writer.WriteAttributeString("dateFrom", invoice.StartDate);
        writer.WriteAttributeString("dateTo", getDateTo(invoice.StartDate));
        writer.WriteAttributeString("invoice", invoice.InvoiceNumber.ToString());

        writer.WriteStartElement("projects");
        foreach (Object subafeKeyObject in subafesHash.Keys)
        {
            writer.WriteStartElement("project");

            subafe = SubAfeDM.findByPrimaryKey((String)subafeKeyObject);
            writer.WriteAttributeString("code", subafe.ShortName);
            writer.WriteAttributeString("name", subafe.SubAFE);

            writer.WriteStartElement("landmans");
            landmansHash = (Hashtable)subafesHash[subafeKeyObject];
            foreach (Object landmanKeyObject in landmansHash.Keys)
            {
                writer.WriteStartElement("landman");
                Asset a = new Asset();

                asset = AssetDM.findByPrimaryKey(Int32.Parse((String)landmanKeyObject));
                writer.WriteAttributeString("firstName", asset.FirstName);
                writer.WriteAttributeString("lastName", asset.LastName);

                ArrayList itemsList = (ArrayList)landmansHash[landmanKeyObject];

                foreach (InvoiceItem item in itemsList)
                {
                    if (null == item.BillItemId)
                    {
                        continue;
                    }

                    if (null == logItemsHash[item.BillItemId])
                    {
                        continue;
                    }

                    writer.WriteStartElement("item");
                    writer.WriteAttributeString("date", item.InvoiceDate);
                    writer.WriteAttributeString("hours", item.Qty.ToString());
                    writer.WriteAttributeString("log", ((WorkLog)logItemsHash[item.BillItemId]).LogMessage);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteEndDocument();
        writer.Flush();
        writer.Close();
    }

    public String ProcessAttachments(int invoiceId)
    {
        List<ReportAttachmentDataObject> attachments;

        using (SqlConnection conn = SqlHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            String attachmentsName = Uploader.StorageDir + invoiceId.ToString() + " attachments";

            attachments = TractInc.Expense.Data.ReportAttachment.GetInstance().GetInvoiceAttachments(tran, invoiceId);

            tran.Commit();
        }

        string resultFileName = ConfigurationManager.AppSettings[STORAGE_DIR_CONFIG_KEY] + invoiceId.ToString() + " attachments.pdf";

        if (1 == attachments.Count)
        {
            File.Copy(
                ConfigurationManager.AppSettings[STORAGE_DIR_CONFIG_KEY] + "BillItems/" + attachments[0].FileName,
                resultFileName,
                true);
        }
        else if (0 == attachments.Count)
        {
            return null;
        }
        else
        {
            if (File.Exists(resultFileName))
            {
                File.Delete(resultFileName);
            }

            string filesListPath = Path.GetTempPath() + Path.GetRandomFileName() + ".csv";
            StreamWriter filesListWriter = new StreamWriter(filesListPath);
    
            int attachmentsLeft = attachments.Count;
            foreach (ReportAttachmentDataObject attachmentInfo in attachments)
            {
                filesListWriter.Write(ConfigurationManager.AppSettings[STORAGE_DIR_CONFIG_KEY] + "BillItems/" + attachmentInfo.FileName);
                    
                attachmentsLeft--;
                if (0 < attachmentsLeft)
                {
                    filesListWriter.Write(",");
                }
            }
            filesListWriter.Flush();
            filesListWriter.Close();
            
            ProcessStartInfo startInfo = new ProcessStartInfo("java.exe", "-jar "
                + ConfigurationManager.AppSettings[PDFSAM_EXE_CONFIG_KEY]
                + " -l \""
                + filesListPath + "\" -o \""
                + resultFileName + "\" concat");
            StreamWriter outputWriter = new StreamWriter(ConfigurationManager.AppSettings[EXPENSE_DIR_CONFIG_KEY] + "pdfsam.log");

            Process pdfsamProcess = new Process();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            pdfsamProcess.StartInfo = startInfo;
            pdfsamProcess.Start();

            outputWriter.WriteLine(startInfo.Arguments);
            outputWriter.Write(pdfsamProcess.StandardOutput.ReadToEnd());
            outputWriter.Close();
        }

        return getAttachmentsUrl(invoiceId);
    }

    private InvoiceDataMapper InvoiceDM
    {
        get
        {
            if (null == m_invoiceDM)
            {
                m_invoiceDM = new InvoiceDataMapper();
            }
            return m_invoiceDM;
        }
    }

    private InvoiceItemDataMapper ItemDM
    {
        get
        {
            if (null == m_itemDM)
            {
                m_itemDM = new InvoiceItemDataMapper();
            }
            return m_itemDM;
        }
    }

    private InvoiceItemTypeDataMapper ItemTypeDM
    {
        get
        {
            if (null == m_itemTypeDM)
            {
                m_itemTypeDM = new InvoiceItemTypeDataMapper();
            }
            return m_itemTypeDM;
        }
    }

    private AfeDataMapper AfeDM
    {
        get
        {
            if (null == m_afeDM)
            {
                m_afeDM = new AfeDataMapper();
            }
            return m_afeDM;
        }
    }

    private SubAfeDataMapper SubAfeDM
    {
        get
        {
            if (null == m_subafeDM)
            {
                m_subafeDM = new SubAfeDataMapper();
            }
            return m_subafeDM;
        }
    }

    private AssetDataMapper AssetDM
    {
        get
        {
            if (null == m_assetDM)
            {
                m_assetDM = new AssetDataMapper();
            }
            return m_assetDM;
        }
    }

    private AssetAssignmentDataMapper AssignmentDM
    {
        get
        {
            if (null == m_assignmentDM)
            {
                m_assignmentDM = new AssetAssignmentDataMapper();
            }
            return m_assignmentDM;
        }
    }

    private WorkLogDataMapper WorkLogDM
    {
        get
        {
            if (null == m_workLogDM)
            {
                m_workLogDM = new WorkLogDataMapper();
            }
            return m_workLogDM;
        }
    }

    private String getDateTo(String dateFromString)
    {
        IFormatProvider culture = new CultureInfo("en-US", true);
        DateTime dateFrom = DateTime.Parse(dateFromString, culture);
        if (1 == dateFrom.Day)
        {
            return dateFrom.Month.ToString() + "/15/" + dateFrom.Year;
        }
        else
        {
            return dateFrom.Month.ToString() + "/" + DateTime.DaysInMonth(dateFrom.Year, dateFrom.Month) + "/" + dateFrom.Year;
        }
    }

    internal class FloatValue
    {

        public int quantity;

        public float value;

        public FloatValue(float val)
        {
            value = val;
            quantity = 0;
        }

        public void add(float val)
        {
            value += val;
        }

        public void addQuantity(int qty)
        {
            quantity += qty;
        }

    }

    internal class LandmanInfo
    {

        public String firstName = null;
        public String lastName = null;
        public int days = 0;
        public float rate = 0;
        public float amount = 0;

    }

}

}
