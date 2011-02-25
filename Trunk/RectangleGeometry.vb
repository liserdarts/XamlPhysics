'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Creates a <c>PolygonShape</c> for the Farseer Physics Engine
''' </summary>
Public Class RectangleGeometry
    Inherits PhysicalGeometry
    
    ''' <summary>
    ''' Gets or sets the left.
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Left() As Double

    ''' <summary>
    ''' Gets or sets the top.
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Top() As Double

    ''' <summary>
    ''' Gets or sets the width. If none is given and the body is a <c>RectangleBody</c> it will be added automatically.
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Width() As Double

    ''' <summary>
    ''' Gets or sets the height. If none is given and the body is a <c>RectangleBody</c> it will be added automatically.
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Height() As Double
    
    Protected Overrides Sub CreateGeom(Body As PhysicalBody)
        If TypeOf Body Is RectangleBody Then
            Dim RBody As RectangleBody = Body
            If Width = 0 Then Width = RBody.Width
            If Height = 0 Then Height = RBody.Height
        End If
        
        Dim Vertices = FarseerPhysics.Common.PolygonTools.CreateRectangle(Box.PixelToMeter(Width / 2), Box.PixelToMeter(Height / 2))

        For I As Integer = 0 To Vertices.Count - 1
            Dim Point = Vertices(I)
            Point.X = Point.X + Box.PixelToMeter(Left)
            Point.Y = Point.Y + Box.PixelToMeter(Top)
            Vertices(I) = Point
        Next

        Geom = New FarseerPhysics.Collision.Shapes.PolygonShape(Vertices, 1)
    End Sub
    
End Class