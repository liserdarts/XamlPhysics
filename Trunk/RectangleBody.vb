'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A <c>PhysicalBody</c> that will create a <c>RectangleGeometry</c> if no geometry is given
''' </summary>
Public Class RectangleBody
    Inherits SingleGeometryBody
    
    ''' <summary>
    ''' Gets or sets the width. If none is given the width will be calculated automatically.
    ''' </summary>
    Public Property Width() As Double
    
    ''' <summary>
    ''' Gets or sets the height. If none is given the height will be calculated automatically.
    ''' </summary>
    Public Property Height() As Double
    
    Protected Overrides Sub CreatePhysicalObject()
        MyBase.CreatePhysicalObject
        
        If TypeOf Element Is FrameworkElement Then
            Dim FElement As FrameworkElement = Element
            If Width = 0 Then Width = FElement.Width
            If Height = 0 Then Height = FElement.Height
        End If
        
        Dim Rect As New RectangleGeometry
        Rect.Width = Width
        Rect.Height = Height
        Geometries.Add(Rect)
    End Sub

End Class