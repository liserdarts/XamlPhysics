Partial Public Class SampleMenu
    Inherits Page

    Public Class SampleInfo
        
        Public Sub New(Type As Type)
            Me.Type = Type
        End Sub

        Public Property Type As Type
        Public ReadOnly Property Name() As String
            Get
                Return Type.Name
            End Get
        End Property
        Public ReadOnly Property Path() As String
            Get
                Return "/Samples/" & Type.FullName
            End Get
        End Property
    End Class

    Public Sub New()
        InitializeComponent
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As Windows.Navigation.NavigationEventArgs)
        Dim Samples As New List(Of SampleInfo)
        Samples.Add(New SampleInfo(GetType(SimpleBodies)))
        Samples.Add(New SampleInfo(GetType(WeldJoint)))
        Samples.Add(New SampleInfo(GetType(RevoluteJoint)))
        Samples.Add(New SampleInfo(GetType(FixedRevoluteJoint)))
        Samples.Add(New SampleInfo(GetType(AngleJoint)))
        Samples.Add(New SampleInfo(GetType(FixedAngleJoint)))
        Samples.Add(New SampleInfo(GetType(LineJoint)))
        Samples.Add(New SampleInfo(GetType(Breakpoints)))
        Samples.Add(New SampleInfo(GetType(Polygons)))
        Samples.Add(New SampleInfo(GetType(Mass)))
        Samples.Add(New SampleInfo(GetType(Friction)))
        Samples.Add(New SampleInfo(GetType(Restitution)))
        Samples.Add(New SampleInfo(GetType(Controls)))
        Samples.Add(New SampleInfo(GetType(CollisionEvents)))

        UxSamples.ItemsSource = Samples
    End Sub

End Class
