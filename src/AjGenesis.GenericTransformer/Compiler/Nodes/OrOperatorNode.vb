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

Imports AjGenesis.Core

Class OrOperatorNode
    Inherits ExpressionNode

    Private mLeft As ExpressionNode
    Private mRight As ExpressionNode

    Sub New(ByVal left As Node, ByVal right As Node)
        mLeft = left
        mRight = right
    End Sub

    ReadOnly Property Left() As ExpressionNode
        Get
            Return mLeft
        End Get
    End Property

    ReadOnly Property Right() As ExpressionNode
        Get
            Return mRight
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim left As Object = mLeft.Evaluate(env)

        If IsTrue(left) Then
            Return True
        End If

        Dim right As Object = mRight.Evaluate(env)

        Return IsTrue(right)
    End Function
End Class
