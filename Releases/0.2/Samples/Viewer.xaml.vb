Partial Public Class Viewer
    Inherits Page

    Public Sub New()
        InitializeComponent
    End Sub

    Dim WithEvents GameLoop As New GameLoop

    Protected Overrides Sub OnNavigatedTo(e As Windows.Navigation.NavigationEventArgs)
        Dim SampleType = Type.GetType(NavigationContext.QueryString("Sample"))
        Dim Sample = Activator.CreateInstance(SampleType)
        
        UxSampleContainer.Children.Add(Sample)

        GameLoop.Interval = TimeSpan.FromMilliseconds(20)
        GameLoop.Start

        MyBase.OnNavigatedTo(e)
    End Sub

    Protected Overrides Sub OnNavigatedFrom(e As Windows.Navigation.NavigationEventArgs)
        GameLoop.Stop(False)
        MyBase.OnNavigatedFrom(e)
    End Sub

    Private Sub GameLoop_Tick(sender As Object, e As GameLoop.TickEventArgs) Handles GameLoop.Tick
        Static Count As Integer = 0
        Count = Count + 1
        If Count > 10 Then
            Dispatcher.BeginInvoke(AddressOf Tick)
        End If
    End Sub
    Private Sub Tick()
        UxBox.World.Step(GameLoop.Interval.TotalSeconds)
        UxBox.Update(GameLoop.Interval)
        UxBox.UpdateUI(GameLoop.Interval)
    End Sub

End Class