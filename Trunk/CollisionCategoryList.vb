'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class CollisionCategoryCollection
    Inherits Collections.ObjectModel.Collection(Of CollisionCategory)

    Public Function GetCombinedValue() As FarseerPhysics.Dynamics.Category
        Dim Value As FarseerPhysics.Dynamics.Category
        
        For Each Category In Me
            If Category.Include Then
                Value = Value Or Category.Category
            Else
                Value = Value Or (Not Category.Category)
            End If
        Next

        Return Value
    End Function

End Class
Public Class CollisionCategory
    Public Property Include() As Boolean
    Public Property Category As FarseerPhysics.Dynamics.Category
End Class