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

Public Interface IObject
    Function GetValue(ByVal name As String) As Object
    Sub SetValue(ByVal name As String, ByVal value As Object)
    Function GetNames() As IList
    Function AcceptValue(ByVal name As String) As Boolean
    Function GetLeftValue(ByVal name As String) As Object
    Sub AddValue(ByVal name As String, ByVal obj As Object)
    Sub RemoveValue(ByVal name As String, ByVal obj As Object)
    Sub Copy(ByVal obj As Object)
    Function Equals(ByVal obj As Object) As Boolean
End Interface
