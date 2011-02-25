'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Maintains a fixed angle on one body
''' </summary>
Public Class FixedAngleJoint
    Inherits PhysicalJoint
    
    ''' <summary>
    ''' The angle to maintain, this can't be changed after the simulation is started
    ''' </summary>
    Public Property TargetAngle() As Double
    
    ''' <summary>
    ''' The maximum impulse to use to achieve the target angle, this can't be changed after the simulation is started
    ''' </summary>
    Public Property MaxImpulse() As Double = Single.MaxValue
    
    ''' <summary>
    ''' The inner joint object from the Farseer Physics Engine
    ''' </summary> 
    Public Shadows Property Joint() As FarseerPhysics.Dynamics.Joints.FixedAngleJoint
        Get
            Return MyBase.Joint
        End Get
        Set
            MyBase.Joint = Value
        End Set
    End Property
    
    Protected Overrides Sub CreateJoint()
        Joint = New FarseerPhysics.Dynamics.Joints.FixedAngleJoint(PhysicalBox.GetBody(Body).Body)
    End Sub
    
    Protected Overrides Sub SetProperties()
        MyBase.SetProperties
        
        Joint.TargetAngle = TargetAngle / 57.29578
        Joint.MaxImpulse = MaxImpulse
    End Sub
    
End Class