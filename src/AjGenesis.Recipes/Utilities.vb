Module Utilities
    Sub RecipesToTree(ByVal recipes As Recipes, ByVal tree As TreeView)
        Dim tn As New TreeNode
        tn.Text = recipes.Name
        tree.Nodes.Add(tn)

        tn.ImageIndex = 1
        tn.SelectedImageIndex = 1
        tn.Tag = recipes

        NodesToNode(recipes.Nodes, tn)
        RecipesToNode(recipes.Recipes, tn)
    End Sub

    Sub NodesToNode(ByVal nodes As List(Of RecipeNode), ByVal node As TreeNode)
        If nodes Is Nothing Then
            Return
        End If

        For Each nd As RecipeNode In nodes
            Dim tn As New TreeNode
            tn.Text = nd.Name
            tn.ImageIndex = 1
            tn.SelectedImageIndex = 1
            tn.Tag = nd
            node.Nodes.Add(tn)
            NodesToNode(nd.Nodes, tn)
            RecipesToNode(nd.Recipes, tn)
        Next
    End Sub

    Sub RecipesToNode(ByVal recipes As List(Of Recipe), ByVal node As TreeNode)
        If recipes Is Nothing Then
            Return
        End If

        For Each recipe As Recipe In recipes
            Dim tn As New TreeNode
            tn.Text = recipe.Name
            tn.ImageIndex = 0
            tn.SelectedImageKey = 0
            tn.Tag = recipe
            node.Nodes.Add(tn)
        Next
    End Sub
End Module
