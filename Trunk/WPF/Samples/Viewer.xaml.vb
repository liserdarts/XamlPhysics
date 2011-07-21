Public Class Viewer
    
    Public Sub New()
        InitializeComponent
        
        AddSample
    End Sub

    Private Sub AddSample()
        Dim Sample = Activator.CreateInstance(GetType(SimpleBodies))
        
        UxSampleContainer.Children.Clear
        UxSampleContainer.Children.Add(Sample)
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

        AddSample
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