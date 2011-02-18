'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' The base class for all joints
''' </summary>
Public MustInherit Class PhysicalJoint
    Inherits FrameworkElement
    
    Shared Sub New()
        BodyProperty = DependencyProperty.Register("Body", GetType(UIElement), GetType(PhysicalJoint), New PropertyMetadata(Nothing))
    End Sub
    
    Event Broke As EventHandler
    Protected Overridable Sub OnBroke()
        RaiseEvent Broke(Me, EventArgs.Empty)
    End Sub
    
    ''' <summary>
    ''' The PhysicalBox this joint is in
    ''' </summary>
    Public Property Box() As PhysicalBox

    Public Shared BodyProperty As DependencyProperty
    ''' <summary>
    ''' Gets or Sets the body this joint is attached to
    ''' </summary>
    ''' <remarks>This can't be changed after the simulation is started.</remarks>
    Public Property Body() As UIElement
        Get
            Return GetValue(BodyProperty)
        End Get
        Set
            SetValue(BodyProperty, Value)
        End Set
    End Property
    
    ''' <summary>
    ''' Gets or sets the linear breakpoint.
    ''' </summary>
    Public Property LinearBreakpoint() As Double = Single.MaxValue

    ''' <summary>
    ''' Gets or sets the torque breakpoint.
    ''' </summary>
    Public Property TorqueBreakpoint() As Double = Single.MaxValue
    
    Dim WithEvents LJoint As FarseerPhysics.Dynamics.Joints.Joint
    ''' <summary>
    ''' Gets the joint object from the Farseer Physics Engine
    ''' </summary>
    Public Property Joint() As FarseerPhysics.Dynamics.Joints.Joint
        Get
            Return LJoint
        End Get
        Set
            LJoint = Value
        End Set
    End Property

    Dim LEnabled As Boolean = True
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="PhysicalJoint" /> is enabled.
    ''' </summary>
    ''' <value><c>True</c> if enabled; otherwise, <c>False</c>.</value>
    Public Property Enabled() As Boolean
        Get
            Return LEnabled
        End Get
        Set
            LEnabled = Value
            If Joint Is Nothing Then Return
            Joint.Enabled = Value
        End Set
    End Property
    
    ''' <summary>
    ''' Creates the joint object
    ''' </summary>
    Public Overridable Sub Initialize()
        CreateJoint
        SetProperties
        If Enabled Then Box.World.AddJoint(Joint)
    End Sub
    
    Protected MustOverride Sub CreateJoint()
    
    Protected Overridable Sub SetProperties()
        Joint.Enabled = Enabled
    End Sub

    ''' <summary>
    ''' Calculates if the joint should brak or not
    ''' </summary>
    ''' <param name="Interval">The time passed sense the last update.</param>
    Public Overridable Sub Update(Interval As TimeSpan)
        If Not Enabled Then Return

        Static LastInterval As TimeSpan
        Static LastLinearBreakpoint As Double
        Static LastTorqueBreakpoint As Double
        If LastInterval <> Interval Then
            Dim Multi = (Interval.TotalMilliseconds / 5)
            LastLinearBreakpoint = LinearBreakpoint * Multi
            LastTorqueBreakpoint = TorqueBreakpoint * Multi
            LastInterval = Interval
        End If

        Dim Break As Boolean
        If Joint.GetReactionForce(1).Length > LastLinearBreakpoint Then
            Break = True
        End If
        If Joint.GetReactionTorque(1) > LastTorqueBreakpoint Then
            Break = True
        End If

        If Break Then
            Box.World.RemoveJoint(Joint)
            Enabled = False
            Dispatcher.BeginInvoke(AddressOf OnBroke)
        End If
    End Sub

    Public Overridable Sub UpdateUI(Interval As TimeSpan)
    End Sub
End Class