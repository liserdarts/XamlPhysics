'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Constrains two points on two bodies
''' to remain at a fixed distance from each other. You can view
''' this as a massless, rigid rod.
''' </summary>
Public Class SliderJoint
    Inherits DoubleJoint
    
    ''' <summary>
    ''' The minimum distance between the two bodies
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Min() As Double
    
    ''' <summary>
    ''' The maximum distance between the two bodies
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Max() As Double
    
    ''' <summary>
    ''' The point attached to the first body
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Anchor1() As Point
    
    ''' <summary>
    ''' The point attached to the second body
    ''' </summary>
    Public Property Anchor2() As Point
    
    ''' <summary>
    ''' Gets the joint object from the Farseer Physics Engine
    ''' </summary>
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