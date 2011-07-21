Public Class Viewer
Public Shared AutoPlay As Boolean = True
    
    Dim WithEvents GameLoop As New GameLoop

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
        GameLoop.Start
        UxPlay.IsEnabled = False
        UxPause.IsEnabled = True
        UxStop.IsEnabled = True
    End Sub
    Private Sub Pause() Handles UxPause.Click
        Lock.Set
        GameLoop.Stop(False)
        UxPlay.IsEnabled = True
        UxPause.IsEnabled = False
        UxStop.IsEnabled = True
    End Sub
    Private Sub [Stop]() Handles UxStop.Click
        Active = False
        Lock.Set
        GameLoop.Stop(False)
        UxBox.Reset
        
        UxPlay.IsEnabled = True
        UxPause.IsEnabled = False
        UxStop.IsEnabled = False

        AddSample
    End Sub

    Dim Active As Boolean
    Dim Lock As New System.Threading.ManualResetEvent(False)
    Private Sub GameLoop_Tick(sender As Object, e As GameLoop.TickEventArgs) Handles GameLoop.Tick
        Static Count As Integer = 0
        Count = Count + 1
        If Count > 10 Then
            Lock.Reset
            Dispatcher.BeginInvoke(New Action(AddressOf Tick))
            Lock.WaitOne
        End If
    End Sub
    Private Sub Tick()
        If Not GameLoop.IsRunning Then Return
        UxBox.World.Step(GameLoop.Interval.TotalSeconds)
        UxBox.Update(GameLoop.Interval)
        UxBox.UpdateUI(GameLoop.Interval)
        Lock.Set
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