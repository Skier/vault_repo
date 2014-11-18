<cfcomponent output="false" alias="com.llsvc.domain.cfc.expencetypeVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="expencetypeid" type="numeric" default="0">
    <cfproperty name="basedon" type="numeric" default="0">
    <cfproperty name="userid" type="numeric" default="0">
    <cfproperty name="itemname" type="string" default="">
    <cfproperty name="defaultrate" type="numeric" default="0">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.expencetypeid = 0;
        this.basedon = 0;
        this.userid = 0;
        this.itemname = "";
        this.defaultrate = 0;
    </cfscript>

    <cffunction name="init" output="false" returntype="expencetypeVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getExpencetypeid" output="false" access="public" returntype="any">
        <cfreturn this.Expencetypeid>
    </cffunction>

    <cffunction name="setExpencetypeid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Expencetypeid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getBasedon" output="false" access="public" returntype="any">
        <cfreturn this.Basedon>
    </cffunction>

    <cffunction name="setBasedon" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Basedon = arguments.val>
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

    <cffunction name="getItemname" output="false" access="public" returntype="any">
        <cfreturn this.Itemname>
    </cffunction>

    <cffunction name="setItemname" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Itemname = arguments.val>
    </cffunction>

    <cffunction name="getDefaultrate" output="false" access="public" returntype="any">
        <cfreturn this.Defaultrate>
    </cffunction>

    <cffunction name="setDefaultrate" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Defaultrate = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="expencetypeVO">
        <cfscript>
            if(this.getexpencetypeid() eq 0)
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
            select  expencetypeid, basedon, userid, itemname, defaultrate
            from public.ll_expencetype
            where expencetypeid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setexpencetypeid(qRead.expencetypeid);
            this.setbasedon(qRead.basedon);
            this.setuserid(qRead.userid);
            this.setitemname(qRead.itemname);
            this.setdefaultrate(qRead.defaultrate);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="expencetypeVO">
        <cfset var qCreate="">

        <cfset var local1=this.getbasedon()>
        <cfset var local2=this.getuserid()>
        <cfset var local3=this.getitemname()>
        <cfset var local4=this.getdefaultrate()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_expencetype(basedon, userid, itemname, defaultrate)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local4 eq ""), de("yes"), de("no"))#" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select expencetypeid
                from public.ll_expencetype
                where basedon = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
                  and userid = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                  and itemname = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                  and defaultrate = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local4 eq ""), de("yes"), de("no"))#" />
                order by expencetypeid desc
            </cfquery>
        </cftransaction>

        <cfset this.expencetypeid = qGetID.expencetypeid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="expencetypeVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_expencetype
            set basedon = <cfqueryparam value="#this.getbasedon()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getbasedon() eq ""), de("yes"), de("no"))#" />,
                userid = <cfqueryparam value="#this.getuserid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getuserid() eq ""), de("yes"), de("no"))#" />,
                itemname = <cfqueryparam value="#this.getitemname()#" cfsqltype="CF_SQL_VARCHAR" />,
                defaultrate = <cfqueryparam value="#this.getdefaultrate()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.getdefaultrate() eq ""), de("yes"), de("no"))#" />
            where expencetypeid = <cfqueryparam value="#this.getexpencetypeid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_expencetype
            where expencetypeid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getexpencetypeid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>