'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class RectangleGeometry
    Inherits PhysicalGeometry
    
    Dim LLeft As Double
    Public Property Left() As Double
        Get
            Return LLeft
        End Get
        Set
            LLeft = Value
        End Set
    End Property

    Dim LTop As Double
    Public Property Top() As Double
        Get
            Return LTop
        End Get
        Set
            LTop = Value
        End Set
    End Property

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
    
    Protected Overrides Sub CreateGeom(Body As PhysicalBody)
        If TypeOf Body Is RectangleBody Then
            Dim RBody As RectangleBody = Body
            If Width = 0 Then Width = RBody.Width
            If Height = 0 Then Height = RBody.Height
        End If
        
        Dim Vertices = FarseerPhysics.Common.PolygonTools.CreateRectangle(Box.PixelToMeter(Width / 2), Box.PixelToMeter(Height / 2))

        For Each Point In Vertices
            Point.X = Point.X + Left
            Point.Y = Point.Y + Top
        Next

        Geom = New FarseerPhysics.Collision.Shapes.PolygonShape(Vertices, 1)
    End Sub
    
End Class