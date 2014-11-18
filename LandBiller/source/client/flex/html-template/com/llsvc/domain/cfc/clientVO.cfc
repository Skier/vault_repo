<cfcomponent output="false" alias="com.llsvc.domain.cfc.clientVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="clientid" type="numeric" default="0">
    <cfproperty name="userid" type="numeric" default="0">
    <cfproperty name="personid" type="numeric" default="0">
    <cfproperty name="name" type="string" default="">
    <cfproperty name="description" type="string" default="">
    <cfproperty name="companyid" type="numeric" default="0">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.clientid = 0;
        this.userid = 0;
        this.personid = 0;
        this.name = "";
        this.description = "";
        this.companyid = 0;
    </cfscript>

    <cffunction name="init" output="false" returntype="clientVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
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

    <cffunction name="getPersonid" output="false" access="public" returntype="any">
        <cfreturn this.Personid>
    </cffunction>

    <cffunction name="setPersonid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Personid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getName" output="false" access="public" returntype="any">
        <cfreturn this.Name>
    </cffunction>

    <cffunction name="setName" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Name = arguments.val>
    </cffunction>

    <cffunction name="getDescription" output="false" access="public" returntype="any">
        <cfreturn this.Description>
    </cffunction>

    <cffunction name="setDescription" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Description = arguments.val>
    </cffunction>

    <cffunction name="getCompanyid" output="false" access="public" returntype="any">
        <cfreturn this.Companyid>
    </cffunction>

    <cffunction name="setCompanyid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Companyid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="clientVO">
        <cfscript>
            if(this.getclientid() eq 0)
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
            select  clientid, userid, personid, name, description, companyid
            from public.ll_client
            where clientid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setclientid(qRead.clientid);
            this.setuserid(qRead.userid);
            this.setpersonid(qRead.personid);
            this.setname(qRead.name);
            this.setdescription(qRead.description);
            this.setcompanyid(qRead.companyid);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="clientVO">
        <cfset var qCreate="">

        <cfset var local1=this.getuserid()>
        <cfset var local2=this.getpersonid()>
        <cfset var local3=this.getname()>
        <cfset var local4=this.getdescription()>
        <cfset var local5=this.getcompanyid()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_client(userid, personid, name, description, companyid)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_INTEGER" null="#iif((local5 eq ""), de("yes"), de("no"))#" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select clientid
                from public.ll_client
                where userid = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
                  and personid = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                  and name = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                  and description = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />
	              and companyid = <cfqueryparam value="#local5#" cfsqltype="CF_SQL_INTEGER" null="#iif((local5 eq ""), de("yes"), de("no"))#" />
                order by clientid desc
            </cfquery>
        </cftransaction>

        <cfset this.clientid = qGetID.clientid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="clientVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_client
            set userid = <cfqueryparam value="#this.getuserid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getuserid() eq ""), de("yes"), de("no"))#" />,
                personid = <cfqueryparam value="#this.getpersonid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getpersonid() eq ""), de("yes"), de("no"))#" />,
                name = <cfqueryparam value="#this.getname()#" cfsqltype="CF_SQL_VARCHAR" />,
                description = <cfqueryparam value="#this.getdescription()#" cfsqltype="CF_SQL_VARCHAR" />,
            	companyid = <cfqueryparam value="#this.getcompanyid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getcompanyid() eq ""), de("yes"), de("no"))#" />
            where clientid = <cfqueryparam value="#this.getclientid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_client
            where clientid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getclientid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>