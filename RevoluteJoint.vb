'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class RevoluteJoint
    Inherits DoubleJoint
    
    Public Shadows Property Joint() As FarseerPhysics.Dynamics.Joints.RevoluteJoint
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
        Dim AnchorB = Box.PointToMeter(Canvas.GetLeft(Me), Canvas.GetTop(Me))
        
        AnchorB = BodyB.GetLocalPoint(AnchorB)
        Joint = FarseerPhysics.Factories.JointFactory.CreateRevoluteJoint(BodyA, BodyB, AnchorB)
    End Sub
    
End Class