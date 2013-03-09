Imports System.Xml

Module XmlUtilities
    Function GetNodeProperty(ByVal node As XmlNode, ByVal name As String, ByVal defaultValue As String) As String
        Dim attr As XmlAttribute

        attr = node.Attributes(name)

        If Not attr Is Nothing Then
            Return attr.Value
        End If

        Dim prop As XmlNode

        prop = node.SelectSingleNode(name)

        If prop Is Nothing Then
            Return defaultValue
        End If

        Return prop.InnerText
    End Function

    Function GetNodeProperty(ByVal node As XmlNode, ByVal name As String)
        Return GetNodeProperty(node, name, Nothing)
    End Function
End Module
