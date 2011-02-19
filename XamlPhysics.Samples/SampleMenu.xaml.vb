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
        Samples.Add(New SampleInfo(GetType(Sample1)))
        Samples.Add(New SampleInfo(GetType(Sample2)))

        UxSamples.ItemsSource = Samples
    End Sub

End Class
