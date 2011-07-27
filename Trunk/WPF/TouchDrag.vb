'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'You may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Catches touch events and allows dragging and dropping bodies
''' </summary>
''' <remarks>
''' Rewrite for Touch by Jesper Johansen
''' </remarks>
Public Class TouchDrag
    Inherits Canvas

    Private Structure Connection
        Dim Body As PhysicalBody
        Dim MouseJoint As FarseerPhysics.Dynamics.Joints.FixedMouseJoint
        Dim AngleJoint As FarseerPhysics.Dynamics.Joints.FixedAngleJoint
    End Structure

    ''' <summary>
    ''' The maximum constraint force that can be exerted to move the candidate body.
    ''' </summary>
    Public Property MaxForce() As Double = 100

    ''' <summary>
    ''' The response speed.
    ''' </summary>
    Public Property Frequency() As Double = 5

    ''' <summary>
    ''' The damping ratio. 0 = no damping, 1 = critical damping.
    ''' </summary>
    Public Property DampingRatio() As Double = 0.7

    Public Property LinearBreakpoint() As Double = Single.MaxValue

    Public Property TorqueBreakpoint() As Double = Single.MaxValue

    ''' <summary>
    ''' If true, a FixedAngleJoint will be created to stop rotation of the touched body
    ''' </summary>
    Public Property StopRotaion() As Boolean = True


    Dim Connections As New Dictionary(Of Integer, Connection)()

    Private Sub TouchDrag_Down(sender As Object, e As Windows.Input.TouchEventArgs) Handles Me.TouchDown
        If Connections.ContainsKey(e.TouchDevice.Id) Then Return

        Dim Body = PhysicalBox.GetBody(e.OriginalSource)
        If Body Is Nothing Then Return
        If Body.Body Is Nothing Then Return

        Dim TouchPoint = e.TouchDevice.GetTouchPoint(Body.Box)
        
        Dim MouseJoint As New FarseerPhysics.Dynamics.Joints.FixedMouseJoint(Body.Body, Body.Box.PointToMeter(TouchPoint.Position))
        MouseJoint.MaxForce = MaxForce
        MouseJoint.Frequency = Frequency
        MouseJoint.Breakpoint = LinearBreakpoint
        MouseJoint.DampingRatio = DampingRatio
        Body.Box.World.AddJoint(MouseJoint)
        
        Dim AngleJoint As New FarseerPhysics.Dynamics.Joints.FixedAngleJoint(Body.Body)
        AngleJoint.TargetAngle = Body.Body.Rotation
        AngleJoint.Breakpoint = TorqueBreakpoint
        If StopRotaion Then
            Body.Box.World.AddJoint(AngleJoint)
        End If

        Dim Connection As Connection
        Connection.Body = Body
        Connection.MouseJoint = MouseJoint
        Connection.AngleJoint = AngleJoint
        Connections.Add(e.TouchDevice.Id, Connection)
        
        e.Handled = True
        CaptureTouch(e.TouchDevice)
    End Sub

    Private Sub TouchDrag_Up(sender As Object, e As Windows.Input.TouchEventArgs) Handles Me.TouchUp
        If Not Connections.ContainsKey(e.TouchDevice.Id) Then Return

        Dim Connection = Connections(e.TouchDevice.Id)
        Connections.Remove(e.TouchDevice.Id)

        Connection.Body.Box.World.RemoveJoint(Connection.MouseJoint)
        Connection.Body.Box.World.RemoveJoint(Connection.AngleJoint)
        
        e.Handled = True
        ReleaseTouchCapture(e.TouchDevice)
    End Sub

    Private Sub TouchDrag_Move(sender As Object, e As Windows.Input.TouchEventArgs) Handles Me.TouchMove
        If Not Connections.ContainsKey(e.TouchDevice.Id) Then Return
        
        Dim Connection = Connections(e.TouchDevice.Id)
        
        Dim TouchPoint = e.TouchDevice.GetTouchPoint(Connection.Body.Box)
        Connection.MouseJoint.WorldAnchorB = Connection.Body.Box.PointToMeter(TouchPoint.Position)

        Debug.WriteLine(TouchPoint.Position)
    End Sub

End Class