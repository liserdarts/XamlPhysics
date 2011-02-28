'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A line joint provides two degrees of freedom: translation
''' along an axis fixed in body1 and rotation in the plane.
''' </summary>
Public Class LineJoint
    Inherits DoubleJoint
    
    ''' <summary>
    ''' The inner joint object from the Farseer Physics Engine
    ''' </summary>
    Public Shadows Property Joint() As FarseerPhysics.Dynamics.Joints.LineJoint
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
        Joint = FarseerPhysics.Factories.JointFactory.CreateLineJoint(BodyA, BodyB, AnchorB, New Microsoft.Xna.Framework.Vector2(0.1, 0.1))
        Joint.CollideConnected = True
        Joint.EnableLimit = True
        Joint.LowerLimit = Box.PixelToMeter(1)
        Joint.UpperLimit = Box.PixelToMeter(1)
    End Sub
    
End Class