<cfcomponent output="false" alias="com.llsvc.domain.cfc.companyVO">
	<!---
		 These are properties that are exposed by this CFC object.
		 These property definitions are used when calling this CFC as a web services, 
		 passed back to a flash movie, or when generating documentation

		 NOTE: these cfproperty tags do not set any default property values.
	--->
	<cfproperty name="companyid" type="numeric" default="0">
	<cfproperty name="userid" type="numeric" default="0">
	<cfproperty name="name" type="string" default="">
	<cfproperty name="description" type="string" default="">

	<cfscript>
		//Initialize the CFC with the default properties values.
		this.companyid = 0;
		this.userid = 0;
		this.name = "";
		this.description = "";
	</cfscript>

	<cffunction name="init" output="false" returntype="companyVO">
		<cfargument name="id" required="false">
		<cfscript>
			if( structKeyExists(arguments, "id") )
			{
				load(arguments.id);
			}
			return this;
		</cfscript>
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



	<cffunction name="save" output="false" access="public" returntype="companyVO">
		<cfscript>
			if(this.getcompanyid() eq 0)
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
			select 	companyid, userid, name, description
			from public.ll_company
			where companyid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
		</cfquery>

		<cfscript>
			this.setcompanyid(qRead.companyid);
			this.setuserid(qRead.userid);
			this.setname(qRead.name);
			this.setdescription(qRead.description);
		</cfscript>
	</cffunction>



	<cffunction name="create" output="false" access="private" returntype="companyVO">
		<cfset var qCreate="">

		<cfset var local1=this.getuserid()>
		<cfset var local2=this.getname()>
		<cfset var local3=this.getdescription()>

		<cftransaction isolation="read_committed">
			<cfquery name="qCreate" datasource="LL_Expence">
				insert into public.ll_company(userid, name, description)
				values (
					<cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
					<cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
					<cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
				)
			</cfquery>

			<!--- If your server has a better way to get the ID that is more reliable, use that instead --->
			<cfquery name="qGetID" datasource="LL_Expence">
				select companyid
				from public.ll_company
				where userid = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
				  and name = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
				  and description = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
				order by companyid desc
			</cfquery>
		</cftransaction>

		<cfset this.companyid = qGetID.companyid>

        <cfscript>
            return this;
        </cfscript>
	</cffunction>



	<cffunction name="update" output="false" access="private" returntype="companyVO">
		<cfset var qUpdate="">

		<cfquery name="qUpdate" datasource="LL_Expence" result="status">
			update public.ll_company
			set userid = <cfqueryparam value="#this.getuserid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getuserid() eq ""), de("yes"), de("no"))#" />,
				name = <cfqueryparam value="#this.getname()#" cfsqltype="CF_SQL_VARCHAR" />,
				description = <cfqueryparam value="#this.getdescription()#" cfsqltype="CF_SQL_VARCHAR" />
			where companyid = <cfqueryparam value="#this.getcompanyid()#" cfsqltype="CF_SQL_INTEGER">
		</cfquery>

        <cfscript>
            return this;
        </cfscript>
	</cffunction>



	<cffunction name="delete" output="false" access="public" returntype="void">
		<cfset var qDelete="">

		<cfquery name="qDelete" datasource="LL_Expence" result="status">
			delete
			from public.ll_company
			where companyid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getcompanyid()#" />
		</cfquery>
	</cffunction>


</cfcomponent>