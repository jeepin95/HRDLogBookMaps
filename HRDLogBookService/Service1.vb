' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Imports System.Data.Entity.Spatial
Imports System.Data.SqlClient

Public Class Service1
    Implements IService1

    Public Function getGridsReached() As List(Of GridReached) Implements IService1.getGridsReached
        Dim r As List(Of GridReached) = New List(Of GridReached)

        Dim conn As SqlConnection = New SqlConnection("data source=localhost;initial catalog=HRDLogbook;integrated security=True;")
        conn.Open()
        Dim sql As String = "SELECT *,Shape.STAsBinary() AS ShapeBinary FROM vwGridSquaresReached"
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        Dim results = cmd.ExecuteReader()
        While results.Read
            Dim gr As GridReached = New GridReached
            gr.Callsign = results.Item("COL_CALL")
            gr.GridSquare = results.Item("GridSquare")
            gr.MH2 = results.Item("MH2")
            gr.Distance = results.Item("COL_DISTANCE")
            gr.Band = results.Item("COL_BAND")
            gr.Mode = results.Item("COL_MODE")
            gr.syncid = results.Item("syncid").ToString()
            gr.Shape = DbGeometry.FromBinary(results.Item("ShapeBinary"), "4326")

            r.Add(gr)
        End While

        Return r

    End Function

    Public Function GetData(ByVal value As Integer) As String Implements IService1.GetData
        Return String.Format("You entered: {0}", value)
    End Function

    Public Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType Implements IService1.GetDataUsingDataContract
        If composite Is Nothing Then
            Throw New ArgumentNullException("composite")
        End If
        If composite.BoolValue Then
            composite.StringValue &= "Suffix"
        End If
        Return composite
    End Function

End Class
