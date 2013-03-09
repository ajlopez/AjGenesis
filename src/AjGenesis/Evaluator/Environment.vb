Imports System.Collections
Imports System.Collections.Specialized

Public Class Environment
    Private mValues As New ListDictionary()
    Private mParent As Environment
    Private mProcessor As Processor

    Public Sub New()

    End Sub

    Public Sub New(ByVal parent As Environment)
        Me.mParent = parent
    End Sub

    Public Property Parent() As Environment
        Get
            Return mParent
        End Get
        Set(ByVal Value As Environment)
            mParent = Value
        End Set
    End Property

    Public Property TopParent() As Environment
        Get
            If mParent Is Nothing Then
                Return Me
            End If

            Return mParent.TopParent
        End Get
        Set(ByVal Value As Environment)
            If mParent Is Nothing Then
                Parent = Value
            Else
                mParent.TopParent = Value
            End If
        End Set
    End Property

    Public Property Processor() As Processor
        Get
            If mProcessor Is Nothing AndAlso Not mParent Is Nothing Then
                Return mParent.Processor
            End If

            If mProcessor Is Nothing Then
                mProcessor = New Processor()
            End If

            Return mProcessor
        End Get
        Set(ByVal Value As Processor)
            mProcessor = Value
        End Set
    End Property

    Default Public Property Item(ByVal key As String) As Object
        Get
            Dim value As Object

            value = mValues(key)

            If value Is Nothing AndAlso Not mValues.Contains(key) AndAlso Not Parent Is Nothing Then
                Return Parent.Item(key)
            End If

            Return value
        End Get
        Set(ByVal Value As Object)
            mValues(key) = Value
        End Set
    End Property
End Class
