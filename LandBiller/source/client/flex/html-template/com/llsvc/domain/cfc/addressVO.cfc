<cfcomponent output="false" alias="com.llsvc.domain.cfc.addressVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="addressid" type="numeric" default="0">
    <cfproperty name="address1" type="string" default="">
    <cfproperty name="address2" type="string" default="">
    <cfproperty name="city" type="string" default="">
    <cfproperty name="stateid" type="numeric" default="0">
    <cfproperty name="zip" type="string" default="">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.addressid = 0;
        this.address1 = "";
        this.address2 = "";
        this.city = "";
        this.stateid = 0;
        this.zip = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="addressVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getAddressid" output="false" access="public" returntype="any">
        <cfreturn this.Addressid>
    </cffunction>

    <cffunction name="setAddressid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Addressid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getAddress1" output="false" access="public" returntype="any">
        <cfreturn this.Address1>
    </cffunction>

    <cffunction name="setAddress1" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Address1 = arguments.val>
    </cffunction>

    <cffunction name="getAddress2" output="false" access="public" returntype="any">
        <cfreturn this.Address2>
    </cffunction>

    <cffunction name="setAddress2" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Address2 = arguments.val>
    </cffunction>

    <cffunction name="getCity" output="false" access="public" returntype="any">
        <cfreturn this.City>
    </cffunction>

    <cffunction name="setCity" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.City = arguments.val>
    </cffunction>

    <cffunction name="getStateid" output="false" access="public" returntype="any">
        <cfreturn this.Stateid>
    </cffunction>

    <cffunction name="setStateid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Stateid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getZip" output="false" access="public" returntype="any">
        <cfreturn this.Zip>
    </cffunction>

    <cffunction name="setZip" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Zip = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="addressVO">
        <cfscript>
            if(this.getaddressid() eq 0)
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
            select  addressid, address1, address2, city, stateid, zip
                    
            from public.ll_address
            where addressid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setaddressid(qRead.addressid);
            this.setaddress1(qRead.address1);
            this.setaddress2(qRead.address2);
            this.setcity(qRead.city);
            this.setstateid(qRead.stateid);
            this.setzip(qRead.zip);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="addressVO">
        <cfset var qCreate="">

        <cfset var local1=this.getaddress1()>
        <cfset var local2=this.getaddress2()>
        <cfset var local3=this.getcity()>
        <cfset var local4=this.getstateid()>
        <cfset var local5=this.getzip()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_address(address1, address2, city, stateid, zip)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_INTEGER" null="#iif((local4 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select addressid
                from public.ll_address
                where address1 = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />
                  and address2 = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                  and city = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                  and stateid = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_INTEGER" null="#iif((local4 eq ""), de("yes"), de("no"))#" />
                  and zip = <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                order by addressid desc
            </cfquery>
        </cftransaction>

        <cfset this.addressid = qGetID.addressid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="addressVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_address
            set address1 = <cfqueryparam value="#this.getaddress1()#" cfsqltype="CF_SQL_VARCHAR" />,
                address2 = <cfqueryparam value="#this.getaddress2()#" cfsqltype="CF_SQL_VARCHAR" />,
                city = <cfqueryparam value="#this.getcity()#" cfsqltype="CF_SQL_VARCHAR" />,
                stateid = <cfqueryparam value="#this.getstateid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getstateid() eq ""), de("yes"), de("no"))#" />,
                zip = <cfqueryparam value="#this.getzip()#" cfsqltype="CF_SQL_VARCHAR" />
            where addressid = <cfqueryparam value="#this.getaddressid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_address
            where addressid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getaddressid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>