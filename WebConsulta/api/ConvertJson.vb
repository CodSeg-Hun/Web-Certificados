Public Class ConvertJson

    Public Function SerializarJson(ByVal firstTable As DataTable) As String
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))
            Dim row As Dictionary(Of String, Object)
            Dim msginicio As String = "{" & """" & "results" & """" & ": "
            Dim msgfinal As String = " }"
            For Each dr As DataRow In firstTable.Rows
                row = New Dictionary(Of String, Object)
                For Each col As DataColumn In firstTable.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            Return msginicio & serializer.Serialize(rows).ToString & msgfinal
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
