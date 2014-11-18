<cftry>

    <cfset currentPath = getCurrentTemplatePath()>

    <cffile action="upload" 
            filefield="filedata" 
            destination="#getDirectoryFromPath(currentPath)#ExpenseFiles\InvoiceItemAttachments\" 
            nameconflict="makeunique" 
            accept="application/octet-stream"/>
        
    <cffile action="rename" 
            destination = "#URL.uniqueFileName#" 
            source = "#getDirectoryFromPath(currentPath)#ExpenseFiles\InvoiceItemAttachments\#File.ServerFile#"/>
        
    <cfcatch type="any">
        <cfdocument format="PDF" overwrite="yes" filename="errordebug.pdf">
            <cfdump var="#cfcatch#"/>
        </cfdocument>
    </cfcatch>
</cftry>
