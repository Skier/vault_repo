<cfcomponent output="false" alias="com.llsvc.domain.cfc.userVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="userid" type="numeric" default="0">
    <cfproperty name="loginid" type="numeric" default="0">
    <cfproperty name="logourl" type="string" default="">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.userid = 0;
        this.loginid = 0;
        this.logourl = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="userVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
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

    <cffunction name="getLoginid" output="false" access="public" returntype="any">
        <cfreturn this.Loginid>
    </cffunction>

    <cffunction name="setLoginid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Loginid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getlogourl" output="false" access="public" returntype="any">
        <cfreturn this.logourl>
    </cffunction>

    <cffunction name="setlogourl" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.logourl = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="userVO">
        <cfscript>
            if(this.getuserid() eq 0)
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
            select  userid, loginid, logourl
            from public.ll_user
            where userid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setuserid(qRead.userid);
            this.setloginid(qRead.loginid);
            this.setlogourl(qRead.logourl);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="userVO">
        <cfset var qCreate="">

        <cfset var local1=this.getloginid()>
        <cfset var local2=this.getlogourl()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_user(loginid, logourl)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select userid
                from public.ll_user
                where loginid = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
                  and logourl = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                order by userid desc
            </cfquery>
        </cftransaction>

        <cfset this.userid = qGetID.userid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="userVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_user
            set loginid = <cfqueryparam value="#this.getloginid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getloginid() eq ""), de("yes"), de("no"))#" />,
                logourl = <cfqueryparam value="#this.getlogourl()#" cfsqltype="CF_SQL_VARCHAR" />
            where userid = <cfqueryparam value="#this.getuserid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_user
            where userid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getuserid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>