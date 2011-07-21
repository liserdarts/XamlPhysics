Partial Public Class Viewer
    Inherits Page

    Public Sub New()
        InitializeComponent
    End Sub

    Public Shared AutoPlay As Boolean = True

    Protected Overrides Sub OnNavigatedTo(e As Windows.Navigation.NavigationEventArgs)
        [Stop]

        If AutoPlay Then
            Play
        End If

        MyBase.OnNavigatedTo(e)
    End Sub

    Protected Overrides Sub OnNavigatedFrom(e As Windows.Navigation.NavigationEventArgs)
        [Stop]
        MyBase.OnNavigatedFrom(e)
    End Sub

    Private Sub Play()
        UxBox.Clock.Start
        UxPlay.IsEnabled = False
        UxPause.IsEnabled = True
        UxStop.IsEnabled = True
    End Sub
    Private Sub Pause() Handles UxPause.Click
        UxBox.Clock.Stop(False)
        UxPlay.IsEnabled = True
        UxPause.IsEnabled = False
        UxStop.IsEnabled = True
    End Sub
    Private Sub [Stop]() Handles UxStop.Click
        UxBox.Clock.Stop(False)
        UxBox.Reset
        
        UxPlay.IsEnabled = True
        UxPause.IsEnabled = False
        UxStop.IsEnabled = False

        Dim SampleType = Type.GetType(NavigationContext.QueryString("Sample"))
        Dim Sample = Activator.CreateInstance(SampleType)
        
        UxSampleContainer.Children.Clear
        UxSampleContainer.Children.Add(Sample)
    End Sub

    Private Sub UxPlay_Click(sender As Object, e As Windows.RoutedEventArgs) Handles UxPlay.Click
        Play
    End Sub
    Private Sub UxPause_Click(sender As Object, e As Windows.RoutedEventArgs) Handles UxPause.Click
        Pause
    End Sub
    Private Sub UxStop_Click(sender As Object, e As Windows.RoutedEventArgs) Handles UxStop.Click
        [Stop]
    End Sub
End Class