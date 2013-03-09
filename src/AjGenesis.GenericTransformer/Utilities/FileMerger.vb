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

Imports System.IO
Imports AjGenesis.Core

Public Class FileMerger
    Dim newlines As String()
    Dim oldlines As String()
    Dim targetlines As String()

    Dim newfragments As IList
    Dim oldfragments As IList

    Private Function SearchFragment(ByVal texttosearch As String) As Fragment
        For Each frag As Fragment In oldfragments
            If frag.Text = texttosearch Then
                Return frag
            End If
        Next

        Return Nothing
    End Function

    Private Sub MergeByMarker(ByVal marker As String)
        Dim mlines As New ArrayList
        Dim lastn As Integer = 0

        For Each frag As Fragment In newfragments
            For n As Integer = lastn To frag.FromLine - 1
                mlines.Add(newlines(n))
            Next

            lastn = frag.ToLine + 1

            Dim oldfrag As Fragment

            oldfrag = SearchFragment(frag.Text)

            If oldfrag Is Nothing Then
                For n As Integer = frag.FromLine To frag.ToLine
                    mlines.Add(newlines(n))
                Next
            Else
                For n As Integer = oldfrag.FromLine To oldfrag.ToLine
                    mlines.Add(oldlines(n))
                Next
            End If
        Next

        For n As Integer = lastn To newlines.Length - 1
            mlines.Add(newlines(n))
        Next

        targetlines = mlines.ToArray(GetType(String))
    End Sub

    Private Function NormalizeLine(ByVal line As String) As String
        line = line.Replace(vbTab, " "c) ' Remove tabs

        While line.Contains("  ")
            line = line.Replace("  ", " ") ' Collapse spaces
        End While

        line = Trim(line) ' Remove initial and ending spaces

        Return line
    End Function

    Private Function MakeFragments(ByVal lines As String(), ByVal marker As String) As IList
        Dim fragments As New ArrayList
        Dim fragment As Fragment = Nothing
        Dim nline As Integer = 0

        For Each line As String In lines
            If line.Contains(marker) Then
                If fragment Is Nothing Then
                    fragment = New Fragment
                    fragment.FromLine = nline
                    fragment.Text = NormalizeLine(line)
                Else
                    fragment.ToLine = nline
                    fragments.Add(fragment)
                    fragment = Nothing
                End If
            End If

            nline += 1
        Next

        Return fragments
    End Function

    Public Sub Merge(ByVal newfile As String, ByVal oldfile As String, ByVal targetfile As String, ByVal marker As String)
        newlines = File.ReadAllLines(newfile)
        oldlines = File.ReadAllLines(oldfile)

        newfragments = MakeFragments(newlines, marker)
        oldfragments = MakeFragments(oldlines, marker)

        MergeByMarker(marker)

        File.WriteAllLines(targetfile, targetlines, System.Text.Encoding.Default)
    End Sub
End Class

Class Fragment
    Public FromLine As Integer
    Public ToLine As Integer
    Public Text As String
End Class