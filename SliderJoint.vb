'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class SliderJoint
    Inherits DoubleJoint
    
    Dim LMin As Double
    Public Property Min() As Double
        Get
            Return LMin
        End Get
        Set
            LMin = Value
        End Set
    End Property
    
    Dim LMax As Double
    Public Property Max() As Double
        Get
            Return LMax
        End Get
        Set
            LMax = Value
        End Set
    End Property
    
    Dim LAnchor1 As Point
    Public Property Anchor1() As Point
        Get
            Return LAnchor1
        End Get
        Set
            LAnchor1 = Value
        End Set
    End Property
    
    Dim LAnchor2 As Point
    Public Property Anchor2() As Point
        Get
            Return LAnchor2
        End Get
        Set
            LAnchor2 = Value
        End Set
    End Property
    
    Public Shadows Property Joint() As FarseerPhysics.Dynamics.Joints.SliderJoint
        Get
            Return MyBase.Joint
        End Get
        Set
            MyBase.Joint = Value
        End Set
    End Property
    
    Protected Overrides Sub CreateJoint()
        Dim BodyA = PhysicalBox.GetBody(Body).Body
        Dim BodyB = PhysicalBox.GetBody(Body2).Body
        Dim VectorA = Box.PointToMeter(Anchor1.X, Anchor1.Y)
        Dim VectorB = Box.PointToMeter(Anchor2.X, Anchor2.Y)

        Joint = New FarseerPhysics.Dynamics.Joints.SliderJoint(BodyA, BodyB, VectorA, VectorB, Min, Max)
        Joint.Frequency = 100
    End Sub
    
End Class