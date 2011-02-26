'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' PhysicalBody that will create an EllipseGeometry if none is provided
''' </summary>
Public Class EllipseBody
    Inherits PhysicalBody
    
    ''' <summary>
    ''' The width of the ellipse, if not provided the parent objects width will be used
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started</remarks>
    Public Property Width() As Double

    ''' <summary>
    ''' The height of the ellipse, if not provided the parent objects width will be used
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started</remarks>
    Public Property Height() As Double
    
    Protected Overrides Sub CreatePhysicalObject(Element As UIElement)
        If TypeOf Element Is FrameworkElement Then
            Dim FElement As FrameworkElement = Element
            If Width = 0 Then Width = FElement.Width
            If Height = 0 Then Height = FElement.Height
        End If

        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Box.PointToMeter(Canvas.GetLeft(Element) + Width / 2, Canvas.GetTop(Element) + Height / 2)
        
        If Geometries.Count = 0 Then
            Geometries.Add(New EllipseGeometry)
        End If
    End Sub
    
    Protected Overrides Sub CreateTransforms(Element As UIElement)
        MyBase.CreateTransforms(Element)
        
        Rotate.CenterX = Width / 2
        Rotate.CenterY = Height / 2
    End Sub
    
End Class