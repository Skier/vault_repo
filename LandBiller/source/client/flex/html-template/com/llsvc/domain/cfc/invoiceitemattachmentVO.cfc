<cfcomponent output="false" alias="com.llsvc.domain.cfc.invoiceitemattachmentVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="invoiceitemattachmentid" type="numeric" default="0">
    <cfproperty name="invoiceitemid" type="numeric" default="0">
    <cfproperty name="fileid" type="numeric" default="0">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.invoiceitemattachmentid = 0;
        this.invoiceitemid = 0;
        this.fileid = 0;
    </cfscript>

    <cffunction name="init" output="false" returntype="invoiceitemattachmentVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getInvoiceitemattachmentid" output="false" access="public" returntype="any">
        <cfreturn this.Invoiceitemattachmentid>
    </cffunction>

    <cffunction name="setInvoiceitemattachmentid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Invoiceitemattachmentid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getInvoiceitemid" output="false" access="public" returntype="any">
        <cfreturn this.Invoiceitemid>
    </cffunction>

    <cffunction name="setInvoiceitemid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Invoiceitemid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getFileid" output="false" access="public" returntype="any">
        <cfreturn this.Fileid>
    </cffunction>

    <cffunction name="setFileid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Fileid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="invoiceitemattachmentVO">
        <cfscript>
            if(this.getinvoiceitemattachmentid() eq 0)
            {
                return create();
            }else{
                return update();
            }
        </cfscript>
    </cffunction>



    <cffunction name="load" output="false" access="public" returntype="void">
        <cfargument name="id" required="true" >
        <cfset var qRead="">
        <cfquery name="qRead" datasource="LL_Expence">
            select  invoiceitemattachmentid, invoiceitemid, fileid
            from public.ll_invoiceitemattachment
            where invoiceitemattachmentid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setinvoiceitemattachmentid(qRead.invoiceitemattachmentid);
            this.setinvoiceitemid(qRead.invoiceitemid);
            this.setfileid(qRead.fileid);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="invoiceitemattachmentVO">
        <cfset var qCreate="">

        <cfset var local1=this.getinvoiceitemid()>
        <cfset var local2=this.getfileid()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_invoiceitemattachment(invoiceitemid, fileid)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select invoiceitemattachmentid
                from public.ll_invoiceitemattachment
                where invoiceitemid = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
                  and fileid = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                order by invoiceitemattachmentid desc
            </cfquery>
        </cftransaction>

        <cfset this.invoiceitemattachmentid = qGetID.invoiceitemattachmentid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="invoiceitemattachmentVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_invoiceitemattachment
            set invoiceitemid = <cfqueryparam value="#this.getinvoiceitemid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getinvoiceitemid() eq ""), de("yes"), de("no"))#" />,
                fileid = <cfqueryparam value="#this.getfileid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getfileid() eq ""), de("yes"), de("no"))#" />
            where invoiceitemattachmentid = <cfqueryparam value="#this.getinvoiceitemattachmentid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_invoiceitemattachment
            where invoiceitemattachmentid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getinvoiceitemattachmentid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>