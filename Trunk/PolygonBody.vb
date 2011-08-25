'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A <c>PhysicalBody</c> that will create a <c>PolygonGeometry</c> if no geometry is given
''' </summary>
Public Class PolygonBody
    Inherits SingleGeometryBody
    
    ''' <summary>
    ''' Gets or sets the points for the geometry. If none are given and this is placed in a <c>Polygon</c> the points will be added automatically.
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Points() As New PointCollection
    
    Protected Overrides Sub CreatePhysicalObject()
        If TypeOf Element Is Polygon Then
            If Points.Count = 0 Then
                For Each Point In CType(Element, Polygon).Points
                    Points.Add(Point)
                Next
            End If
        End If
        
        Dim Position = Box.PointToMeter(Canvas.GetLeft(Element), Canvas.GetTop(Element))
        If Single.IsNaN(Position.X) Then Position.X = 0
        If Single.IsNaN(Position.Y) Then Position.Y = 0
        
        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Position
        
        Dim PhysicalPoints As New FarseerPhysics.Common.Vertices
        For Each Point In Points
            PhysicalPoints.Add(New Microsoft.Xna.Framework.Vector2(Point.X, Point.Y))
        Next
        
        Dim Polygons = FarseerPhysics.Common.Decomposition.EarclipDecomposer.ConvexPartition(PhysicalPoints)
            
        For Each Poly In Polygons
            Dim Geom As New PolygonGeometry
            For Each Vertex In Poly
                Geom.Points.Add(New Point(Vertex.X, Vertex.Y))
            Next
            Geometries.Add(Geom)
        Next
    End Sub
    
End Class