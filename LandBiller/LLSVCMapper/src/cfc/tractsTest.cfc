<cfcomponent>
    
     <cffunction name="updateTracts" access="remote" returntype="any">
    <cfargument name="tracts" required="yes" type="array">
    
        <cfscript>
            For (i=1;i LTE ArrayLen(tracts); i=i+1)
            {
                updateTract(tracts[variables.i]);
            }
            
        </cfscript>

    </cffunction>
    
    <cffunction name="updateTract" access="remote" returntype="any">
   
    <cfargument name="tract" required="yes" type="struct">
    
    <cfset var qUpdate="">

       <cfquery name="qUpdate" datasource="parclesTest">
        
        update public.tad_parcles
            set taxpin = <cfqueryparam value="#tract.taxpin#" cfsqltype="CF_SQL_VARCHAR" />,
                acres = <cfqueryparam value="#tract.acres#" cfsqltype="CF_SQL_VARCHAR" />,
                calculated = <cfqueryparam value="#tract.calculated#" cfsqltype="CF_SQL_NUMERIC"/>,
                parceltype = <cfqueryparam value="#tract.parceltype#" cfsqltype="CF_SQL_BIGINT"/>,
                area = <cfqueryparam value="#tract.area#" cfsqltype="CF_SQL_NUMERIC"/>,
                len = <cfqueryparam value="#tract.len#" cfsqltype="CF_SQL_NUMERIC"/>,
                the_geom = ST_GeomFromText('MULTIPOLYGON(((#tract.the_geom#)))',4326),

                pidn = <cfqueryparam value="#tract.pidn#" cfsqltype="CF_SQL_VARCHAR" />
                where gid = <cfqueryparam value="#tract.gid#" cfsqltype="CF_SQL_INTEGER">
       </cfquery>

    </cffunction>
    

    
    
    
    <cffunction name="getTractsByExtent" access="remote" returntype="any">
       <cfargument name="ll_lat" required="true"> 
       <cfargument name="ll_lon" required="true"> 
       <cfargument name="ur_lat" required="true"> 
       <cfargument name="ur_lon" required="true"> 
       
        <cfquery name="qRead" datasource="parclesTest">
            SELECT
            public.tad_parcles.gid,
            public.tad_parcles.taxpin,
            public.tad_parcles.acres,
            public.tad_parcles.calculated,
            public.tad_parcles.parceltype,
            public.tad_parcles.area,
            public.tad_parcles.len,
            ST_AsText(public.tad_parcles.the_geom) AS geom,
            public.aaaa."Owner Name"
 
            FROM
            public.tad_parcles
            Inner Join public.aaaa ON public.aaaa."GisLink" = public.tad_parcles.taxpin
            WHERE
            public.tad_parcles.the_geom && SetSRID('BOX3D(#ll_lon# #ll_lat#,#ur_lon# #ur_lat#)'::box3d,4326);
        </cfquery>
        
        <cfreturn qread>
        
    </cffunction>
    
    <cffunction name="createTracts" access="remote" returntype="any">
    
    <cfargument name="tracts" required="yes" type="array">
     <cfset var newTracts = ArrayNew(1)>
    
        <cfscript>
            For (i=1;i LTE ArrayLen(tracts); i=i+1)
            {
               ArrayAppend(newTracts,createTract(tracts[variables.i]));
            }
            
        </cfscript>

    </cffunction>
    
    <cffunction name="createTract" output="false" access="private" returntype="any">
        <cfargument name="tract" required="yes" type="struct">
        
        <cfset var qCreate="">

        <cfset var local1=tract.taxpin>
        <cfset var local2=tract.acres>
        <cfset var local3=tract.calculated>
        <cfset var local4=tract.parceltype>
        <cfset var local5=tract.area>
        <cfset var local6=tract.len>
    <!---   <cfset var local7=tract.the_geom>--->
        <cfset var local8=tract.pidn>

        <cftransaction isolation="read_committed">
            <cfquery name="qCreate" datasource="parclesTest">
                insert into public.tad_parcles(taxpin, acres, calculated, parceltype, area, len, the_geom, pidn)
                values (
                    <cfqueryparam value="#local1#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local2#" cfsqltype="CF_SQL_VARCHAR" />,
                    <cfqueryparam value="#local3#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local3 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local4#" cfsqltype="CF_SQL_BIGINT" null="#iif((local4 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local5#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local5 eq ""), de("yes"), de("no"))#" />,
                    <cfqueryparam value="#local6#" cfsqltype="CF_SQL_NUMERIC" null="#iif((local6 eq ""), de("yes"), de("no"))#" />,
                    (ST_GeomFromText('MULTIPOLYGON(((#tract.the_geom#)))',4326)),
                    <cfqueryparam value="#local8#" cfsqltype="CF_SQL_VARCHAR" />
                )
                RETURNING gid;
            </cfquery>
            
            <cfreturn qCreate>

    </cffunction>
    
    
    
    
    
  <cffunction name="deleteTracts" access="remote" returntype="any">
    <cfargument name="tracts" required="yes" type="array">
    
        <cfscript>
            For (i=1;i LTE ArrayLen(tracts); i=i+1)
            {
                deleteTract(tracts[variables.i]);
            }
            
        </cfscript>

    </cffunction>
    
    <cffunction name="deleteTract" output="false" access="public" returntype="void">
        <cfargument name="tract" required="yes" type="struct">
        <cfset var qDelete="">

        <cfquery name="qDelete" datasource="parclesTest" result="status">
            delete
            from public.tad_parcles
            where gid = <cfqueryparam cfsqltype="CF_SQL_INTEGER" value="#tract.gid#" />
        </cfquery>
    </cffunction>

</cfcomponent>

