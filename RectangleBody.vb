'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A <c>PhysicalBody</c> that will create a <c>RectangleGeometry</c> if no geometry is given
''' </summary>
Public Class RectangleBody
    Inherits PhysicalBody
    
    ''' <summary>
    ''' Gets or sets the width. If none is given the width will be calculated automatically.
    ''' </summary>
    Public Property Width() As Double
    
    ''' <summary>
    ''' Gets or sets the height. If none is given the height will be calculated automatically.
    ''' </summary>
    Public Property Height() As Double
    
    Protected Overrides Sub CreatePhysicalObject(Element As System.Windows.UIElement)
        If TypeOf Element Is FrameworkElement Then
            Dim FElement As FrameworkElement = Element
            If Width = 0 Then Width = FElement.Width
            If Height = 0 Then Height = FElement.Height
        End If
        
        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Box.PointToMeter(Canvas.GetLeft(Element) + (Width / 2), Canvas.GetTop(Element) + (Height / 2))
        
        If Geometries.Count = 0 Then
            Geometries.Add(New RectangleGeometry)
        End If
    End Sub
    
    Protected Overrides Sub CreateTransforms(Element As UIElement)
        MyBase.CreateTransforms(Element)
        
        Rotate.CenterX = Width / 2
        Rotate.CenterY = Height / 2
    End Sub
End Class