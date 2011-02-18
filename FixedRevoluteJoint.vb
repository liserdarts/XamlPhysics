'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' A revolute joint rains a body to share a common point with the word while it
''' is free to rotate about the point.
''' </summary>
Public Class FixedRevoluteJoint
    Inherits PhysicalJoint

    ''' <summary>
    ''' The inner joint object from the Farseer Physics Engine
    ''' </summary>
    Public Shadows Property Joint() As FarseerPhysics.Dynamics.Joints.FixedRevoluteJoint
        Get
            Return MyBase.Joint
        End Get
        Set
            MyBase.Joint = Value
        End Set
    End Property
    
    Protected Overrides Sub CreateJoint()
        Dim WorldAnchor = Box.PointToMeter(Canvas.GetLeft(Me), Canvas.GetTop(Me))
        Dim BodyAnchor As New Microsoft.Xna.Framework.Vector2

        Joint = New FarseerPhysics.Dynamics.Joints.FixedRevoluteJoint(PhysicalBox.GetBody(Body).Body, BodyAnchor, WorldAnchor)
    End Sub
    
End Class