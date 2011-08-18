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

    Public Property Geomerty() As CircleGeometry
    
    Protected Overrides Sub CreatePhysicalObject()
        If TypeOf Element Is FrameworkElement Then
            Dim FElement As FrameworkElement = Element
            If Radius = 0 Then Radius = FElement.Width / 2
        End If
        
        Dim Position = New Point(Canvas.GetLeft(Element), Canvas.GetTop(Element))
        If Single.IsNaN(Position.X) Then Position.X = 0
        If Single.IsNaN(Position.Y) Then Position.Y = 0

        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Box.PointToMeter(Position.X + Radius, Position.Y + Radius)
        
        Dim Circle As New CircleGeometry
        Circle.Radius = Radius
        Geometries.Add(Circle)
    End Sub
    
    Protected Overrides Sub CreateTransforms()
        MyBase.CreateTransforms
        
        Rotate.CenterX = Radius
        Rotate.CenterY = Radius
    End Sub
End Class