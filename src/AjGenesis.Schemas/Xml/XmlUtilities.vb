Imports System.Xml
Imports System.IO

Public Module XmlUtilities
    Private Sub ProcessElement(ByVal reader As XmlReader)
        If reader.IsEmptyElement Then
            reader.ReadStartElement()
            Return
        End If

        reader.ReadStartElement()

        Do Until reader.NodeType = XmlNodeType.EndElement
            If reader.NodeType = XmlNodeType.Element Then
                ProcessElement(reader)
            Else
                reader.Read()
            End If
        Loop

        reader.ReadEndElement()
    End Sub

    Private Sub LoadProperty(ByVal prop As [Property], ByVal reader As XmlReader)
        Dim isempty As Boolean

        isempty = reader.IsEmptyElement

        While reader.MoveToNextAttribute
            Select Case reader.Name
                Case "Name"
                    prop.Name = reader.Value
                Case "Type"
                    prop.TypeName = reader.Value
            End Select
        End While

        reader.ReadStartElement("Property")
    End Sub

    Private Sub LoadProperties(ByVal type As Type, ByVal reader As XmlReader)
        Dim isempty As Boolean

        isempty = reader.IsEmptyElement
        reader.ReadStartElement("Properties")

        If Not isempty Then
            Do Until reader.NodeType = XmlNodeType.EndElement
                Select Case reader.Name
                    Case "Property"
                        Dim prop As New [Property]
                        LoadProperty(prop, reader)
                        type.AddProperty(prop)
                    Case Else
                        If reader.NodeType = XmlNodeType.Element Then
                            ProcessElement(reader)
                        Else
                            reader.Read()
                        End If
                End Select
            Loop

            reader.ReadEndElement()
        End If
    End Sub

    Private Sub LoadType(ByVal type As Type, ByVal reader As XmlReader)
        Dim isempty As Boolean

        isempty = reader.IsEmptyElement

        While reader.MoveToNextAttribute
            Select Case reader.Name
                Case "Name"
                    type.Name = reader.Value
            End Select
        End While

        reader.ReadStartElement("Type")

        If Not isempty Then
            Do Until reader.NodeType = XmlNodeType.EndElement
                Select Case reader.Name
                    Case "Name"
                        type.Name = reader.ReadElementString
                    Case "Properties"
                        LoadProperties(type, reader)
                    Case Else
                        If reader.NodeType = XmlNodeType.Element Then
                            ProcessElement(reader)
                        Else
                            reader.Read()
                        End If
                End Select
            Loop

            reader.ReadEndElement()
        End If
    End Sub

    Private Sub LoadTypes(ByVal schema As Schema, ByVal reader As XmlReader)
        Dim isempty As Boolean

        isempty = reader.IsEmptyElement
        reader.ReadStartElement("Types")

        If Not isempty Then
            Do Until reader.NodeType = XmlNodeType.EndElement
                Select Case reader.Name
                    Case "Type"
                        Dim type As New type
                        LoadType(type, reader)
                        schema.AddType(type)
                    Case Else
                        If reader.NodeType = XmlNodeType.Element Then
                            ProcessElement(reader)
                        Else
                            reader.Read()
                        End If
                End Select
            Loop

            reader.ReadEndElement()
        End If
    End Sub

    Private Sub LoadSchema(ByVal schema As Schema, ByVal reader As XmlReader)
        Dim isempty As Boolean

        isempty = reader.IsEmptyElement

        While reader.MoveToNextAttribute
            Select Case reader.Name
                Case "Name"
                    schema.Name = reader.Value
                Case "Root"
                    schema.RootName = reader.Value
            End Select
        End While

        reader.ReadStartElement("Schema")

        If Not isempty Then
            Do Until reader.NodeType = XmlNodeType.EndElement
                Select Case reader.Name
                    Case "Name"
                        schema.Name = reader.ReadElementString
                    Case "Root"
                        schema.RootName = reader.ReadElementString
                    Case "Types"
                        LoadTypes(schema, reader)
                    Case Else
                        reader.Read()
                End Select
            Loop

            reader.ReadEndElement()
        End If
    End Sub

    Public Function LoadSchemaFromFile(ByVal filename As String) As Schema
        Dim reader As New XmlTextReader(File.OpenText(filename))
        reader.WhitespaceHandling = WhitespaceHandling.None

        Dim schema As New schema

        reader.MoveToContent()
        LoadSchema(schema, reader)

        Return schema
    End Function
End Module
