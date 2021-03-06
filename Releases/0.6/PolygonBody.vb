﻿'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A <c>PhysicalBody</c> that will create a <c>PolygonGeometry</c> if no geometry is given
''' </summary>
Public Class PolygonBody
    Inherits PhysicalBody
    
    ''' <summary>
    ''' Gets or sets the points for the geometry. If none are given and this is placed in a <c>Polygon</c> the points will be added automatically.
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Points() As New PointCollection
    
    Protected Overrides Sub CreatePhysicalObject(Element As System.Windows.UIElement)
        If TypeOf Element Is Polygon Then
            If Points.Count = 0 Then
                For Each Point In CType(Element, Polygon).Points
                    Points.Add(Point)
                Next
            End If
        End If
        
        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Box.PointToMeter(Canvas.GetLeft(Element), Canvas.GetTop(Element))
        
        If Geometries.Count = 0 Then
            Geometries.Add(New PolygonGeometry)
        End If
    End Sub
    
End Class