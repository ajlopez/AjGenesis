Imports System.Xml.Serialization

<XmlRoot("Node")> _
Public Class RecipeNode
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

    Private mNodes As List(Of RecipeNode)

    <XmlElement("Node")> _
    Public Property Nodes() As List(Of RecipeNode)
        Get
            Return mNodes
        End Get
        Set(ByVal value As List(Of RecipeNode))
            mNodes = value
        End Set
    End Property

    Private mRecipes As List(Of Recipe)

    <XmlElement("Recipe")> _
    Public Property Recipes() As List(Of Recipe)
        Get
            Return mRecipes
        End Get
        Set(ByVal value As List(Of Recipe))
            mRecipes = value
        End Set
    End Property
End Class
