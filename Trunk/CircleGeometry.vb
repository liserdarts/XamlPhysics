'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A perfect circle shape
''' </summary>
'''<remarks>
''' Unlike an ellipse that is made of a polygon, a circle is a perfect circle
''' </remarks>
Public Class CircleGeometry
    Inherits PhysicalGeometry
    
    ''' <summary>
    ''' Gets or sets the radius.
    ''' </summary>
    ''' <value>The radius.</value>
    Public Property Radius() As Double

    ''' <summary>
    ''' Gets or sets the position relative to the body.
    ''' </summary>
    ''' <value>The position.</value>
    Public Property Position() As Point

    Protected Overrides Sub CreateGeom(Body As PhysicalBody)
        If TypeOf Body Is CircleBody Then
            Dim CBody As CircleBody = Body
            If Radius = 0 Then Radius = CBody.Radius
        End If

        Dim Shape = New FarseerPhysics.Collision.Shapes.CircleShape(Box.PixelToMeter(Radius), 1)
        Shape.Position = Box.PointToMeter(Position)
        
        Geom = Shape
    End Sub
    
End Class