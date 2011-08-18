'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A line segment (edge) Shape. These can be connected in chains or loops to other edge Shapes.
''' </summary>
''' <remarks>
''' Edges can not collide with other edges.
''' </remarks>
Public Class EdgeGeomerty
    Inherits PhysicalGeometry

    Dim LVertex0 As New Point
    ''' <summary>
    ''' Vertex 0 of the edge, this can't be changed after the simulation is started
    ''' </summary>
    Public Property Vertex0() As Point
        Get
            Return LVertex0
        End Get
        Set
            LVertex0 = Value
            HasVertex0 = True
        End Set
    End Property
    
    ''' <summary>
    ''' Vertex 1 of the edge, this can't be changed after the simulation is started
    ''' </summary>
    Public Property Vertex1() As Point
    
    ''' <summary>
    ''' Vertex 2 of the edge, this can't be changed after the simulation is started
    ''' </summary>
    Public Property Vertex2() As Point
    
    Dim LVertex3 As New Point
    ''' <summary>
    ''' Vertex 3 of the edge, this can't be changed after the simulation is started
    ''' </summary>
    Public Property Vertex3() As Point
        Get
            Return LVertex3
        End Get
        Set
            LVertex3 = Value
            HasVertex3 = True
        End Set
    End Property

    ''' <summary>
    ''' True to use Vertex0
    ''' </summary>
    Public Property HasVertex0() As Boolean
    
    ''' <summary>
    ''' True to use Vertex3
    ''' </summary>
    Public Property HasVertex3() As Boolean
    
    Protected Overrides Sub CreateGeom()
        Dim Edge As New FarseerPhysics.Collision.Shapes.EdgeShape(Microsoft.Xna.Framework.Vector2.Zero, Microsoft.Xna.Framework.Vector2.Zero)
        
        Edge.Vertex0 = Box.PointToMeter(Vertex0.X, Vertex0.Y)
        Edge.Vertex1 = Box.PointToMeter(Vertex1.X, Vertex1.Y)
        Edge.Vertex2 = Box.PointToMeter(Vertex2.X, Vertex2.Y)
        Edge.Vertex3 = Box.PointToMeter(Vertex3.X, Vertex3.Y)
        Edge.HasVertex0 = HasVertex0
        Edge.HasVertex3 = HasVertex3
        
        Geom = Edge
    End Sub
    
End Class