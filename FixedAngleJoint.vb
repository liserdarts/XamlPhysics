'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class FixedAngleJoint
    Inherits PhysicalJoint
    
    Dim LTargetAngle As Double
    Public Property TargetAngle() As Double
        Get
            Return LTargetAngle
        End Get
        Set
            LTargetAngle = Value
        End Set
    End Property
    
    Dim LMaxImpulse As Double = Single.MaxValue
    Public Property MaxImpulse() As Double
        Get
            Return LMaxImpulse
        End Get
        Set
            LMaxImpulse = Value
        End Set
    End Property
     
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
        
        Joint.TargetAngle = TargetAngle * 57.29578
        Joint.MaxImpulse = MaxImpulse
    End Sub
    
End Class