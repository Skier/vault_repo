Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports AerSysCo.Common
Imports System.Xml
Imports System





Public Class PackageService
    Private Class Box
        Public weight As Integer
        Public SKU As String
    End Class


    Public Shared Function Package(ByVal strconn As String, ByVal request() As PackageRequest, ByVal ShoppingcartId As Int32) As PackageSet()
        Dim conn As New SqlConnection
        Dim result As New List(Of PackageSet)
        Try
            ' Dim path As String = System.Reflection.Assembly.GetExecutingAssembly.GetModules()(0).FullyQualifiedName
            'Dim fs = New IO.StreamWriter("c:\package.log", True)
            'fs.WriteLine(Now & "package function begins")
            'ddd

            Dim boxset As New List(Of Box)
            Dim boxset2 As New List(Of Box)
            Dim dt1 As New DataTable
            Dim MultiBoxLBLimit As Integer = 10
            Dim MultiBoxCntLimit As Integer = 3
            Dim MultBoxTLWeight As Integer = 0
            Dim MultBoxTLBoxCnt As Integer = 0
            Dim SingleBoxTLWeight As Integer = 0
            Dim SingleBoxTLBoxCnt As Integer = 0
            conn.ConnectionString = strconn
            conn.Open()
            Dim sw As New IO.StringWriter
            Dim xmlw As XmlTextWriter = New XmlTextWriter(sw)
            Dim i As Integer
            xmlw.WriteStartElement("root")
            xmlw.WriteStartElement("inputs")

            'Dim fs = New IO.StreamWriter("c:\package.log", True)
            'For Each pack As PackageRequest In request
            '    fs.WriteLine(Now & pack.SKU & " " & pack.ItemCount)
            'Next
            'fs.close()


            For Each pack As PackageRequest In request
                xmlw.WriteStartElement("input")
                xmlw.WriteAttributeString("SKU", pack.SKU)
                xmlw.WriteAttributeString("Qty", pack.ItemCount)

                Dim stritemrec As String = GetWeight(conn, pack.SKU)
                Dim item() As String = stritemrec.Split("|")
                xmlw.WriteAttributeString("Wt", item(1))
                xmlw.WriteAttributeString("BoxQty", item(2))

                'Calculate Box
                If CInt(item(1)) <= MultiBoxLBLimit Then 'Weight
                    MultBoxTLWeight = MultBoxTLWeight + CInt(item(1)) * (pack.ItemCount / CInt(item(2)))
                    MultBoxTLBoxCnt = MultBoxTLBoxCnt + (pack.ItemCount / CInt(item(2)))
                    xmlw.WriteAttributeString("SingleQTYBox", "N")
                Else
                    xmlw.WriteAttributeString("SingleQTYBox", "Y")
                    SingleBoxTLWeight = SingleBoxTLWeight + CInt(item(1)) * pack.ItemCount / CInt(item(2))
                    SingleBoxTLBoxCnt = SingleBoxTLBoxCnt + 1
                    Dim box_rec As New Box
                    Dim actualcnt As Integer = 0

                    Do While actualcnt < (pack.ItemCount / CInt(item(2)))
                        box_rec.weight = CInt(item(1))
                        box_rec.SKU = pack.SKU
                        boxset.Add(box_rec)
                        actualcnt = actualcnt + 1
                    Loop
                End If
                xmlw.WriteEndElement()
            Next


            xmlw.WriteEndElement()
            xmlw.WriteStartElement("PackageLogic")
            xmlw.WriteStartElement("SinglePackageBoxes")
            xmlw.WriteAttributeString("MuliBoxWeightLimit", MultiBoxLBLimit)
            xmlw.WriteAttributeString("ItemsPerBox", MultiBoxCntLimit)
            For Each rec As Box In boxset
                xmlw.WriteStartElement("Box")
                xmlw.WriteAttributeString("Wt", rec.weight)
                xmlw.WriteAttributeString("SKU", rec.SKU)
                xmlw.WriteEndElement()
            Next
            xmlw.WriteEndElement() 'SinglePackageBoxes


            If MultBoxTLBoxCnt > 0 Then
                Dim box As Integer = System.Math.Round((MultBoxTLBoxCnt / MultiBoxCntLimit) + 0.4, 0)
                ' Dim box As Integer = System.Math.Round((MultBoxTLBoxCnt / MultiBoxCntLimit), 0)

                Dim boxwt As Integer = System.Math.Round((MultBoxTLWeight / box) + 0.4, 1)
                xmlw.WriteStartElement("SpecialLogicPackageBoxes")
                xmlw.WriteAttributeString("TotalWight", MultBoxTLWeight)
                xmlw.WriteAttributeString("TotalBoxes", box)
                For i = 1 To box
                    Dim box_rec As New Box
                    box_rec.weight = boxwt
                    boxset2.Add(box_rec)
                    xmlw.WriteStartElement("Box")
                    xmlw.WriteAttributeString("Wt", box_rec.weight)
                    xmlw.WriteEndElement()
                Next
                xmlw.WriteEndElement() 'SpecialLogicPackageBoxes

            End If
            xmlw.WriteEndElement() 'packageLogic
            'Calculate final weigth and cnt to determine pallet size
            Dim tlwt As Integer = 0
            Dim tlcnt As Integer = 0

            For Each rec As Box In boxset
                Dim packset As New PackageSet
                tlwt = tlwt + rec.weight
                tlcnt = tlcnt + 1
                packset.weightLB = rec.weight
                result.Add(packset)
            Next

            For Each rec As Box In boxset2
                Dim packset As New PackageSet
                tlwt = tlwt + rec.weight
                tlcnt = tlcnt + 1
                packset.weightLB = rec.weight
                result.Add(packset)
            Next
            xmlw.WriteStartElement("PalletLogic")
            xmlw.WriteAttributeString("TotalWt", tlwt)
            xmlw.WriteAttributeString("TotalBoxes", tlcnt)
            Dim palletcnt As Integer = 0
            Dim palletover500 As Decimal


            palletover500 = Floor(CDbl(CInt(tlwt) / CInt(500)))
            'calc number of pallet over 500 lb

            For i = 1 To CDbl(palletover500)
                xmlw.WriteStartElement("Pallet")
                xmlw.WriteAttributeString("Desc", "Over 500 Lbs")
                xmlw.WriteAttributeString("Wt", 45)
                xmlw.WriteEndElement()
                palletcnt = palletcnt + 1
                Dim packset As New PackageSet
                packset.weightLB = 45
                result.Add(packset)
            Next

            Dim remainderwt = tlwt - (palletover500 * 500)

            If remainderwt < 150 And tlcnt > 10 Then
                xmlw.WriteStartElement("Pallet")
                xmlw.WriteAttributeString("Desc", " Less Than 150Lbs but Greater Than 10 Qty")
                xmlw.WriteAttributeString("Wt", 45)
                xmlw.WriteEndElement()
                palletcnt = palletcnt + 1
                Dim packset As New PackageSet
                packset.weightLB = 45
                result.Add(packset)
            ElseIf remainderwt > 150 Then
                palletcnt = palletcnt + 1
                xmlw.WriteStartElement("Pallet")
                xmlw.WriteAttributeString("Desc", " Greater Than 150Lbs")
                xmlw.WriteAttributeString("Wt", 45)
                xmlw.WriteEndElement()
                Dim packset As New PackageSet
                packset.weightLB = 45
                result.Add(packset)
            End If
            xmlw.WriteEndElement()

            xmlw.WriteStartElement("ArrayReturnToAffilia")
            For Each rec As PackageSet In result
                xmlw.WriteStartElement("Box")
                xmlw.WriteAttributeString("Wt", rec.weightLB)
                xmlw.WriteEndElement()
            Next
            xmlw.WriteEndElement()
            xmlw.WriteEndElement()

            'Dim a As String = sw.ToString()
            UpdateLog(conn, sw.ToString, ShoppingcartId)
            conn.Close()
            conn.Dispose()

        Catch ex As Exception


            Throw New Exception(ex.Message)


            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
        End Try
        Return result.ToArray
    End Function



    Private Shared Function Floor(ByVal input As Decimal) As Decimal
        Dim places As Integer = Convert.ToInt32(input).ToString().Length
        Return Decimal.Floor(input / (10 ^ places)) * (10 ^ places)

    End Function

    Private Shared Function GetWeight(ByVal conn As SqlConnection, ByVal SKU As String) As String
        Dim item As String = ""
        Dim sql As String = "getSKUWeight"
        Dim cmd As New SqlCommand
        Dim wt As Integer
        cmd.Connection = conn
        cmd.CommandText = sql
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("SKU", SKU))
        Dim rdr As SqlDataReader = cmd.ExecuteReader
        Dim schematable As DataTable = rdr.GetSchemaTable()
        If rdr.Read() Then
            If rdr.Item("Weight") = 0 Then
                wt = 3
            Else
                wt = rdr.Item("Weight")
            End If
            item = rdr.Item("SKU") & "|" & wt & "|" & rdr.Item("BoxQty")
        End If
        rdr.Close()
        Return item
    End Function


    Private Shared Sub UpdateLog(ByVal conn As SqlConnection, ByVal XML As String, ByVal shoppingcartid As Integer)
        Dim sql As String = "InsertPackageCalcLog"
        Dim cmd As New SqlCommand
        cmd.Connection = conn
        cmd.CommandText = sql
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("shoppingCartId", shoppingcartid))
        cmd.Parameters.Add(New SqlParameter("CalcXml", XML))
        cmd.ExecuteNonQuery()
    End Sub
End Class
