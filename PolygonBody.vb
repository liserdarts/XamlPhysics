'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class PolygonBody
    Inherits PhysicalBody
    
    Dim LPoints As New PointCollection
    Public Property Points() As PointCollection
        Get
            Return LPoints
        End Get
        Set
            LPoints = Value
        End Set
    End Property
    
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