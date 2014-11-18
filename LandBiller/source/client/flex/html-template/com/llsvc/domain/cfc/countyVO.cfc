<cfcomponent output="false" alias="com.llsvc.domain.cfc.countyVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="countyid" type="numeric" default="0">
    <cfproperty name="name" type="string" default="">
    <cfproperty name="stateid" type="numeric" default="0">
    <cfproperty name="statefips" type="string" default="">
    <cfproperty name="countyfips" type="string" default="">
    <cfproperty name="fullfips" type="string" default="">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.countyid = 0;
        this.name = "";
        this.stateid = 0;
        this.statefips = "";
        this.countyfips = "";
        this.fullfips = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="countyVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getCountyid" output="false" access="public" returntype="any">
        <cfreturn this.Countyid>
    </cffunction>

    <cffunction name="setCountyid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Countyid = arguments.val>
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

    <cffunction name="getStatefips" output="false" access="public" returntype="any">
        <cfreturn this.Statefips>
    </cffunction>

    <cffunction name="setStatefips" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Statefips = arguments.val>
    </cffunction>

    <cffunction name="getCountyfips" output="false" access="public" returntype="any">
        <cfreturn this.Countyfips>
    </cffunction>

    <cffunction name="setCountyfips" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Countyfips = arguments.val>
    </cffunction>

    <cffunction name="getFullfips" output="false" access="public" returntype="any">
        <cfreturn this.Fullfips>
    </cffunction>

    <cffunction name="setFullfips" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Fullfips = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="countyVO">
        <cfscript>
            if(this.getcountyid() eq 0)
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
            select  countyid, name, stateid, statefips, countyfips, fullfips
                    
            from public.ll_county
            where countyid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setcountyid(qRead.countyid);
            this.setname(qRead.name);
            this.setstateid(qRead.stateid);
            this.setstatefips(qRead.statefips);
            this.setcountyfips(qRead.countyfips);
            this.setfullfips(qRead.fullfips);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="countyVO">
        <cfset var qCreate="">

        <cfset var local1=this.getname()>
        <cfset var local2=this.getstateid()>
        <cfset var local3=this.getstatefips()>
        <cfset var local4=this.getcountyfips()>
        <cfset var local5=this.getfullfips()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_county(name, stateid, statefips, countyfips, fullfips)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select countyid
                from public.ll_county
                where name = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />
                  and stateid = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                  and statefips = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                  and countyfips = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />
                  and fullfips = <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                order by countyid desc
            </cfquery>
        </cftransaction>

        <cfset this.countyid = qGetID.countyid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="countyVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_county
            set name = <cfqueryparam value="#this.getname()#" cfsqltype="CF_SQL_VARCHAR" />,
                stateid = <cfqueryparam value="#this.getstateid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getstateid() eq ""), de("yes"), de("no"))#" />,
                statefips = <cfqueryparam value="#this.getstatefips()#" cfsqltype="CF_SQL_VARCHAR" />,
                countyfips = <cfqueryparam value="#this.getcountyfips()#" cfsqltype="CF_SQL_VARCHAR" />,
                fullfips = <cfqueryparam value="#this.getfullfips()#" cfsqltype="CF_SQL_VARCHAR" />
            where countyid = <cfqueryparam value="#this.getcountyid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_county
            where countyid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getcountyid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>