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
