'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class EdgeGeomerty
    Inherits PhysicalGeometry
    
    Dim LVertex0 As New Point
    Public Property Vertex0() As Point
        Get
            Return LVertex0
        End Get
        Set
            LVertex0 = Value
        End Set
    End Property
    
    Dim LVertex1 As New Point
    Public Property Vertex1() As Point
        Get
            Return LVertex1
        End Get
        Set
            LVertex1 = Value
            HasVertex0 = True
        End Set
    End Property
    
    Dim LVertex2 As New Point
    Public Property Vertex2() As Point
        Get
            Return LVertex2
        End Get
        Set
            LVertex2 = Value
        End Set
    End Property
    
    Dim LVertex3 As New Point
    Public Property Vertex3() As Point
        Get
            Return LVertex3
        End Get
        Set
            LVertex3 = Value
            HasVertex3 = True
        End Set
    End Property

    Dim LHasVertex0 As New Boolean
    Public Property HasVertex0() As Boolean
        Get
            Return LHasVertex0
        End Get
        Set
            LHasVertex0 = Value
        End Set
    End Property

    Dim LHasVertex3 As New Boolean
    Public Property HasVertex3() As Boolean
        Get
            Return LHasVertex3
        End Get
        Set
            LHasVertex3 = Value
        End Set
    End Property
    
    Protected Overrides Sub CreateGeom(Body As PhysicalBody)
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