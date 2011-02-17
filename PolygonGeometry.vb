'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class PolygonGeometry
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
        If TypeOf Body Is PolygonBody Then
            If Points.Count = 0 Then
                For Each Point In CType(Body, PolygonBody).Points
                    Points.Add(Point)
                Next
            End If
        End If
        
        Dim PhysicalPoints As New List(Of Microsoft.Xna.Framework.Vector2)
        For Each Point In Points
            PhysicalPoints.Add(Box.PointToMeter(Point.X - Body.Rotate.CenterX, Point.Y - Body.Rotate.CenterY))
        Next
        
        Dim Vertices As New FarseerPhysics.Common.Vertices(PhysicalPoints.ToArray)
        Geom = New FarseerPhysics.Collision.Shapes.PolygonShape(Vertices, 1)
    End Sub
    
End Class