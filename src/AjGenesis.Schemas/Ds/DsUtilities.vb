Public Module DsUtilities
    Private Function DsProperties() As DataSet
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds.Tables.Add(dt)

        Dim dc As DataColumn

        dc = New DataColumn("Property")
        dc.ReadOnly = True

        dt.Columns.Add(dc)

        dc = New DataColumn("Value")
        dt.Columns.Add(dc)

        Return ds
    End Function

    Private Sub AddPropertyValue(ByVal ds As DataSet, ByVal name As String, ByVal value As Object)
        Dim dr As DataRow

        dr = ds.Tables(0).NewRow
        dr("Property") = name
        dr("Value") = value
        ds.Tables(0).Rows.Add(dr)
    End Sub

    Public Function PropertyAsDataSet(ByVal prop As [Property]) As DataSet
        Dim ds As DataSet

        ds = DsProperties()

        AddPropertyValue(ds, "Name", prop.Name)
        AddPropertyValue(ds, "Type", prop.TypeName)
        AddPropertyValue(ds, "IsItem", prop.IsItem)

        Return ds
    End Function

    Public Function TypeAsDataSet(ByVal type As Type) As DataSet
        Dim ds As DataSet

        ds = DsProperties()

        AddPropertyValue(ds, "Name", type.Name)

        Return ds
    End Function
End Module
