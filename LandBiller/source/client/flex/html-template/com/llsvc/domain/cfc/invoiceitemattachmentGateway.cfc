<cfcomponent output="false">

    <cffunction name="get" output="false" access="remote">
        <cfargument name="id" required="true" />
        <cfreturn createObject("component", "invoiceitemattachmentVO").init(arguments.id)>
    </cffunction>


    <cffunction name="save" output="false" access="remote">
        <cfargument name="obj" required="true" />
        <cfreturn obj.save() />
    </cffunction>


    <cffunction name="deleteEntity" output="false" access="remote">
        <cfargument name="id" required="true" />
        <cfset var obj = get(arguments.id)>
        <cfset obj.delete()>
    </cffunction>



    <cffunction name="getAll" output="false" access="remote" returntype="com.llsvc.domain.cfc.invoiceitemattachmentVO[]">
        <cfset var qRead="">
        <cfset var obj="">
        <cfset var ret=arrayNew(1)>

        <cfquery name="qRead" datasource="LL_Expence">
            select invoiceitemattachmentid
            from public.ll_invoiceitemattachment
        </cfquery>

        <cfloop query="qRead">
        <cfscript>
            obj = createObject("component", "invoiceitemattachmentVO").init(qRead.invoiceitemattachmentid);
            ArrayAppend(ret, obj);
        </cfscript>
        </cfloop>
        <cfreturn ret>
    </cffunction>



    <cffunction name="getByUserId" output="false" access="remote" returntype="com.llsvc.domain.cfc.invoiceitemattachmentVO[]">
        <cfargument name="id" required="true" >
        <cfset var qRead="">
        <cfset var obj="">
        <cfset var ret=arrayNew(1)>

        <cfquery name="qRead" datasource="LL_Expence">
            select invoiceitemattachmentid
            from public.ll_invoiceitemattachment, public.ll_invoiceitem, public.ll_project, public.ll_client
            where public.ll_invoiceitemattachment.invoiceitemid = public.ll_invoiceitem.invoiceitemid
            and public.ll_invoiceitem.projectid = public.ll_project.projectid
            and public.ll_project.clientid = public.ll_client.clientid
            and public.ll_client.userid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfloop query="qRead">
        <cfscript>
            obj = createObject("component", "invoiceitemattachmentVO").init(qRead.invoiceitemattachmentid);
            ArrayAppend(ret, obj);
        </cfscript>
        </cfloop>
        <cfreturn ret>
    </cffunction>



    <cffunction name="getAllAsQuery" output="false" access="remote" returntype="query">
        <cfargument name="fieldlist" default="*" hint="List of columns to be returned in the query.">

        <cfset var qRead="">

        <cfquery name="qRead" datasource="LL_Expence">
            select #arguments.fieldList#
            from public.ll_invoiceitemattachment
        </cfquery>

        <cfreturn qRead>
    </cffunction>




</cfcomponent>