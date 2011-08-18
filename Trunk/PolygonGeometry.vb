'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Creates a <c>PolygonShape</c> for the Farseer Physics Engine
''' </summary>
Public Class PolygonGeometry
    Inherits PhysicalGeometry
    
    ''' <summary>
    ''' The points of the shape. If none are given and the body is a <c>PolygonBody</c> they will be added automatically.
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Points() As New PointCollection
    
    Protected Overrides Sub CreateGeom()
        Dim PhysicalPoints As New List(Of Microsoft.Xna.Framework.Vector2)
        For Each Point In Points
            PhysicalPoints.Add(Box.PointToMeter(Point.X - Body.Rotate.CenterX, Point.Y - Body.Rotate.CenterY))
        Next
        
        Dim Vertices As New FarseerPhysics.Common.Vertices(PhysicalPoints.ToArray)
        Geom = New FarseerPhysics.Collision.Shapes.PolygonShape(Vertices, 1)
    End Sub
    
End Class