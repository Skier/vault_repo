<cfcomponent output="false">

	<cffunction name="get" output="false" access="remote">
		<cfargument name="id" required="true" />
 		<cfreturn createObject("component", "countyVO").init(arguments.id)>
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



	<cffunction name="getAll" output="false" access="remote" returntype="com.llsvc.domain.cfc.countyVO[]">
		<cfset var qRead="">
		<cfset var obj="">
		<cfset var ret=arrayNew(1)>

		<cfquery name="qRead" datasource="LL_Expence">
			select countyid
			from public.ll_county
		</cfquery>

		<cfloop query="qRead">
		<cfscript>
			obj = createObject("component", "countyVO").init(qRead.countyid);
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
			from public.ll_county
		</cfquery>

		<cfreturn qRead>
	</cffunction>




</cfcomponent>