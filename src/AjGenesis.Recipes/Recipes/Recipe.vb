Imports System.Xml
Imports System.Xml.Serialization

<XmlRoot("Recipe")> _
Public Class Recipe
    Private mName As String

    <XmlAttribute("Name")> _
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    Private mTask As String

    <XmlAttribute("Task")> _
    Public Property Task() As String
        Get
            Return mTask
        End Get
        Set(ByVal value As String)
            mTask = value
        End Set
    End Property

    Private mDocumentation As String

    <XmlAttribute("Documentation")> _
    Public Property Documentation() As String
        Get
            Return mDocumentation
        End Get
        Set(ByVal value As String)
            mDocumentation = value
        End Set
    End Property
End Class
