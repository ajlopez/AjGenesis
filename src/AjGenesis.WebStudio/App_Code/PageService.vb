Imports Microsoft.VisualBasic

Imports System.IO
Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions

Public Class DynamicPage
    Private mCode As String
    Private mTitle As String
    Private mContent As String
    Private mRelated As List(Of String)
    Private mCategories As List(Of String)

    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal value As String)
            mCode = value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value
        End Set
    End Property

    Public Property Content() As String
        Get
            Return mContent
        End Get
        Set(ByVal value As String)
            mContent = value
        End Set
    End Property

    Public Property Related() As List(Of String)
        Get
            Return mRelated
        End Get
        Set(ByVal value As List(Of String))
            mRelated = value
        End Set
    End Property

    Public Property Categories() As List(Of String)
        Get
            Return mCategories
        End Get
        Set(ByVal value As List(Of String))
            mCategories = value
        End Set
    End Property
End Class

Public Class PageService
    Private Shared mSerializer As New XmlSerializer(GetType(DynamicPage))
    Private Shared rex As New Regex("\[\[[^\]]*\]\]")
    Private Shared mev As New MatchEvaluator(AddressOf EvaluateMatch)

    Public Shared Function GetPagesDirectory() As String
        Return System.Web.HttpContext.Current.Server.MapPath("~/Pages")
    End Function

    Public Shared Function GetPageFilename(ByVal code As String) As String
        Return GetPagesDirectory() & "/" & code & ".xml"
    End Function

    Public Shared Function GetPage(ByVal code As String) As DynamicPage
        Dim pagefilename As String

        pagefilename = GetPageFilename(code)

        If Not File.Exists(pagefilename) Then
            Return NewPage(code)
        End If

        Dim pagefile As New StreamReader(pagefilename)

        Dim page As DynamicPage = mSerializer.Deserialize(pagefile)

        pagefile.Close()

        Return page
    End Function

    Public Shared Function NewPage(ByVal code As String) As DynamicPage
        Dim page As New DynamicPage

        page.Code = code
        page.Title = code
        page.Content = "(To be done)"

        Return page
    End Function

    Public Shared Sub SavePage(ByVal page As DynamicPage)
        Dim pagefilename As String

        pagefilename = GetPageFilename(Page.Code)

        Dim pagefile As New StreamWriter(pagefilename)

        mSerializer.Serialize(pagefile, page)

        pagefile.Close()
    End Sub

    Public Shared Function GetContentAsHtml(ByVal pagecontent As String) As String
        Dim html As String = rex.Replace(pagecontent, mev)
        html = html.Replace(vbLf & vbLf, "<br /><br />" & vbLf & vbLf)
        html = html.Replace("\[", "[")
        html = html.Replace("\]", "]")
        html = html.Replace("\<", "&lt;")
        html = html.Replace("\>", "&gt;")
        html = html.Replace("\\", "\")

        Return html
    End Function

    Private Shared Function EvaluateMatch(ByVal match As Match) As String
        Dim text As String = match.Value.Substring(2, match.Value.Length - 4)
        Dim parts As String() = text.Split("|")
        Dim url As String = parts(0)
        Dim value As String
        Dim result As String

        If parts.Length > 1 Then
            value = parts(1)
        Else
            value = parts(0)
        End If

        If url.StartsWith("image:") Then ' Image
            url = url.Substring(6)

            If Not url.Contains(":") Then
                url = System.Web.HttpContext.Current.Request.ApplicationPath() & "/" & url
            End If

            result = String.Format("<img src='{0}'>", url)
        Else ' Link
            If url.Contains(":") Then
                ' nothing
            ElseIf url.StartsWith("~/") Then
                url = System.Web.HttpContext.Current.Request.ApplicationPath & url.Substring(1)
                If parts.Length = 1 Then
                    value = url
                End If
            Else
                url = String.Format("{0}?Page={1}", System.Web.HttpContext.Current.Request.ApplicationPath() & "/PageView.aspx", url)
            End If

            result = String.Format("<a href='{0}'>{1}</a>", url, value)
        End If

        Return result
    End Function
End Class
