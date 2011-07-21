'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A <c>PhysicalBody</c> that will create a <c>CircleGeometry</c> if no geometry is given
''' </summary>
Public Class CircleBody
    Inherits PhysicalBody
    
    ''' <summary>
    ''' Gets or sets the Radius. If none is given the Radius will be calculated automatically.
    ''' </summary>
    Public Property Radius() As Double
    
    Protected Overrides Sub CreatePhysicalObject(Element As System.Windows.UIElement)
        If TypeOf Element Is FrameworkElement Then
            Dim FElement As FrameworkElement = Element
            If Radius = 0 Then Radius = FElement.Width / 2
        End If
        
        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Box.PointToMeter(Canvas.GetLeft(Element) + Radius, Canvas.GetTop(Element) + Radius)
        
        If Geometries.Count = 0 Then
            Geometries.Add(New CircleGeometry)
        End If
    End Sub
    
    Protected Overrides Sub CreateTransforms(Element As UIElement)
        MyBase.CreateTransforms(Element)
        
        Rotate.CenterX = Radius
        Rotate.CenterY = Radius
    End Sub
End Class