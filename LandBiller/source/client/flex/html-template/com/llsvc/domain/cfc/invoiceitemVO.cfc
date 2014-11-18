<cfcomponent output="false" alias="com.llsvc.domain.cfc.invoiceitemVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="invoiceitemid" type="numeric" default="0">
    <cfproperty name="invoiceid" type="numeric" default="0">
    <cfproperty name="projectid" type="numeric" default="0">
    <cfproperty name="expencetypeid" type="numeric" default="0">
    <cfproperty name="itemdate" type="date" default="">
    <cfproperty name="quantity" type="numeric" default="0">
    <cfproperty name="rate" type="numeric" default="0">
    <cfproperty name="total" type="numeric" default="0">
    <cfproperty name="status" type="numeric" default="0">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.invoiceitemid = 0;
        this.invoiceid = 0;
        this.projectid = 0;
        this.expencetypeid = 0;
        this.itemdate = "";
        this.quantity = 0;
        this.rate = 0;
        this.total = 0;
        this.status = 0;
    </cfscript>

    <cffunction name="init" output="false" returntype="invoiceitemVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
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

    <cffunction name="getProjectid" output="false" access="public" returntype="any">
        <cfreturn this.Projectid>
    </cffunction>

    <cffunction name="setProjectid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Projectid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getExpencetypeid" output="false" access="public" returntype="any">
        <cfreturn this.Expencetypeid>
    </cffunction>

    <cffunction name="setExpencetypeid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Expencetypeid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getItemdate" output="false" access="public" returntype="any">
        <cfreturn this.Itemdate>
    </cffunction>

    <cffunction name="setItemdate" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsDate(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Itemdate = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid date"/>
        </cfif>
    </cffunction>

    <cffunction name="getQuantity" output="false" access="public" returntype="any">
        <cfreturn this.Quantity>
    </cffunction>

    <cffunction name="setQuantity" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Quantity = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getRate" output="false" access="public" returntype="any">
        <cfreturn this.Rate>
    </cffunction>

    <cffunction name="setRate" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Rate = arguments.val>
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
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Status = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="invoiceitemVO">
        <cfscript>
            if(this.getinvoiceitemid() eq 0)
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
            select  invoiceitemid, invoiceid, projectid, expencetypeid, itemdate, quantity, 
                    rate, total, status
            from public.ll_invoiceitem
            where invoiceitemid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setinvoiceitemid(qRead.invoiceitemid);
            this.setinvoiceid(qRead.invoiceid);
            this.setprojectid(qRead.projectid);
            this.setexpencetypeid(qRead.expencetypeid);
            this.setitemdate(qRead.itemdate);
            this.setquantity(qRead.quantity);
            this.setrate(qRead.rate);
            this.settotal(qRead.total);
            this.setstatus(qRead.status);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="invoiceitemVO">
        <cfset var qCreate="">

        <cfset var local1=this.getinvoiceid()>
        <cfset var local2=this.getprojectid()>
        <cfset var local3=this.getexpencetypeid()>
        <cfset var local4=this.getitemdate()>
        <cfset var local5=this.getquantity()>
        <cfset var local6=this.getrate()>
        <cfset var local7=this.gettotal()>
        <cfset var local8=this.getstatus()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_invoiceitem(invoiceid, projectid, expencetypeid, itemdate, quantity, rate, total, status)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_INTEGER" null="#iif((local3 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_DATE" null="#iif((local4 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local5 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local6#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local6 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local7#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local7 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local8#" cfsqltype="CF_SQL_INTEGER" null="#iif((local8 eq ""), de("yes"), de("no"))#" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select invoiceitemid
                from public.ll_invoiceitem
                where invoiceid = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
                  and projectid = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                  and expencetypeid = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_INTEGER" null="#iif((local3 eq ""), de("yes"), de("no"))#" />
                  and itemdate = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_DATE" null="#iif((local4 eq ""), de("yes"), de("no"))#" />
                  and quantity = <cfqueryparam value="#local5#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local5 eq ""), de("yes"), de("no"))#" />
                  and rate = <cfqueryparam value="#local6#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local6 eq ""), de("yes"), de("no"))#" />
                  and total = <cfqueryparam value="#local7#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local7 eq ""), de("yes"), de("no"))#" />
                  and status = <cfqueryparam value="#local8#" cfsqltype="CF_SQL_INTEGER" null="#iif((local8 eq ""), de("yes"), de("no"))#" />
                order by invoiceitemid desc
            </cfquery>
        </cftransaction>

        <cfset this.invoiceitemid = qGetID.invoiceitemid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="invoiceitemVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_invoiceitem
            set invoiceid = <cfqueryparam value="#this.getinvoiceid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getinvoiceid() eq ""), de("yes"), de("no"))#" />,
                projectid = <cfqueryparam value="#this.getprojectid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getprojectid() eq ""), de("yes"), de("no"))#" />,
                expencetypeid = <cfqueryparam value="#this.getexpencetypeid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getexpencetypeid() eq ""), de("yes"), de("no"))#" />,
                itemdate = <cfqueryparam value="#this.getitemdate()#" cfsqltype="CF_SQL_DATE" null="#iif((this.getitemdate() eq ""), de("yes"), de("no"))#" />,
                quantity = <cfqueryparam value="#this.getquantity()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.getquantity() eq ""), de("yes"), de("no"))#" />,
                rate = <cfqueryparam value="#this.getrate()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.getrate() eq ""), de("yes"), de("no"))#" />,
                total = <cfqueryparam value="#this.gettotal()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.gettotal() eq ""), de("yes"), de("no"))#" />,
                status = <cfqueryparam value="#this.getstatus()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getstatus() eq ""), de("yes"), de("no"))#" />
            where invoiceitemid = <cfqueryparam value="#this.getinvoiceitemid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_invoiceitem
            where invoiceitemid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getinvoiceitemid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>