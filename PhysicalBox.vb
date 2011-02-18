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
        World = New FarseerPhysics.Dynamics.World(New Microsoft.Xna.Framework.Vector2(0, 1.42))
        FarseerPhysics.Settings.EnableDiagnostics = False
        FarseerPhysics.Settings.ContinuousPhysics = False
        FarseerPhysics.Settings.VelocityIterations = 8
        FarseerPhysics.Settings.PositionIterations = 8
    End Sub
    
    ''' <summary>
    ''' The world object from the Farseer Physics Engine.
    ''' </summary>
    Public Property World As FarseerPhysics.Dynamics.World

    ''' <summary>
    ''' Gets or sets the amount of pixels in A meter.
    ''' </summary>
    Public Property PixelsInAMeter As Double = 350

    Dim Bodies As List(Of PhysicalBody)
    Dim Elements As New Dictionary(Of Object, UIElement)
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
            FindGeometries(Me)

            Joints = New List(Of PhysicalJoint)
            FindJoints(Me)

            Update(Interval)
        End If

        For Each Body In Bodies
            Body.UpdateUI(Elements(Body))
        Next

        For Each Joint In Joints
            Joint.UpdateUI(Interval)
        Next
    End Sub
    
    Private Sub FindGeometries(Parent As DependencyObject)
        For I As Integer = 0 To VisualTreeHelper.GetChildrenCount(Parent) - 1
            Dim Child As DependencyObject = VisualTreeHelper.GetChild(Parent, I)
            
            If TypeOf Child Is UIElement Then
                Dim Body As PhysicalBody = GetBody(Child)
                If Body IsNot Nothing Then
                    
                    Body.Box = Me
                    Body.Initialize(Child)
                    
                    Bodies.Add(Body)
                    Elements.Add(Body, Child)
                    For Each Geom In Body.Geometries
                        Geom.Box = Me
                        Geom.Initialize(Body)
                    Next
                End If
            End If
            
            FindGeometries(Child)
        Next
    End Sub
    Private Sub FindJoints(Parent As DependencyObject)
        For I As Integer = 0 To VisualTreeHelper.GetChildrenCount(Parent) - 1
            Dim Child As DependencyObject = VisualTreeHelper.GetChild(Parent, I)
            
            If TypeOf Child Is PhysicalJoint Then
                Dim Joint As PhysicalJoint = Child
                
                Joint.Box = Me
                Joint.Initialize
                joints.Add(Child)
            End If
            
            FindJoints(Child)
        Next
    End Sub
    
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