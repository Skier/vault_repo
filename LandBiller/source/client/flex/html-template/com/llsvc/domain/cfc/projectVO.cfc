<cfcomponent output="false" alias="com.llsvc.domain.cfc.projectVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="projectid" type="numeric" default="0">
    <cfproperty name="clientid" type="numeric" default="0">
    <cfproperty name="projectname" type="string" default="">
    <cfproperty name="afe" type="string" default="">
    <cfproperty name="description" type="string" default="">
    <cfproperty name="status" type="string" default="">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.projectid = 0;
        this.clientid = 0;
        this.projectname = "";
        this.afe = "";
        this.description = "";
        this.status = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="projectVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getProjectid" output="false" access="public" returntype="any">
        <cfreturn this.Projectid>
    </cffunction>

    <cffunction name="setProjectid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Projectid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getClientid" output="false" access="public" returntype="any">
        <cfreturn this.Clientid>
    </cffunction>

    <cffunction name="setClientid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Clientid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getProjectname" output="false" access="public" returntype="any">
        <cfreturn this.Projectname>
    </cffunction>

    <cffunction name="setProjectname" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Projectname = arguments.val>
    </cffunction>

    <cffunction name="getAfe" output="false" access="public" returntype="any">
        <cfreturn this.Afe>
    </cffunction>

    <cffunction name="setAfe" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Afe = arguments.val>
    </cffunction>

    <cffunction name="getDescription" output="false" access="public" returntype="any">
        <cfreturn this.Description>
    </cffunction>

    <cffunction name="setDescription" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Description = arguments.val>
    </cffunction>

    <cffunction name="getStatus" output="false" access="public" returntype="any">
        <cfreturn this.Status>
    </cffunction>

    <cffunction name="setStatus" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Status = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="projectVO">
        <cfscript>
            if(this.getprojectid() eq 0)
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
            select  projectid, clientid, projectname, afe, description, status
                    
            from public.ll_project
            where projectid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setprojectid(qRead.projectid);
            this.setclientid(qRead.clientid);
            this.setprojectname(qRead.projectname);
            this.setafe(qRead.afe);
            this.setdescription(qRead.description);
            this.setstatus(qRead.status);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="projectVO">
        <cfset var qCreate="">

        <cfset var local1=this.getclientid()>
        <cfset var local2=this.getprojectname()>
        <cfset var local3=this.getafe()>
        <cfset var local4=this.getdescription()>
        <cfset var local5=this.getstatus()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_project(clientid, projectname, afe, description, status)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select projectid
                from public.ll_project
                where clientid = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
                  and projectname = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                  and afe = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                  and description = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />
                  and status = <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                order by projectid desc
            </cfquery>
        </cftransaction>

        <cfset this.projectid = qGetID.projectid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="projectVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_project
            set clientid = <cfqueryparam value="#this.getclientid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getclientid() eq ""), de("yes"), de("no"))#" />,
                projectname = <cfqueryparam value="#this.getprojectname()#" cfsqltype="CF_SQL_VARCHAR" />,
                afe = <cfqueryparam value="#this.getafe()#" cfsqltype="CF_SQL_VARCHAR" />,
                description = <cfqueryparam value="#this.getdescription()#" cfsqltype="CF_SQL_VARCHAR" />,
                status = <cfqueryparam value="#this.getstatus()#" cfsqltype="CF_SQL_VARCHAR" />
            where projectid = <cfqueryparam value="#this.getprojectid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_project
            where projectid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getprojectid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>