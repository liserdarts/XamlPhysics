'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' The container of a physics simulation
''' </summary>
Public Class PhysicalBox
    Inherits Canvas
    
    Shared Sub New()
         BodyProperty = DependencyProperty.RegisterAttached("Body", GetType(PhysicalBody), GetType(PhysicalBox), _
            New PropertyMetadata(Nothing))
    End Sub
    
    Public Shared ReadOnly BodyProperty As DependencyProperty
    Public Shared Function GetBody(Element As UIElement) As PhysicalBody
        Return Element.GetValue(BodyProperty)
    End Function
    Public Shared Sub SetBody(Element As UIElement, Value As PhysicalBody)
        Element.SetValue(BodyProperty, Value)
    End Sub
 
    Public Sub New()
        Reset
    End Sub
    
    ''' <summary>
    ''' The world object from the Farseer Physics Engine.
    ''' </summary>
    Public Property World() As FarseerPhysics.Dynamics.World

    ''' <summary>
    ''' Enable/Disable Continuous Collision Detection (CCD)
    ''' </summary>
    Public Property ContinuousPhysics() As Boolean = True
    
    ''' <summary>
    ''' Gets or sets the amount of pixels in A meter.
    ''' </summary>
    Public Property PixelsInAMeter() As Double = 350

    Dim LGravity As New Point(0, 1.42)
    Public Property Gravity() As Point
        Get
            If World Is Nothing Then
                Return LGravity
            Else
                Return New Point(World.Gravity.X, World.Gravity.Y)
            End If
        End Get
        Set
            If World Is Nothing Then
                LGravity = Value
            Else
                World.Gravity = New Microsoft.Xna.Framework.Vector2(Value.X, Value.Y)
            End If
        End Set
    End Property

    Dim WithEvents LClock As GameLoop
    ''' <summary>
    ''' Gets or sets the clock.
    ''' </summary>
    Public Property Clock() As GameLoop
        Get
            Return LClock
        End Get
        Set
            LClock = Value
        End Set
    End Property
    
    Private Sub Clock_Tick(sender As Object, e As GameLoop.TickEventArgs) Handles LClock.Tick
        World.Step(LClock.Interval.TotalSeconds)
    End Sub

    Dim UIResetEvent As New System.Threading.ManualResetEvent(False)
    Private Sub Clock_UITick(sender As Object, e As GameLoop.TickEventArgs) Handles LClock.UITick
        Update(e.TimeElapsed)
        
        UIResetEvent.Reset
        Dispatcher.BeginInvoke(New Action(AddressOf Clock_UITick))
        UIResetEvent.WaitOne
    End Sub
    Private Sub Clock_UITick()
        If LClock.IsRunning Then
            Dim Interval As New TimeSpan(LClock.Interval.Ticks * LClock.UISteps)
            UpdateUI(Interval)
        End If
        UIResetEvent.Set
    End Sub


    Dim Bodies As List(Of PhysicalBody)
    Dim Elements As New Dictionary(Of PhysicalBody, UIElement)
    Dim Joints As List(Of PhysicalJoint)
    
    ''' <summary>
    ''' Recalculates the position and rotation of each body in the simulation.
    ''' </summary>
    Public Sub Update(Interval As TimeSpan)
        If Bodies Is Nothing Then Return
        
        For Each Body In Bodies
            Body.Update
        Next

        For Each Body In Joints
            Body.Update(Interval)
        Next
    End Sub

    ''' <summary>
    ''' Moves each body to its calculated location.
    ''' </summary>
    Public Sub UpdateUI(Interval As TimeSpan)
        If Bodies Is Nothing Then 'Find all children with a PhysicalBody
            Bodies = New List(Of PhysicalBody)
            Joints = New List(Of PhysicalJoint)
            FindNewBodies
        End If

        For Each Body In Bodies
            Body.UpdateUI(Elements(Body))
        Next

        For Each Joint In Joints
            Joint.UpdateUI(Interval)
        Next
    End Sub
    
    ''' <summary>
    ''' Erases all data and restarts the simulation
    ''' </summary>
    ''' <remarks>Be sure any thread calling Update or UpdateUI has been stopped</remarks>
    Public Sub Reset()
        World = New FarseerPhysics.Dynamics.World(New Microsoft.Xna.Framework.Vector2(Gravity.X, Gravity.Y))
        FarseerPhysics.Settings.EnableDiagnostics = False
        FarseerPhysics.Settings.ContinuousPhysics = ContinuousPhysics
        FarseerPhysics.Settings.VelocityIterations = 8
        FarseerPhysics.Settings.PositionIterations = 8

        Bodies = Nothing
        Elements.Clear
        Joints = Nothing
    End Sub

    ''' <summary>
    ''' Adds any new bodies to the simulation that were added after it was started
    ''' </summary>
    Public Sub FindNewBodies()
        If Bodies Is Nothing Then Return
        
        FindBodies(Me)
        FindJoints(Me)
        'Prevent the bodies from being placed at the origin for a frame
        Update(TimeSpan.FromSeconds(1))
    End Sub

    Private Sub FindBodies(Parent As DependencyObject)
        For I As Integer = 0 To VisualTreeHelper.GetChildrenCount(Parent) - 1
            Dim Child As DependencyObject = VisualTreeHelper.GetChild(Parent, I)
            
            If TypeOf Child Is UIElement Then
                Dim Body As PhysicalBody = GetBody(Child)
                If Body IsNot Nothing Then
                    If Not Bodies.Contains(Body) Then
                        Bodies.Add(Body)
                        
                        Body.Box = Me
                        Body.Initialize(Child)

                        Elements.Add(Body, Child)
                        For Each Geom In Body.Geometries
                            Geom.Box = Me
                            Geom.Initialize(Body)
                        Next
                    End If
                End If
            End If
            
            FindBodies(Child)
        Next
    End Sub
    Private Sub FindJoints(Parent As DependencyObject)
        For I As Integer = 0 To VisualTreeHelper.GetChildrenCount(Parent) - 1
            Dim Child As DependencyObject = VisualTreeHelper.GetChild(Parent, I)
            
            If TypeOf Child Is PhysicalJoint Then
                Dim Joint As PhysicalJoint = Child
                If Not Joints.Contains(Joint) Then
                    Joints.Add(Child)

                    Joint.Box = Me
                    Joint.Initialize
                End If
            End If
            
            FindJoints(Child)
        Next
    End Sub
    
    ''' <summary>
    ''' Searches the elements for one that matches the Predicate
    ''' </summary>
    Public Function FindElement(Match As Predicate(Of PhysicalBody)) As UIElement
        If Elements IsNot Nothing Then
            For Each PhysicalBody In Elements
                If Match(PhysicalBody.Key) Then
                    Return PhysicalBody.Value
                End If
            Next
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Converts a measurement in pixels to meters
    ''' </summary>
    Public Function PixelToMeter(X As Single) As Single
        Return X / PixelsInAMeter
    End Function
    ''' <summary>
    ''' Converts a point in pixels to meters
    ''' </summary>
    Public Function PointToMeter(X As Single, Y As Single) As Microsoft.Xna.Framework.Vector2
        Return New Microsoft.Xna.Framework.Vector2(X / PixelsInAMeter, Y / PixelsInAMeter)
    End Function
    ''' <summary>
    ''' Converts a point in pixels to meters
    ''' </summary>
    Public Function PointToMeter(Point As Point) As Microsoft.Xna.Framework.Vector2
        Return New Microsoft.Xna.Framework.Vector2(Point.X / PixelsInAMeter, Point.Y / PixelsInAMeter)
    End Function

    ''' <summary>
    ''' Converts a measurement in meters to pixels
    ''' </summary>
    Public Function MeterToPixel(X As Single) As Double
        Return X * PixelsInAMeter
    End Function
    ''' <summary>
    ''' Converts a point in meters to pixels
    ''' </summary>
    Public Function MeterToPoint(X As Single, Y As Single) As Point
        Return New Point(X * PixelsInAMeter, Y * PixelsInAMeter)
    End Function
    ''' <summary>
    ''' Converts a point in meters to pixels
    ''' </summary>
    Public Function MeterToPoint(Meter As Microsoft.Xna.Framework.Vector2) As Point
        Return New Point(Meter.X * PixelsInAMeter, Meter.Y * PixelsInAMeter)
    End Function
End Class