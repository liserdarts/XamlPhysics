'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public MustInherit Class PhysicalJoint
    Inherits FrameworkElement
    
    Shared Sub New()
        BodyProperty = DependencyProperty.Register("Body", GetType(UIElement), GetType(PhysicalJoint), New PropertyMetadata(Nothing))
    End Sub
    
    Event Broke As EventHandler
    Protected Overridable Sub OnBroke()
        RaiseEvent Broke(Me, EventArgs.Empty)
    End Sub
    
    Dim LBox As PhysicalBox
    Public Property Box() As PhysicalBox
        Get
            Return LBox
        End Get
        Set
            LBox = Value
        End Set
    End Property

    Public Shared BodyProperty As DependencyProperty
    Public Property Body() As UIElement
        Get
            Return GetValue(BodyProperty)
        End Get
        Set
            SetValue(BodyProperty, Value)
        End Set
    End Property
    
    Dim LSoftness As Double
    Public Property Softness() As Double
        Get
            Return LSoftness
        End Get
        Set
            LSoftness = Value
        End Set
    End Property
    
    Dim LLinearBreakpoint As Double = Single.MaxValue
    Public Property LinearBreakpoint() As Double
        Get
            Return LLinearBreakpoint
        End Get
        Set
            LLinearBreakpoint = Value
        End Set
    End Property

    Dim LTorqueBreakpoint As Double = Single.MaxValue
    Public Property TorqueBreakpoint() As Double
        Get
            Return LTorqueBreakpoint
        End Get
        Set
            LTorqueBreakpoint = Value
        End Set
    End Property
    
    Dim LBiasFactor As Double = 0.2
    Public Property BiasFactor() As Double
        Get
            Return LBiasFactor
        End Get
        Set
            LBiasFactor = Value
        End Set
    End Property
    
    Dim WithEvents LJoint As FarseerPhysics.Dynamics.Joints.Joint
    Public Property Joint() As FarseerPhysics.Dynamics.Joints.Joint
        Get
            Return LJoint
        End Get
        Set
            LJoint = Value
        End Set
    End Property

    Dim LEnabled As Boolean = True
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
    
    Public Overridable Sub Initialize()
        CreateJoint
        SetProperties
        If Enabled Then Box.World.AddJoint(Joint)
    End Sub
    
    Protected MustOverride Sub CreateJoint()
    
    Protected Overridable Sub SetProperties()
        Joint.Enabled = Enabled
    End Sub

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