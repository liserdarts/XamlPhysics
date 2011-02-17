'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class ElipseGeometry
    Inherits PhysicalGeometry
    
    Dim LWidth As Double
    Public Property Width() As Double
        Get
            Return LWidth
        End Get
        Set
            LWidth = Value
        End Set
    End Property

    Dim LHeight As Double
    Public Property Height() As Double
        Get
            Return LHeight
        End Get
        Set
            LHeight = Value
        End Set
    End Property

    Dim LNumberOfEdges As Integer = 20
    Public Property NumberOfEdges() As Integer
        Get
            Return LNumberOfEdges
        End Get
        Set
            LNumberOfEdges = Value
        End Set
    End Property
    
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