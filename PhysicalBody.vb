'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class PhysicalBody
    
    Shared Sub New()
        GeometriesProperty = DependencyProperty.Register("Geometries", GetType(GeometryCollection), GetType(PhysicalBody), New PropertyMetadata(Nothing))
    End Sub

    Dim LIsStatic As Boolean
    Public Property IsStatic() As Boolean
        Get
            Return LIsStatic
        End Get
        Set
            LIsStatic = Value
        End Set
    End Property

    Dim LMass As Double = 1
    Public Property Mass() As Double
        Get
            Return LMass
        End Get
        Set
            LMass = Value
        End Set
    End Property

    Dim LIgnoreGravity As Boolean
    Public Property IgnoreGravity() As Boolean
        Get
            Return LIgnoreGravity
        End Get
        Set
            LIgnoreGravity = Value
        End Set
    End Property

    Public Shared GeometriesProperty As DependencyProperty
    Dim LGeometries As New GeometryCollection
    Public ReadOnly Property Geometries() As GeometryCollection
        Get
            Return LGeometries
        End Get
    End Property
    
    Public Box As PhysicalBox
    Public Body As FarseerPhysics.Dynamics.Body
    Public Rotate As New RotateTransform
    
    Public Sub Initialize(Element As UIElement)
        CreatePhysicalObject(Element)
        SetBodyProperties
        CreateTransforms(Element)
    End Sub
    
    Protected Overridable Sub CreatePhysicalObject(Element As UIElement)
        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = New Microsoft.Xna.Framework.Vector2(Canvas.GetLeft(Element), Canvas.GetTop(Element))
    End Sub
    
    Protected Overridable Sub SetBodyProperties()
        Body.Mass = Mass
        If Not IsStatic Then
            Body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic
        End If
        Body.IgnoreGravity = IgnoreGravity
    End Sub
    
    Protected Overridable Sub CreateTransforms(Element As UIElement)
        Dim Transforms As New TransformGroup
        If Element.RenderTransform IsNot Nothing Then 'Keep the transforms that are already there
            Transforms.Children.Add(Element.RenderTransform)
        End If
        Element.RenderTransform = Transforms

        Rotate = New RotateTransform
        Transforms.Children.Add(Rotate)
    End Sub
    
    Dim Position As Point
    Dim Angle As Double
    Public Overridable Sub Update()
        If IsStatic Then Return
        Position = Box.MeterToPoint(Body.Position)
        Angle = Body.Rotation * 57.29578
    End Sub

    Public Overridable Sub UpdateUI(Element As UIElement)
        If IsStatic Then Return
        Canvas.SetLeft(Element, Position.X - Rotate.CenterX)
        Canvas.SetTop(Element, Position.Y - Rotate.CenterY)
        Rotate.Angle = Angle
    End Sub
    
End Class