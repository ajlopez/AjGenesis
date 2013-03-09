'
' +---------------------------------------------------------------------+
' | AjGenesis - Code and Artifacts Generator in .NET                    |
' +---------------------------------------------------------------------+
' | Copyright (c) 2003-2011 Angel J. Lopez. All rights reserved.        |
' | http://www.ajlopez.com                                              |
' | http://www.ajlopez.net                                              |
' +---------------------------------------------------------------------+
' | This source file is subject to the ajgenesis Software License,      |
' | Version 1.0, that is bundled with this package in the file LICENSE. |
' | If you did not receive a copy of this file, you may read it online  |
' | at http://www.ajlopez.net/ajgenesis/license.php.                    |
' +---------------------------------------------------------------------+
'
'

Imports System.Xml

Public Module XmlUtilities
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

    Function GetNodeProperty(ByVal node As XmlNode, ByVal name As String) As String
        Return GetNodeProperty(node, name, Nothing)
    End Function
End Module
