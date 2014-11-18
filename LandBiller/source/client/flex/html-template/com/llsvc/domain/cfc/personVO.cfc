<cfcomponent output="false" alias="com.llsvc.domain.cfc.personVO">
    <!---
         These are properties that are exposed by this CFC object.
         These property definitions are used when calling this CFC as a web services, 
         passed back to a flash movie, or when generating documentation

         NOTE: these cfproperty tags do not set any default property values.
    --->
    <cfproperty name="personid" type="numeric" default="0">
    <cfproperty name="firstname" type="string" default="">
    <cfproperty name="middlename" type="string" default="">
    <cfproperty name="lastname" type="string" default="">
    <cfproperty name="phone" type="string" default="">
    <cfproperty name="phonealt" type="string" default="">
    <cfproperty name="addressid" type="numeric" default="0">

    <cfscript>
        //Initialize the CFC with the default properties values.
        this.personid = 0;
        this.firstname = "";
        this.middlename = "";
        this.lastname = "";
        this.phone = "";
        this.phonealt = "";
        this.addressid = 0;
    </cfscript>

    <cffunction name="init" output="false" returntype="personVO">
        <cfargument name="id" required="false">
        <cfscript>
            if( structKeyExists(arguments, "id") )
            {
                load(arguments.id);
            }
            return this;
        </cfscript>
    </cffunction>

    <cffunction name="getPersonid" output="false" access="public" returntype="any">
        <cfreturn this.Personid>
    </cffunction>

    <cffunction name="setPersonid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Personid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>

    <cffunction name="getFirstname" output="false" access="public" returntype="any">
        <cfreturn this.Firstname>
    </cffunction>

    <cffunction name="setFirstname" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Firstname = arguments.val>
    </cffunction>

    <cffunction name="getMiddlename" output="false" access="public" returntype="any">
        <cfreturn this.Middlename>
    </cffunction>

    <cffunction name="setMiddlename" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Middlename = arguments.val>
    </cffunction>

    <cffunction name="getLastname" output="false" access="public" returntype="any">
        <cfreturn this.Lastname>
    </cffunction>

    <cffunction name="setLastname" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Lastname = arguments.val>
    </cffunction>

    <cffunction name="getPhone" output="false" access="public" returntype="any">
        <cfreturn this.Phone>
    </cffunction>

    <cffunction name="setPhone" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Phone = arguments.val>
    </cffunction>

    <cffunction name="getPhonealt" output="false" access="public" returntype="any">
        <cfreturn this.Phonealt>
    </cffunction>

    <cffunction name="setPhonealt" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfset this.Phonealt = arguments.val>
    </cffunction>

    <cffunction name="getAddressid" output="false" access="public" returntype="any">
        <cfreturn this.Addressid>
    </cffunction>

    <cffunction name="setAddressid" output="false" access="public" returntype="void">
        <cfargument name="val" required="true">
        <cfif (IsNumeric(arguments.val)) OR (arguments.val EQ "")>
            <cfset this.Addressid = arguments.val>
        <cfelse>
            <cfthrow message="'#arguments.val#' is not a valid numeric"/>
        </cfif>
    </cffunction>



    <cffunction name="save" output="false" access="public" returntype="personVO">
        <cfscript>
            if(this.getpersonid() eq 0)
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
            select  personid, firstname, middlename, lastname, phone, phonealt, 
                    addressid
            from public.ll_person
            where personid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#arguments.id#" />
        </cfquery>

        <cfscript>
            this.setpersonid(qRead.personid);
            this.setfirstname(qRead.firstname);
            this.setmiddlename(qRead.middlename);
            this.setlastname(qRead.lastname);
            this.setphone(qRead.phone);
            this.setphonealt(qRead.phonealt);
            this.setaddressid(qRead.addressid);
        </cfscript>
    </cffunction>



    <cffunction name="create" output="false" access="private" returntype="personVO">
        <cfset var qCreate="">

        <cfset var local1=this.getfirstname()>
        <cfset var local2=this.getmiddlename()>
        <cfset var local3=this.getlastname()>
        <cfset var local4=this.getphone()>
        <cfset var local5=this.getphonealt()>
        <cfset var local6=this.getaddressid()>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="LL_Expence">
                insert into public.ll_person(firstname, middlename, lastname, phone, phonealt, addressid)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local6#" cfsqltype="CF_SQL_INTEGER" null="#iif((local6 eq ""), de("yes"), de("no"))#" />
                )
            </cfquery>

            <!--- If your server has a better way to get the ID that is more reliable, use that instead --->
            <cfquery name="qGetID" datasource="LL_Expence">
                select personid
                from public.ll_person
                where firstname = <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />
                  and middlename = <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />
                  and lastname = <cfqueryparam value="#local3#" cfsqltype="CF_SQL_VARCHAR" />
                  and phone = <cfqueryparam value="#local4#" cfsqltype="CF_SQL_VARCHAR" />
                  and phonealt = <cfqueryparam value="#local5#" cfsqltype="CF_SQL_VARCHAR" />
                  and addressid = <cfqueryparam value="#local6#" cfsqltype="CF_SQL_INTEGER" null="#iif((local6 eq ""), de("yes"), de("no"))#" />
                order by personid desc
            </cfquery>
        </cftransaction>

        <cfset this.personid = qGetID.personid>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="update" output="false" access="private" returntype="personVO">
        <cfset var qUpdate="">

        <cfquery name="qUpdate" datasource="LL_Expence" result="status">
            update public.ll_person
            set firstname = <cfqueryparam value="#this.getfirstname()#" cfsqltype="CF_SQL_VARCHAR" />,
                middlename = <cfqueryparam value="#this.getmiddlename()#" cfsqltype="CF_SQL_VARCHAR" />,
                lastname = <cfqueryparam value="#this.getlastname()#" cfsqltype="CF_SQL_VARCHAR" />,
                phone = <cfqueryparam value="#this.getphone()#" cfsqltype="CF_SQL_VARCHAR" />,
                phonealt = <cfqueryparam value="#this.getphonealt()#" cfsqltype="CF_SQL_VARCHAR" />,
                addressid = <cfqueryparam value="#this.getaddressid()#" cfsqltype="CF_SQL_INTEGER" null="#iif((this.getaddressid() eq ""), de("yes"), de("no"))#" />
            where personid = <cfqueryparam value="#this.getpersonid()#" cfsqltype="CF_SQL_INTEGER">
        </cfquery>
        <cfscript>
            return this;
        </cfscript>
    </cffunction>



    <cffunction name="delete" output="false" access="public" returntype="void">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="LL_Expence" result="status">
            delete
            from public.ll_person
            where personid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#this.getpersonid()#" />
        </cfquery>
    </cffunction>


</cfcomponent>