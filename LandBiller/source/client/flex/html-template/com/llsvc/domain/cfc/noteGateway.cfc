<cfcomponent output="false">

	<cffunction name="get" output="false" access="remote">
		<cfargument name="id" required="true" />
 		<cfreturn createObject("component", "noteVO").init(arguments.id)>
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



	<cffunction name="getAll" output="false" access="remote" returntype="com.llsvc.domain.cfc.noteVO[]">
		<cfset var qRead="">
		<cfset var obj="">
		<cfset var ret=arrayNew(1)>

		<cfquery name="qRead" datasource="LL_Expence">
			select noteid
			from public.ll_note
		</cfquery>

		<cfloop query="qRead">
		<cfscript>
			obj = createObject("component", "noteVO").init(qRead.noteid);
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
			from public.ll_note
		</cfquery>

		<cfreturn qRead>
	</cffunction>

	<cffunction name="getByUserId" output="false" access="remote" returntype="com.llsvc.domain.cfc.noteVO[]">
		<cfargument name="id" required="true" >
		<cfset var qRead="">
		<cfset var obj="">
		<cfset var ret=arrayNew(1)>

		<cfquery name="qRead" datasource="LL_Expence">
			select noteid
			from public.ll_note, public.ll_invoice
			where public.ll_note.invoiceid = public.ll_invoice.invoiceid
			and public.ll_invoice.userid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
		</cfquery>

		<cfloop query="qRead">
		<cfscript>
			obj = createObject("component", "noteVO").init(qRead.noteid);
			ArrayAppend(ret, obj);
		</cfscript>
		</cfloop>
		<cfreturn ret>
	</cffunction>




</cfcomponent>