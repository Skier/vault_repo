<cfset currentPath = getCurrentTemplatePath()>
<cfset storageDirectory = "#getDirectoryFromPath(currentPath)#ExpenseFiles\InvoiceItemAttachments\">

<cfset invoiceFileName = "">
<cfset mergedPdfFileName = "">
<cfset finalPdfFileName = "">
<cfscript>
    invoiceFileName = getDirectoryFromPath(currentPath) & "ExpenseFiles\InvoiceProj_" & URL.invoiceId & ".pdf";
    mergedPdfFileName = getDirectoryFromPath(currentPath) & "ExpenseFiles\InvoiceProj_" & URL.invoiceId & "_merged.pdf";
    finalPdfFileName = getDirectoryFromPath(currentPath) & "ExpenseFiles\InvoiceProj_" & URL.invoiceId & "_final.pdf";
</cfscript>

<cfquery name="selectAttachments" datasource="LL_Expence">
    SELECT    ll_invoiceitem.projectid, ll_invoiceitem.invoiceitemid, ll_invoiceitem.itemdate, <cfqueryparam value="#storageDirectory#" cfsqltype="CF_SQL_VARCHAR"> || ll_file.storagekey as filename
    FROM      public.ll_invoice, public.ll_invoiceitem, public.ll_invoiceitemattachment, public.ll_file
    WHERE     public.ll_invoice.invoiceid = public.ll_invoiceitem.invoiceid
      AND     public.ll_invoice.invoiceid = <cfqueryparam value="#URL.invoiceId#" cfsqltype="CF_SQL_INTEGER">
      AND     public.ll_invoiceitem.invoiceitemid = public.ll_invoiceitemattachment.invoiceitemid
      AND     public.ll_invoiceitemattachment.fileid = public.ll_file.fileid
    ORDER BY ll_invoiceitem.projectid, ll_invoiceitem.itemdate, ll_invoiceitem.invoiceitemid
</cfquery>

<cfset pdfSources = "#invoiceFileName#">

<cfset attachHash = StructNew()>

<cfquery name="selectItems" datasource="LL_Expence">
    SELECT    ll_invoiceitem.invoiceitemid
    FROM      public.ll_invoice, public.ll_invoiceitem
    WHERE     public.ll_invoice.invoiceid = public.ll_invoiceitem.invoiceid
      AND     public.ll_invoice.invoiceid = <cfqueryparam value="#URL.invoiceId#" cfsqltype="CF_SQL_INTEGER">
</cfquery>

<cfloop query="selectItems">
    <cfscript>
        attachHash[selectItems.invoiceitemid] = 0;
    </cfscript>
</cfloop>

<cfquery name="selectInvoice" datasource="LL_Expence">
    SELECT    ll_invoice.invoiceno, ll_invoiceitem.invoiceitemid, ll_invoiceitem.itemdate, ll_invoice.startdate, ll_invoice.enddate, ll_invoiceitem.quantity, ll_invoiceitem.total, ll_invoice.invoicedate, ll_project.projectname, ll_invoiceitem.projectid, ll_expencetype.itemname AS expensename, ll_company.name AS companyname, clientperson.firstname AS clientfirstname, clientperson.middlename AS clientmiddlename, clientperson.lastname AS clientlastname, clientperson.phone AS clientphone, clientperson.phonealt AS clientphonealt, clientaddress.address1 AS clientaddress1, clientaddress.address2 AS clientaddress2, clientaddress.city AS clientcity, clientstate.name AS clientstatename, clientaddress.zip AS clientzip, clientstate.stateabbr AS clientstateabbr, ll_user.logourl, userperson.firstname AS userfirstname, userperson.middlename AS usermiddlename, userperson.lastname AS userlastname, userperson.phone AS userphone, userperson.phonealt AS userphonealt, useraddress.address1 AS useraddress1, useraddress.address2 AS useraddress2, useraddress.city AS usercity, userstate.name AS userstatename, useraddress.zip AS userzip, userstate.stateabbr AS userstateabbr
    FROM      public.ll_invoice, public.ll_invoiceitem, public.ll_client, public.ll_company, public.ll_person AS clientperson, public.ll_address AS clientaddress, public.ll_state AS clientstate, public.ll_expencetype, public.ll_project, public.ll_user, public.ll_login, public.ll_person AS userperson, public.ll_address AS useraddress, public.ll_state AS userstate 
    WHERE     public.ll_invoice.invoiceid = public.ll_invoiceitem.invoiceid
      AND     public.ll_invoice.invoiceid = <cfqueryparam value="#URL.invoiceId#" cfsqltype="CF_SQL_INTEGER">
      AND     public.ll_invoice.clientid = public.ll_client.clientid
      AND     public.ll_client.companyid = public.ll_company.companyid
      AND     public.ll_client.personid = clientperson.personid
      AND     clientperson.addressid = clientaddress.addressid
      AND     clientaddress.stateid = clientstate.stateid
      AND     public.ll_invoiceitem.expencetypeid = public.ll_expencetype.expencetypeid
      AND     public.ll_invoiceitem.projectid = public.ll_project.projectid
      AND     public.ll_client.clientid = public.ll_project.clientid
      AND     public.ll_invoice.userid = public.ll_user.userid
      AND     public.ll_user.loginid = public.ll_login.loginid
      AND     public.ll_login.personid = userperson.personid
      AND     userperson.addressid = useraddress.addressid
      AND     useraddress.stateid = userstate.stateid
    ORDER BY ll_invoiceitem.projectid, ll_project.projectname, ll_invoiceitem.itemdate, ll_invoiceitem.invoiceitemid    
</cfquery>

<cfreport template="InvoiceProjectFrom.cfr" format="pdf" query="selectInvoice" 
    filename="#invoiceFileName#" overwrite="yes">
    <cfreportparam name="invoiceid" value="#URL.invoiceId#"> 
    <cfreportparam name="attachHash" value="#attachHash#"> 
</cfreport>

<cfpdf action="getinfo" source="#invoiceFileName#" name="firstReport">

<cfset attachTotalPages = firstReport.totalpages>
<cfset prevId = 0>
<cfset currId = 0>

<cfloop query="selectAttachments">
    <cfif isPDFFile(selectAttachments.filename)>
        <cfpdf action="getinfo" source="#selectAttachments.filename#" name="pdfAttach">

        <cfscript>
            pdfSources = pdfSources & "," & selectAttachments.filename;

            if (pdfAttach.totalpages > 0) 
            {
                currId = selectAttachments.invoiceitemid;

                if (currId != prevId) 
                {
                    attachHash[currId] = attachTotalPages + 1;
                    prevId = currId;
                }

                attachTotalPages = attachTotalPages + pdfAttach.totalpages;
            }
        </cfscript>
    </cfif>
</cfloop>

<cfreport template="InvoiceProjectFrom.cfr" format="pdf" query="selectInvoice" 
    filename="#invoiceFileName#" overwrite="yes">
    <cfreportparam name="invoiceid" value="#URL.invoiceId#"> 
    <cfreportparam name="attachHash" value="#attachHash#"> 
</cfreport>

<cfpdf action="merge" destination="#mergedPdfFileName#" source="#pdfSources#" overwrite="true">

<cfscript>

    reader = CreateObject("java", "com.lowagie.text.pdf.PdfReader");
    reader.init(mergedPdfFileName);

    output = CreateObject("java", "java.io.FileOutputStream");
    output.init(finalPdfFileName);

    stamper = CreateObject("java", "com.lowagie.text.pdf.PdfStamper");
    stamper.init(reader, output);

    baseFont = CreateObject("java", "com.lowagie.text.pdf.BaseFont").createFont("Helvetica", "UTF-8", "True");

    over = CreateObject("java", "com.lowagie.text.pdf.PdfContentByte");

    pages = reader.getNumberOfPages();

    for ( i = 1; i LTE pages; i = i + 1 ) 
    {
        over = stamper.getOverContent(javacast("int",i));
        over.beginText();
        over.setFontAndSize(baseFont, 8);
        over.showTextAligned(2, "Page " & i & " of " & pages, 565, 34, 0);
        over.endText();
    }

    stamper.close();

</cfscript>

<cfpdf action="read" name="finalPdf" source="#finalPdfFileName#">

<cfcontent type="application/pdf" reset="true" variable="#toBinary(finalPdf)#">
