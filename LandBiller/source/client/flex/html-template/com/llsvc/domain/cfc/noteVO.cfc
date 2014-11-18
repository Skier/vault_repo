<cfcomponent output="false" alias="com.llsvc.domain.cfc.noteVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="noteid" type="numeric" default="0">
    <cfproperty name="userid" type="numeric" default="0">
    <cfproperty name="invoiceid" type="numeric" default="0">
    <cfproperty name="notedate" type="date" default="">
    <cfproperty name="notetext" type="string" default="">
    <cfproperty name="notefrom" type="string" default="">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.noteid = 0;
        this.userid = 0;
        this.invoiceid = 0;
        this.notedate = "";
        this.notetext = "";
        this.notefrom = "";
    </cfscript>

    <cffunction name="init" output="false" returntype="noteVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getNoteid" output="false" access="public" returntype="any">
        <cfreturn this.Noteid>
    </cffunction>

    <cffunction name="setNoteid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Noteid = arguments.val>
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

    <cffunction name="getInvoiceid" output="false" access="public" returntype="any">
        <cfreturn this.Invoiceid>
    </cffunction>

    <cffunction name="setInvoiceid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Invoiceid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getNotedate" output="false" access="public" returntype="any">
        <cfreturn this.Notedate>
    </cffunction>

    <cffunction name="setNotedate" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsDate(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Notedate = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid date"/>
        </cfif>
    </cffunction>

    <cffunction name="getNotetext" output="false" access="public" returntype="any">
        <cfreturn this.Notetext>
    </cffunction>

    <cffunction name="setNotetext" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Notetext = arguments.val>
    </cffunction>

    <cffunction name="getNotefrom" output="false" access="public" returntype="any">
        <cfreturn this.Notefrom>
    </cffunction>

    <cffunction name="setNotefrom" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Notefrom = arguments.val>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="noteVO">
        <cfscript>
            if(this.getnoteid() eq 0)
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
            select  noteid, userid, invoiceid, notedate, notetext, notefrom
            from public.ll_note
            where noteid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setnoteid(qRead.noteid);
            this.setuserid(qRead.userid);
            this.setinvoiceid(qRead.invoiceid);
            this.setnotedate(qRead.notedate);
            this.setnotetext(qRead.notetext);
            this.setnotefrom(qRead.notefrom);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="noteVO">
        <cfset var qCreate="">

        <cfset var local1=this.getuserid()>
        <cfset var local2=this.getinvoiceid()>
        <cfset var local3=this.getnotedate()>
        <cfset var local4=this.getnotetext()>
        <cfset var local5=this.getnotefrom()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_note(userid, invoiceid, notedate, notetext, notefrom)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_DATE" null="#iif((local3 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select noteid
                from public.ll_note
                where userid = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_INTEGER" null="#iif((local1 eq ""), de("yes"), de("no"))#" />
                  and invoiceid = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_INTEGER" null="#iif((local2 eq ""), de("yes"), de("no"))#" />
                  and notedate = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_DATE" null="#iif((local3 eq ""), de("yes"), de("no"))#" />
                  and notetext = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />
                  and notefrom = <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                order by noteid desc
            </cfquery>
        </cftransaction>

        <cfset this.noteid = qGetID.noteid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="noteVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_note
            set userid = <cfqueryparam value="#this.getuserid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getuserid() eq ""), de("yes"), de("no"))#" />,
                invoiceid = <cfqueryparam value="#this.getinvoiceid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getinvoiceid() eq ""), de("yes"), de("no"))#" />,
                notedate = <cfqueryparam value="#this.getnotedate()#" cfsqltype="CF_SQL_DATE" null="#iif((this.getnotedate() eq ""), de("yes"), de("no"))#" />,
                notetext = <cfqueryparam value="#this.getnotetext()#" cfsqltype="CF_SQL_VARCHAR" />,
                notefrom = <cfqueryparam value="#this.getnotefrom()#" cfsqltype="CF_SQL_VARCHAR" />
            where noteid = <cfqueryparam value="#this.getnoteid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_note
            where noteid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getnoteid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>