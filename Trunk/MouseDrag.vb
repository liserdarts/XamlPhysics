'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Catches mouse events and allows dragging and dropping bodies
''' </summary>
Public Class MouseDrag
    Inherits Canvas
    
    ''' <summary>
    ''' The maximum constraint force that can be exerted to move the candidate body.
    ''' </summary>
    Public Property MaxForce() As Double = 100
    
    ''' <summary>
    ''' The response speed.
    ''' </summary>
    Public Property Frequency() As Double = 5
    
    Public Property Breakpoint() As Double = Single.MaxValue

    ''' <summary>
    ''' The damping ratio. 0 = no damping, 1 = critical damping.
    ''' </summary>
    Public Property DampingRatio() As Double = 0.7

    
    Dim SelectedBody As PhysicalBody
    Dim Joint As FarseerPhysics.Dynamics.Joints.FixedMouseJoint
    
    Private Sub MouseDrag_MouseLeftButtonDown(sender As Object, e As Windows.Input.MouseButtonEventArgs) Handles Me.MouseLeftButtonDown
        If SelectedBody IsNot Nothing Then Return

        SelectedBody = PhysicalBox.GetBody(e.OriginalSource)
        If SelectedBody Is Nothing Then Return
        If SelectedBody.Body Is Nothing Then
            SelectedBody = Nothing
            Return
        End If
        
        Dim MousePoint = e.GetPosition(SelectedBody.Box)
        Joint = New FarseerPhysics.Dynamics.Joints.FixedMouseJoint(SelectedBody.Body, SelectedBody.Box.PointToMeter(MousePoint))
        Joint.MaxForce = MaxForce
        Joint.Frequency = Frequency
        Joint.Breakpoint = Breakpoint
        Joint.DampingRatio = DampingRatio
        SelectedBody.Box.World.AddJoint(Joint)
        
        e.Handled = True
        CaptureMouse
    End Sub

    Private Sub MouseDrag_MouseLeftButtonUp(sender As Object, e As Windows.Input.MouseButtonEventArgs) Handles Me.MouseLeftButtonUp
        If SelectedBody Is Nothing Then Return
        e.Handled = True
        ReleaseMouseCapture
        
        SelectedBody.Box.World.RemoveJoint(Joint)
        SelectedBody = Nothing
        Joint = Nothing
    End Sub

    Private Sub MouseDrag_MouseMove(sender As Object, e As Windows.Input.MouseEventArgs) Handles Me.MouseMove
        If SelectedBody Is Nothing Then Return
        
        Dim MousePoint = e.GetPosition(SelectedBody.Box)
        Joint.WorldAnchorB = SelectedBody.Box.PointToMeter(MousePoint)
    End Sub
End Class