Public Class CircleGeometry
    Inherits PhysicalGeometry
    
    Public Property Radius() As Double
    Public Property Position() As Point

    Protected Overrides Sub CreateGeom(Body As PhysicalBody)
        Dim Shape = New FarseerPhysics.Collision.Shapes.CircleShape(Box.PixelToMeter(Radius), 1)
        Shape.Position = Box.PointToMeter(Position)
        Geom = Shape
    End Sub
    
End Class