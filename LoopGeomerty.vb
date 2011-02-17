'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class LoopGeomerty
    Inherits PhysicalGeometry
    
    Dim LPoints As New PointCollection
    Public Property Points() As PointCollection
        Get
            Return LPoints
        End Get
        Set
            LPoints = Value
        End Set
    End Property
    
    Protected Overrides Sub CreateGeom(Body As PhysicalBody)
        Dim PhysicalPoints As New List(Of Microsoft.Xna.Framework.Vector2)
        For Each Point In Points
            PhysicalPoints.Add(Box.PointToMeter(Point.X, Point.Y))
        Next

        Dim Vertices As New FarseerPhysics.Common.Vertices(PhysicalPoints)
        Dim Shape As New FarseerPhysics.Collision.Shapes.LoopShape(Vertices, 1)

        Geom = Shape
    End Sub
End Class