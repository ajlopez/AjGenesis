Module AssemblyUtilities
    Private AssemblyNames As IList = New ArrayList

    Private Function GetTypeInLoadedAssemblies(ByVal typename As String) As Type
        Dim tp As Type
        Dim assem As System.Reflection.Assembly

        For Each assem In AppDomain.CurrentDomain.GetAssemblies
            tp = assem.GetType(typename)

            If Not tp Is Nothing Then
                Return tp
            End If
        Next

        Return Nothing
    End Function

    Private Sub LoadReferencedAssemblies(ByVal assem As System.Reflection.Assembly, ByVal loaded As IList, ByVal toload As IList)

    End Sub

    Private Sub LoadReferencedAssemblies(ByVal assem As System.Reflection.Assembly, ByVal loaded As IList)
        Dim refassem As System.Reflection.AssemblyName
        Dim loadedassem As System.Reflection.Assembly
        Dim assemname As String

        For Each refassem In assem.GetReferencedAssemblies
            assemname = refassem.Name
            If Not loaded.Contains(assemname) Then
                loaded.Add(assemname)
                Try
                    loadedassem = System.Reflection.Assembly.Load(refassem)
                    LoadReferencedAssemblies(assem, loaded)
                Catch ex As Exception

                End Try
            End If
        Next
    End Sub

    Private Sub LoadReferencedAssemblies()
        Dim loaded As New ArrayList

        Dim assemname As String
        Dim assem As System.Reflection.Assembly
        Dim refassem As System.Reflection.AssemblyName

        For Each assem In AppDomain.CurrentDomain.GetAssemblies
            assemname = assem.GetName.Name
            loaded.Add(assemname)
        Next

        For Each assem In AppDomain.CurrentDomain.GetAssemblies
            For Each refassem In assem.GetReferencedAssemblies
                assemname = refassem.Name
                If Not loaded.Contains(assemname) Then
                    loaded.Add(assemname)
                    Try
                        LoadReferencedAssemblies(System.Reflection.Assembly.Load(refassem), loaded)
                    Catch ex As Exception

                    End Try

                End If
            Next
        Next
    End Sub

    Private Function GetTypeByPartialName(ByVal typename As String) As Type
        Dim p As Integer

        p = typename.LastIndexOf(".")

        If p < 0 Then
            Return Nothing
        End If

        Dim partialname As String

        partialname = Left(typename, p)

        Dim assem As System.Reflection.Assembly

        Try
            assem = System.Reflection.Assembly.LoadWithPartialName(partialname)
            Return assem.GetType(typename)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetTypeByName(ByVal typename As String) As Type
        Dim tp As Type = Type.GetType(typename)

        If Not tp Is Nothing Then
            Return tp
        End If

        tp = GetTypeInLoadedAssemblies(typename)

        If Not tp Is Nothing Then
            Return tp
        End If

        tp = GetTypeByPartialName(typename)

        If Not tp Is Nothing Then
            Return tp
        End If

        LoadReferencedAssemblies()

        tp = GetTypeInLoadedAssemblies(typename)

        If Not tp Is Nothing Then
            Return tp
        End If

        Return tp
    End Function
End Module
