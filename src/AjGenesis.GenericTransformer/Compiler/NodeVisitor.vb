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

Class NodeVisitor
    Sub Visit(ByVal node As Node)
        If TypeOf node Is NameNode Then
            Visit(DirectCast(node, NameNode))
        ElseIf TypeOf node Is DotNode Then
            Visit(DirectCast(node, DotNode))
        Else
            Console.WriteLine("???")
        End If
    End Sub

    Sub Visit(ByVal name As NameNode)
        Console.WriteLine(name.Name)
    End Sub

    Sub Visit(ByVal dot As DotNode)
        Visit(dot.Subject)
        Console.WriteLine(".")
        Visit(dot.Verb)
    End Sub
End Class
