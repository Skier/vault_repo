<cfcomponent output="false" alias="com.llsvc.domain.cfc.stateVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="stateid" type="numeric" default="0">
    <cfproperty name="name" type="string" default="">
    <cfproperty name="statefips" type="string" default="">
    <cfproperty name="stateabbr" type="string" default="">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.stateid = 0;
        this.name = "";
        this.statefips = "";
        this.stateabbr = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="stateVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
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

    <cffunction name="getName" output="false" access="public" returntype="any">
        <cfreturn this.Name>
    </cffunction>

    <cffunction name="setName" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Name = arguments.val>
    </cffunction>

    <cffunction name="getStatefips" output="false" access="public" returntype="any">
        <cfreturn this.Statefips>
    </cffunction>

    <cffunction name="setStatefips" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Statefips = arguments.val>
    </cffunction>

    <cffunction name="getStateabbr" output="false" access="public" returntype="any">
        <cfreturn this.Stateabbr>
    </cffunction>

    <cffunction name="setStateabbr" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Stateabbr = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="stateVO">
        <cfscript>
            if(this.getstateid() eq 0)
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
            select  stateid, name, statefips, stateabbr
            from public.ll_state
            where stateid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setstateid(qRead.stateid);
            this.setname(qRead.name);
            this.setstatefips(qRead.statefips);
            this.setstateabbr(qRead.stateabbr);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="stateVO">
        <cfset var qCreate="">

        <cfset var local1=this.getname()>
        <cfset var local2=this.getstatefips()>
        <cfset var local3=this.getstateabbr()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_state(name, statefips, stateabbr)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select stateid
                from public.ll_state
                where name = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />
                  and statefips = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                  and stateabbr = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                order by stateid desc
            </cfquery>
        </cftransaction>

        <cfset this.stateid = qGetID.stateid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="stateVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_state
            set name = <cfqueryparam value="#this.getname()#" cfsqltype="CF_SQL_VARCHAR" />,
                statefips = <cfqueryparam value="#this.getstatefips()#" cfsqltype="CF_SQL_VARCHAR" />,
                stateabbr = <cfqueryparam value="#this.getstateabbr()#" cfsqltype="CF_SQL_VARCHAR" />
            where stateid = <cfqueryparam value="#this.getstateid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_state
            where stateid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getstateid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>