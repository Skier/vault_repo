<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Our locations In</title>
</head>

<body>


<font color="#6B6B6B">


<font size="1" face="Arial">


<%
  dim objDC, strConn, objRS, State, City
  Set objDC = Server.CreateObject("ADODB.Connection")
  strConn="Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Server.MapPath("locations/locations.mdb")
  objDC.Open strConn
  Set objRS = objDC.Execute("Select DISTINCT State FROM locations Order by State")
    %>
</font>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
<tr>
<td width="99%" colspan="3">
<p align="center">
<font face="Arial" color="#E0631B"> 
<b>Please select a city and state to locate a dPi reseller near you..</b></font><font face="Arial" color="#6B6B6B" size="2"><br>
&nbsp;</font></td>
</tr>
<tr>
<td width="100%" align="left">
<FORM METHOD="POST" NAME="Form1" ACTION="our_locations_in.asp">

<p style="margin-left: 10px">

<font face="Arial" color="#6B6B6B" style="font-size: 8pt; font-weight:700"> 
Choose a State</font><font face="Arial" size="1" color="#6B6B6B" style="font-size: 9pt"><span style="font-size: 9pt">:&nbsp;
<SELECT NAME="State" style="font-size: 9pt" ONCHANGE=Form1.submit()>
<option selected><% = Request.Form("State") %></option>
<%
  ' Continue until we get to the end of the recordset.
  Do Until objRS.EOF
  ' For each record we create a option tag and set it's value to the country
  %>
<OPTION><%= objRS("State") %></OPTION>
<%
  ' Get next record
  objRS.MoveNext
  Loop
  %>
</SELECT></span></font><font face="Arial" color="#6B6B6B" style="font-size: 9pt"> 
</font>
</p>
</FORM>
<font color="#6B6B6B" size="1" face="Arial">
<%
  ' Close Data Access Objects and free DB variables
    objRS.Close
    Set objRS = Nothing
    objDC.Close
    Set objDC = Nothing
    %>
  
  <!-- End first Drop Down -->
  

<!--Second drop down -->
  <%
    'Some code to hide the second drop down until we make a selection from the first
    IF Request.Form("State") = "" Then
    Else
    'If State has a value then we get a list of cities for the second drop down
    Set objDC = Server.CreateObject("ADODB.Connection")
    strConn="Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Server.MapPath("locations/locations.mdb")
    objDC.Open strConn
    Set objRS = objDC.Execute("Select DISTINCT City FROM locations WHERE State = '" & Request.Form("State") & "'")
    %>
</font>
<FORM METHOD="POST" NAME="Form2" ACTION="our_locations_in.asp">
<input type="hidden" name="State" value="<% = Request.Form("State") %>">
<p style="margin-left: 10px; margin-top: 10px">
<font color="#6B6B6B" style="font-size: 8pt; font-weight:700" face="Arial"> 
Choose a City</font><font color="#6B6B6B" style="font-size: 9pt; " face="Arial">:&nbsp; </font><font face="Arial"><font color="#008080"><b>
<font face="Arial" size="1" color="#6B6B6B">
<span style="font-size: 9pt">
<SELECT NAME="City" style="font-size: 9pt" ONCHANGE=Form2.submit()>
<option selected><% = Request.Form("City") %></option>
<%
  ' Continue until we get to the end of the recordset.
  Do Until objRS.EOF
  ' For each record we create a option tag and set it's value to the city
  %>
<OPTION><%= UCase(objRS("City")) %></OPTION>
<%
  ' Get next record
  objRS.MoveNext
  Loop
  %>
<%
  'Set a hidden value in the second form for the State 
  'so we can pass it along with the city to the next query
  %>
</SELECT></span></font></b></font><font color="#6B6B6B" style="font-size: 9pt"> </font></font>
</p>
</FORM>
  <font color="#6B6B6B" size="1" face="Arial">
  <%
    ' Close Data Access Objects and free DB variables
    objRS.Close
    Set objRS = Nothing
    objDC.Close
    Set objDC = Nothing
    End IF
    %>
  <!-- Display the records -->
   </font>
   



</td>
</tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
<tr>
<td width="40%">

<font color="#6B6B6B" size="1" face="Arial">

<%
IF Request.Form("city") <> "" Then %>
<p align="center"><b><font face="Arial" color="#E0631B" style="font-size: 9pt">dPi 
Reseller Locations in:&nbsp;</font><font face="Arial" color="#E0631B" style="font-size: 9pt"> &nbsp;<% Response.Write Request.Form("city") & ", " & Request.Form("state") %> <br>
<%
End IF %></font></b></td>
</tr>
<tr>
<td width="40%">



<TABLE width='100%' border="0">


<% 
  'Make sure we have submitted a city and don't show results until we do
  IF Request.Form("city") = "" Then
  Else
  Set objDC = Server.CreateObject("ADODB.Connection")
  strConn="Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Server.MapPath("locations/locations.mdb")
  objDC.Open strConn
  Set objRS = objDC.Execute("Select * FROM locations WHERE State = '" & Request.Form("State") & "' AND City = '" & Request.Form("city") & "'")
  'Loop through the database and assign the appropriate values to variables
    'that we will use later
     

const_collumns = 2
thiscollumn = 0

 Do Until objRS.EOF

thiscollumn = thiscollumn + 1

  


td1="<td align='center' width='50%'>"
td2="</TD>"



    Name = objRS("Name")
    Address = objRS("Address")
    City = objRS("City")
    State = objRS("State")
    Zip = objRS("Zip")
    Phone1 = objRS("Phone")
    %>
</div>
</font>





     


<%

if thiscollumn = 1 then
                              response.write "<tr>"
                         end if
response.write td1
%>



<font color="#6B6B6B" size="1" face="Arial">
<br>
<%

  'Set up the display of the record
  Response.Write Name & "<br>"
  Response.Write Address & "<br>"
  Response.Write City & ", " & State & " " & Zip & "<br>"
  IF Phone1 <> "" Then 
       Response.Write "Phone: (" & Left(phone1,3) & ") " & Mid(phone1,4,3) & "-" & Right(phone1,4) & "<br> <br>"
  End IF


  response.write td2

  objRS.MoveNext

if thiscollumn = const_collumns then
                              response.write "</tr>"
                              thiscollumn = 0
                              end if
  Loop



  objRS.Close
  Set objRS = Nothing
  objDC.Close
  Set objDC = Nothing 
  End IF

  %> 
  </font>
  </p>


</TR>
</TABLE>









   </td>
</tr>
</table>


</body>

</html>