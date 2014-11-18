<cfcomponent output="false" alias="com.llsvc.domain.cfc.defaultexpencetypeVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="defaultexpencetypeid" type="numeric" default="0">
    <cfproperty name="itemname" type="string" default="">
    <cfproperty name="defaultrate" type="numeric" default="0">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.defaultexpencetypeid = 0;
        this.itemname = "";
        this.defaultrate = 0;
    </cfscript>

    <cffunction name="init" output="false" returntype="defaultexpencetypeVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getDefaultexpencetypeid" output="false" access="public" returntype="any">
        <cfreturn this.Defaultexpencetypeid>
    </cffunction>

    <cffunction name="setDefaultexpencetypeid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Defaultexpencetypeid = arguments.val>
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



    <cffunction name="save" output="false" access="public" returntype="defaultexpencetypeVO">
        <cfscript>
            if(this.getdefaultexpencetypeid() eq 0)
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
            select  defaultexpencetypeid, itemname, defaultrate
            from public.ll_defaultexpencetype
            where defaultexpencetypeid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setdefaultexpencetypeid(qRead.defaultexpencetypeid);
            this.setitemname(qRead.itemname);
            this.setdefaultrate(qRead.defaultrate);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="defaultexpencetypeVO">
        <cfset var qCreate="">

        <cfset var local1=this.getitemname()>
        <cfset var local2=this.getdefaultrate()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_defaultexpencetype(itemname, defaultrate)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select defaultexpencetypeid
                from public.ll_defaultexpencetype
                where itemname = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />
                  and defaultrate = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                order by defaultexpencetypeid desc
            </cfquery>
        </cftransaction>

        <cfset this.defaultexpencetypeid = qGetID.defaultexpencetypeid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="defaultexpencetypeVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_defaultexpencetype
            set itemname = <cfqueryparam value="#this.getitemname()#" cfsqltype="CF_SQL_VARCHAR" />,
                defaultrate = <cfqueryparam value="#this.getdefaultrate()#" cfsqltype="CF_SQL_NUMERIC" null="#iif((this.getdefaultrate() eq ""), de("yes"), de("no"))#" />
            where defaultexpencetypeid = <cfqueryparam value="#this.getdefaultexpencetypeid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_defaultexpencetype
            where defaultexpencetypeid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getdefaultexpencetypeid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>