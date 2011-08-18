'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A loop Shape is a free form sequence of line segments that form a circular list.
''' The loop may cross upon itself, but this is not recommended for smooth collision.
''' The loop has double sided collision, so you can use inside and outside collision.
''' Therefore, you may use any winding order.
''' </summary>
Public Class LoopGeomerty
    Inherits PhysicalGeometry
    
    ''' <summary>
    ''' The points in the loop, this can't be changed after the simulation is started
    ''' </summary>
    Public Property Points() As New PointCollection
    
    Protected Overrides Sub CreateGeom()
        Dim PhysicalPoints As New List(Of Microsoft.Xna.Framework.Vector2)
        For Each Point In Points
            PhysicalPoints.Add(Box.PointToMeter(Point.X, Point.Y))
        Next

        Dim Vertices As New FarseerPhysics.Common.Vertices(PhysicalPoints)
        Dim Shape As New FarseerPhysics.Collision.Shapes.LoopShape(Vertices, 1)

        Geom = Shape
    End Sub
End Class