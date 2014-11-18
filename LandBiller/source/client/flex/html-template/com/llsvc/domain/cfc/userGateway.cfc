<cfcomponent output="false">

    <cffunction name="get" output="false" access="remote">
        <cfargument name="id" required="true" />
        <cfreturn createObject("component", "userVO").init(arguments.id)>
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



    <cffunction name="getAll" output="false" access="remote" returntype="com.llsvc.domain.cfc.userVO[]">
        <cfset var qRead="">
        <cfset var obj="">
        <cfset var ret=arrayNew(1)>

        <cfquery name="qRead" datasource="LL_Expence">
            select userid
            from public.ll_user
        </cfquery>

        <cfloop query="qRead">
        <cfscript>
            obj = createObject("component", "userVO").init(qRead.userid);
            ArrayAppend(ret, obj);
        </cfscript>
        </cfloop>
        <cfreturn ret>
    </cffunction>



    <cffunction name="login" output="false" access="remote" returntype="com.llsvc.domain.cfc.userVO[]">
        <cfargument name="username" required="true" />
        <cfargument name="password" required="true" />
        <cfset var qRead="">
        <cfset var obj="">
        <cfset var ret=arrayNew(1)>

        <cfquery name="qRead" datasource="LL_Expence">
            select userid
            from public.ll_user, public.ll_login 
            where public.ll_user.loginid = public.ll_login.loginid
            and upper(public.ll_login.username) = upper(<cfqueryparam cfsqltype="CF_SQL_STRING" value="#arguments.username#" />)
            and public.ll_login.password = <cfqueryparam cfsqltype="CF_SQL_STRING" value="#arguments.password#" />
        </cfquery>

        <cfloop query="qRead">
        <cfscript>
            obj = createObject("component", "userVO").init(qRead.userid);
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
            from public.ll_user
        </cfquery>

        <cfreturn qRead>
    </cffunction>




</cfcomponent>