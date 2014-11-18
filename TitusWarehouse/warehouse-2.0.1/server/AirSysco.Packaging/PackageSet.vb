Imports System
Imports System.Text

Public Class PackageSet
    Public Sub PackageSet(ByVal Weight As Decimal, ByVal length As Int32, ByVal Width As Int32, ByVal Height As Int32)
        weightLB = Weight
        lengthIN = length
        widthIN = Width
        heightIN = Height
    End Sub
    Public weightLB As Decimal
    Public lengthIN As Int32
    Public widthIN As Int32
    Public heightIN As Int32
End Class


