<cfquery name="MyQuery" datasource="LL_Expence">
    SELECT    ll_invoice.invoiceno, ll_invoiceitem.itemdate, ll_invoiceitem.quantity, ll_invoiceitem.total, ll_invoice.invoicedate, ll_project.projectname, ll_expencetype.itemname AS expensename, ll_company.name AS companyname, ll_person.firstname, ll_person.middlename, ll_person.lastname, ll_person.phone, ll_person.phonealt, ll_address.address1, ll_address.address2, ll_address.city, ll_state.name AS statename, ll_address.zip
    FROM      public.ll_invoice, public.ll_invoiceitem, public.ll_client, public.ll_company, public.ll_person, public.ll_address, public.ll_state, public.ll_expencetype, public.ll_project 
    WHERE     public.ll_invoice.invoiceid = public.ll_invoiceitem.invoiceid
      AND     public.ll_invoice.invoiceid = <cfqueryparam value="#URL.invoiceId#" cfsqltype="CF_SQL_INTEGER">
      AND     public.ll_invoice.clientid = public.ll_client.clientid
      AND     public.ll_client.companyid = public.ll_company.companyid
      AND     public.ll_client.personid = public.ll_person.personid
      AND     public.ll_person.addressid = public.ll_address.addressid
      AND     public.ll_address.stateid = public.ll_state.stateid
      AND     public.ll_invoiceitem.expencetypeid = public.ll_expencetype.expencetypeid
      AND     public.ll_invoiceitem.projectid = public.ll_project.projectid
      AND     public.ll_client.clientid = public.ll_project.clientid
    ORDER BY ll_invoiceitem.itemdate
</cfquery>

<cfreport template="Invoice.cfr" format="excel" query="MyQuery">
    <cfreportparam name="invoiceid" value="#URL.invoiceId#"> <!-- Integer -->
</cfreport>