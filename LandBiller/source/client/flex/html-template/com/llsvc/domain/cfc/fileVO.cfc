<cfcomponent output="false" alias="com.llsvc.domain.cfc.fileVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="fileid" type="numeric" default="0">
    <cfproperty name="origfilename" type="string" default="">
    <cfproperty name="storagekey" type="string" default="">
    <cfproperty name="userid" type="numeric" default="0">
    <cfproperty name="note" type="string" default="">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.fileid = 0;
        this.origfilename = "";
        this.storagekey = "";
        this.userid = 0;
        this.note = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="fileVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getFileid" output="false" access="public" returntype="any">
        <cfreturn this.Fileid>
    </cffunction>

    <cffunction name="setFileid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Fileid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getOrigfilename" output="false" access="public" returntype="any">
        <cfreturn this.Origfilename>
    </cffunction>

    <cffunction name="setOrigfilename" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Origfilename = arguments.val>
    </cffunction>

    <cffunction name="getStoragekey" output="false" access="public" returntype="any">
        <cfreturn this.Storagekey>
    </cffunction>

    <cffunction name="setStoragekey" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Storagekey = arguments.val>
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

    <cffunction name="getNote" output="false" access="public" returntype="any">
        <cfreturn this.Note>
    </cffunction>

    <cffunction name="setNote" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Note = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="fileVO">
        <cfscript>
            if(this.getfileid() eq 0)
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
            select  fileid, origfilename, storagekey, userid, note
            from public.ll_file
            where fileid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setfileid(qRead.fileid);
            this.setorigfilename(qRead.origfilename);
            this.setstoragekey(qRead.storagekey);
            this.setuserid(qRead.userid);
            this.setnote(qRead.note);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="fileVO">
        <cfset var qCreate="">

        <cfset var local1=this.getorigfilename()>
        <cfset var local2=this.getstoragekey()>
        <cfset var local3=this.getuserid()>
        <cfset var local4=this.getnote()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_file(origfilename, storagekey, userid, note)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_INTEGER" null="#iif((local3 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select fileid
                from public.ll_file
                where origfilename = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />
                  and storagekey = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                  and userid = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_INTEGER" null="#iif((local3 eq ""), de("yes"), de("no"))#" />
                  and note = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />
                order by fileid desc
            </cfquery>
        </cftransaction>

        <cfset this.fileid = qGetID.fileid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="fileVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_file
            set origfilename = <cfqueryparam value="#this.getorigfilename()#" cfsqltype="CF_SQL_VARCHAR" />,
                storagekey = <cfqueryparam value="#this.getstoragekey()#" cfsqltype="CF_SQL_VARCHAR" />,
                userid = <cfqueryparam value="#this.getuserid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getuserid() eq ""), de("yes"), de("no"))#" />,
                note = <cfqueryparam value="#this.getnote()#" cfsqltype="CF_SQL_VARCHAR" />
            where fileid = <cfqueryparam value="#this.getfileid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>

        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_file
            where fileid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getfileid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>