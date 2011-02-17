'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class EllipseBody
    Inherits PhysicalBody
    
    Dim LWidth As Double
    Public Property Width() As Double
        Get
            Return LWidth
        End Get
        Set
            LWidth = Value
        End Set
    End Property

    Dim LHeight As Double
    Public Property Height() As Double
        Get
            Return LHeight
        End Get
        Set
            LHeight = Value
        End Set
    End Property
    
    Protected Overrides Sub CreatePhysicalObject(Element As UIElement)
        If TypeOf Element Is FrameworkElement Then
            Dim FElement As FrameworkElement = Element
            If Width = 0 Then Width = FElement.Width
            If Height = 0 Then Height = FElement.Height
        End If

        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Box.PointToMeter(Canvas.GetLeft(Element) + Width / 2, Canvas.GetTop(Element) + Height / 2)
        
        If Geometries.Count = 0 Then
            Geometries.Add(New ElipseGeometry)
        End If
    End Sub
    
    Protected Overrides Sub CreateTransforms(Element As UIElement)
        MyBase.CreateTransforms(Element)
        
        Rotate.CenterX = Width / 2
        Rotate.CenterY = Height / 2
    End Sub
    
End Class