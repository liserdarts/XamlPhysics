'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public MustInherit Class PhysicalBody
    Inherits DependencyObject
    
    ''' <summary>
    ''' Gets or sets a value indicating whether this body is static. This can't be changed after the simulation is started.
    ''' </summary>
    Public Property IsStatic() As Boolean

    ''' <summary>
    ''' Gets or sets a value indicating whether this body should be included in the CCD solver.
    ''' </summary>
    Public Property IsBullet() As Boolean

    ''' <summary>
    ''' Gets or sets the mass. Usually in kilograms (kg). This can't be changed after the simulation is started.
    ''' </summary>
    Public Property Mass() As Double

    ''' <summary>
    ''' Gets or sets a value indicating whether this body ignores gravity. This can't be changed after the simulation is started.
    ''' </summary>
    Public Property IgnoreGravity() As Boolean

    Dim LGeometries As New GeometryCollection
    ''' <summary>
    ''' Gets the GeometryCollection that holds the geometries of this body
    ''' </summary>
    Protected ReadOnly Property Geometries() As GeometryCollection
        Get
            Return LGeometries
        End Get
    End Property
    
    ''' <summary>
    ''' The <c>PhysicalBox</c> this body is in
    ''' </summary>
    Public Property Box() As PhysicalBox

    ''' <summary>
    ''' The <c>UIElement</c> this body is attached to
    ''' </summary>
    Public Property Element() As UIElement

    ''' <summary>
    ''' The <c>Body</c> object from the Farseer Physics Engine
    ''' </summary>
    Public Property Body() As FarseerPhysics.Dynamics.Body

    ''' <summary>
    ''' The <c>RotateTransform</c> used to rotate the <c>UIElement</c>
    ''' </summary>
    Public Property Rotate() As New RotateTransform
    
    ''' <summary>
    ''' Creates all the objects for the Farseer Physics Engine, and sets their properties
    ''' </summary>
    Public Sub Initialize(Element As UIElement)
        Me.Element = Element
        
        CreatePhysicalObject
        SetBodyProperties
        CreateTransforms
        InitializeGeometries
    End Sub
    
    Protected Overridable Sub CreatePhysicalObject()
        Dim Position = Box.PointToMeter(Canvas.GetLeft(Element), Canvas.GetTop(Element))
        If Single.IsNaN(Position.X) Then Position.X = 0
        If Single.IsNaN(Position.Y) Then Position.Y = 0

        Body = New FarseerPhysics.Dynamics.Body(Box.World)
        Body.Position = Position
    End Sub
    
    Protected Overridable Sub SetBodyProperties()
        If Not IsStatic Then
            Body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic
        End If
        Body.IgnoreGravity = IgnoreGravity
        Body.IsBullet = True
    End Sub
    
    Protected Overridable Sub CreateTransforms()
        Dim Transforms As New TransformGroup
        If Element.RenderTransform IsNot Nothing Then 'Keep the transforms that are already there
            Transforms.Children.Add(Element.RenderTransform)
        End If
        Element.RenderTransform = Transforms

        Rotate = New RotateTransform
        Transforms.Children.Add(Rotate)
    End Sub

    Protected Overridable Sub InitializeGeometries()
        For Each Geom In Geometries
            Geom.Box = Box
            Geom.Initialize(Me)
        Next
    End Sub
    
    Dim Position As Point
    Dim Angle As Double
    ''' <summary>
    ''' Calculates a new position and rotation for this body
    ''' </summary>
    Public Overridable Sub Update()
        If IsStatic Then Return
        If Mass > 0 Then Body.Mass = Mass
        Position = Box.MeterToPoint(Body.Position)
        Angle = Body.Rotation * 57.29578
    End Sub

    ''' <summary>
    ''' Moves the <c>UIElement</c> to the calculated position and rotation
    ''' </summary>
    Public Overridable Sub UpdateUI(Element As UIElement)
        If IsStatic Then Return
        Canvas.SetLeft(Element, Position.X - Rotate.CenterX)
        Canvas.SetTop(Element, Position.Y - Rotate.CenterY)
        Rotate.Angle = Angle
    End Sub
    
End Class