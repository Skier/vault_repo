<cfcomponent output="false">

	<cffunction name="get" output="false" access="remote">
		<cfargument name="id" required="true" />
 		<cfreturn createObject("component", "loginVO").init(arguments.id)>
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



	<cffunction name="getAll" output="false" access="remote" returntype="com.llsvc.domain.cfc.loginVO[]">
		<cfset var qRead="">
		<cfset var obj="">
		<cfset var ret=arrayNew(1)>

		<cfquery name="qRead" datasource="LL_Expence">
			select loginid
			from public.ll_login
		</cfquery>

		<cfloop query="qRead">
		<cfscript>
			obj = createObject("component", "loginVO").init(qRead.loginid);
			ArrayAppend(ret, obj);
		</cfscript>
		</cfloop>
		<cfreturn ret>
	</cffunction>


	<cffunction name="checkUser" output="false" access="remote" returntype="any">
        <cfargument name="username" required="true" />
		<cfset var qRead="">
		<cfset var obj="">
		<cfset var ret=0>

		<cfquery name="qRead" datasource="LL_Expence">
			select loginid
			from public.ll_login
			where upper(public.ll_login.username) = upper(<cfqueryparam cfsqltype="CF_SQL_STRING" value="#arguments.username#" />)
		</cfquery>

		<cfloop query="qRead">
		<cfscript>
			ret = qRead.loginid;
		</cfscript>
		</cfloop>
		<cfreturn ret>
	</cffunction>


	<cffunction name="getAllAsQuery" output="false" access="remote" returntype="query">
		<cfargument name="fieldlist" default="*" hint="List of columns to be returned in the query.">

		<cfset var qRead="">

		<cfquery name="qRead" datasource="LL_Expence">
			select #arguments.fieldList#
			from public.ll_login
		</cfquery>

		<cfreturn qRead>
	</cffunction>




</cfcomponent>