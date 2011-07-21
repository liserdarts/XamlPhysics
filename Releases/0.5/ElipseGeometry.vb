'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Creates a polygon in the shape of an ellipse using the given number of edges
''' </summary>
Public Class EllipseGeometry
    Inherits PhysicalGeometry
    
    ''' <summary>
    ''' The width of the ellipse, this can't be changed after the simulation is started
    ''' </summary>
    Public Property Width() As Double

    ''' <summary>
    ''' The height of the ellipse, this can't be changed after the simulation is started
    ''' </summary>
    Public Property Height() As Double

    ''' <summary>
    ''' The number of edges to create the ellipse, this can't be changed after the simulation is started
    ''' </summary>
    Public Property NumberOfEdges() As Integer = 20
    
    Protected Overrides Sub CreateGeom(Body As PhysicalBody)
        If TypeOf Body Is EllipseBody Then
            Dim EBody As EllipseBody = Body
            If Width = 0 Then Width = EBody.Width
            If Height = 0 Then Height = EBody.Height
        End If
        
        Dim Vertices = FarseerPhysics.Common.PolygonTools.CreateEllipse(Box.PixelToMeter(Width / 2), Box.PixelToMeter(Height / 2), NumberOfEdges)
        Geom = New FarseerPhysics.Collision.Shapes.PolygonShape(Vertices, 1)
    End Sub
    
End Class