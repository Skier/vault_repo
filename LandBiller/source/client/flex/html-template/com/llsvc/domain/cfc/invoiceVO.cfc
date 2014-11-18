<cfcomponent output="false" alias="com.llsvc.domain.cfc.invoiceVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="invoiceid" type="numeric" default="0">
    <cfproperty name="userid" type="numeric" default="0">
    <cfproperty name="clientid" type="numeric" default="0">
    <cfproperty name="invoicedate" type="date" default="">
    <cfproperty name="startdate" type="date" default="">
    <cfproperty name="enddate" type="date" default="">
    <cfproperty name="amount" type="numeric" default="0">
    <cfproperty name="adjustment" type="numeric" default="0">
    <cfproperty name="total" type="numeric" default="0">
    <cfproperty name="status" type="string" default="">
    <cfproperty name="invoiceno" type="string" default="">
    

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.invoiceid = 0;
        this.userid = 0;
        this.clientid = 0;
        this.invoicedate = "";
        this.startdate = "";
        this.enddate = "";
        this.amount = 0;
        this.adjustment = 0;
        this.total = 0;
        this.status = "";
        this.invoiceno = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="invoiceVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getInvoiceid" output="false" access="public" returntype="any">
        <cfreturn this.Invoiceid>
    </cffunction>

    <cffunction name="setInvoiceid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Invoiceid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getUserid" output="false" access="public" returntype="any">
        <cfreturn this.Userid>
    </cffunction>

    <cffunction name="setUserid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Userid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getClientid" output="false" access="public" returntype="any">
        <cfreturn this.Clientid>
    </cffunction>

    <cffunction name="setClientid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Clientid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getInvoicedate" output="false" access="public" returntype="any">
        <cfreturn this.Invoicedate>
    </cffunction>

    <cffunction name="setInvoicedate" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsDate(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Invoicedate = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid date"/>
        </cfif>
    </cffunction>

    <cffunction name="getStartdate" output="false" access="public" returntype="any">
        <cfreturn this.Startdate>
    </cffunction>

    <cffunction name="setStartdate" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsDate(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Startdate = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid date"/>
        </cfif>
    </cffunction>

    <cffunction name="getEnddate" output="false" access="public" returntype="any">
        <cfreturn this.Enddate>
    </cffunction>

    <cffunction name="setEnddate" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsDate(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Enddate = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid date"/>
        </cfif>
    </cffunction>

    <cffunction name="getAmount" output="false" access="public" returntype="any">
        <cfreturn this.Amount>
    </cffunction>

    <cffunction name="setAmount" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Amount = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getAdjustment" output="false" access="public" returntype="any">
        <cfreturn this.Adjustment>
    </cffunction>

    <cffunction name="setAdjustment" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Adjustment = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getTotal" output="false" access="public" returntype="any">
        <cfreturn this.Total>
    </cffunction>

    <cffunction name="setTotal" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Total = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getStatus" output="false" access="public" returntype="any">
        <cfreturn this.Status>
    </cffunction>

    <cffunction name="setStatus" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Status = arguments.val>
    </cffunction>

    <cffunction name="getInvoiceno" output="false" access="public" returntype="any">
        <cfreturn this.Invoiceno>
    </cffunction>

    <cffunction name="setInvoiceno" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Invoiceno = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="invoiceVO">
        <cfscript>
            if(this.getinvoiceid() eq 0)
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
            select  invoiceid, userid, clientid, invoicedate, startdate, enddate, amount, adjustment, total, status, invoiceno
                    
            from public.ll_invoice
            where invoiceid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setinvoiceid(qRead.invoiceid);
            this.setuserid(qRead.userid);
            this.setclientid(qRead.clientid);
            this.setinvoicedate(qRead.invoicedate);
            this.setstartdate(qRead.startdate);
            this.setenddate(qRead.enddate);
            this.setamount(qRead.amount);
            this.setadjustment(qRead.adjustment);
            this.settotal(qRead.total);
            this.setstatus(qRead.status);
            this.setinvoiceno(qRead.invoiceno);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="invoiceVO">
        <cfset var qCreate="">

        <cfset var local1=this.getuserid()>
        <cfset var local2=this.getclientid()>
        <cfset var local3=this.getinvoicedate()>
        <cfset var local4=this.getstartdate()>
        <cfset var local5=this.getenddate()>
        <cfset var local6=this.getamount()>
        <cfset var local7=this.getadjustment()>
        <cfset var local8=this.gettotal()>
        <cfset var local9=this.getstatus()>
        <cfset var local10=this.getinvoiceno()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_invoice(userid, clientid, invoicedate, startdate, enddate, amount, adjustment, total, status, invoiceno)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_DATE" null="#iif((local3 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_DATE" null="#iif((local4 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_DATE" null="#iif((local5 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local6#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local6 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local7#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local7 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local8#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local8 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local9#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local10#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
				select currval('ll_invoice_sqc') as invoiceid
            </cfquery>
        </cftransaction>

        <cfset this.invoiceid = qGetID.invoiceid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="invoiceVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_invoice
            set userid = <cfqueryparam value="#this.getuserid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getuserid() eq ""), de("yes"), de("no"))#" />,
                clientid = <cfqueryparam value="#this.getclientid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getclientid() eq ""), de("yes"), de("no"))#" />,
                invoicedate = <cfqueryparam value="#this.getinvoicedate()#" cfsqltype="CF_SQL_DATE" null="#iif((this.getinvoicedate() eq ""), de("yes"), de("no"))#" />,
                startdate = <cfqueryparam value="#this.getstartdate()#" cfsqltype="CF_SQL_DATE" null="#iif((this.getstartdate() eq ""), de("yes"), de("no"))#" />,
                enddate = <cfqueryparam value="#this.getenddate()#" cfsqltype="CF_SQL_DATE" null="#iif((this.getenddate() eq ""), de("yes"), de("no"))#" />,
                amount = <cfqueryparam value="#this.getamount()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.getamount() eq ""), de("yes"), de("no"))#" />,
                adjustment = <cfqueryparam value="#this.getadjustment()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.getadjustment() eq ""), de("yes"), de("no"))#" />,
                total = <cfqueryparam value="#this.gettotal()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.gettotal() eq ""), de("yes"), de("no"))#" />,
                status = <cfqueryparam value="#this.getstatus()#" cfsqltype="CF_SQL_VARCHAR" />,
                invoiceno = <cfqueryparam value="#this.getinvoiceno()#" cfsqltype="CF_SQL_VARCHAR" />
            where invoiceid = <cfqueryparam value="#this.getinvoiceid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_invoice
            where invoiceid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getinvoiceid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>