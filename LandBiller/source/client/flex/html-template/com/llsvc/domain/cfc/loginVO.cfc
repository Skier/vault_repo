<cfcomponent output="false" alias="com.llsvc.domain.cfc.loginVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="loginid" type="numeric" default="0">
    <cfproperty name="username" type="string" default="">
    <cfproperty name="password" type="string" default="">
    <cfproperty name="email" type="string" default="">
    <cfproperty name="personid" type="numeric" default="0">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.loginid = 0;
        this.username = "";
        this.password = "";
        this.email = "";
        this.personid = 0;
    </cfscript>

    <cffunction name="init" output="false" returntype="loginVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
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

    <cffunction name="getUsername" output="false" access="public" returntype="any">
        <cfreturn this.Username>
    </cffunction>

    <cffunction name="setUsername" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Username = arguments.val>
    </cffunction>

    <cffunction name="getPassword" output="false" access="public" returntype="any">
        <cfreturn this.Password>
    </cffunction>

    <cffunction name="setPassword" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Password = arguments.val>
    </cffunction>

    <cffunction name="getEmail" output="false" access="public" returntype="any">
        <cfreturn this.Email>
    </cffunction>

    <cffunction name="setEmail" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Email = arguments.val>
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



    <cffunction name="save" output="false" access="public" returntype="loginVO">
        <cfscript>
            if(this.getloginid() eq 0)
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
            select  loginid, username, password, email, personid
            from public.ll_login
            where loginid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setloginid(qRead.loginid);
            this.setusername(qRead.username);
            this.setpassword(qRead.password);
            this.setemail(qRead.email);
            this.setpersonid(qRead.personid);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="loginVO">
        <cfset var qCreate="">

        <cfset var local1=this.getusername()>
        <cfset var local2=this.getpassword()>
        <cfset var local3=this.getemail()>
        <cfset var local4=this.getpersonid()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_login(username, password, email, personid)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_INTEGER" null="#iif((local4 eq ""), de("yes"), de("no"))#" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select loginid
                from public.ll_login
                where username = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />
                  and password = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                  and email = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                  and personid = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_INTEGER" null="#iif((local4 eq ""), de("yes"), de("no"))#" />
                order by loginid desc
            </cfquery>
        </cftransaction>

        <cfset this.loginid = qGetID.loginid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="loginVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_login
            set username = <cfqueryparam value="#this.getusername()#" cfsqltype="CF_SQL_VARCHAR" />,
                password = <cfqueryparam value="#this.getpassword()#" cfsqltype="CF_SQL_VARCHAR" />,
                email = <cfqueryparam value="#this.getemail()#" cfsqltype="CF_SQL_VARCHAR" />,
                personid = <cfqueryparam value="#this.getpersonid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getpersonid() eq ""), de("yes"), de("no"))#" />
            where loginid = <cfqueryparam value="#this.getloginid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_login
            where loginid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getloginid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>