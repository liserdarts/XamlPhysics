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
    
    Protected Overrides Sub CreateGeom()
        Dim Half = Box.PointToMeter(Width / 2, Height / 2)
        Dim Origin = Box.PointToMeter(Left, Top)
        Dim Vertices = FarseerPhysics.Common.PolygonTools.CreateRectangle(Half.X, Half.Y, Half + Origin, 0)

        Geom = New FarseerPhysics.Collision.Shapes.PolygonShape(Vertices, 1)
    End Sub
    
End Class