Partial Public Class Viewer
    Inherits Page

    Public Sub New()
        InitializeComponent
    End Sub

    Public Shared AutoPlay As Boolean = True
    Dim WithEvents GameLoop As New GameLoop

    Protected Overrides Sub OnNavigatedTo(e As Windows.Navigation.NavigationEventArgs)
        [Stop]

        GameLoop.Interval = TimeSpan.FromMilliseconds(20)
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
        GameLoop.Start
        UxPlay.IsEnabled = False
        UxPause.IsEnabled = True
        UxStop.IsEnabled = True
    End Sub
    Private Sub Pause() Handles UxPause.Click
        Lock.Set
        GameLoop.Stop(True)
        UxPlay.IsEnabled = True
        UxPause.IsEnabled = False
        UxStop.IsEnabled = True
    End Sub
    Private Sub [Stop]() Handles UxStop.Click
        Active = False
        Lock.Set
        GameLoop.Stop(True)
        UxBox.Reset
        
        UxPlay.IsEnabled = True
        UxPause.IsEnabled = False
        UxStop.IsEnabled = False

        Dim SampleType = Type.GetType(NavigationContext.QueryString("Sample"))
        Dim Sample = Activator.CreateInstance(SampleType)
        
        UxSampleContainer.Children.Clear
        UxSampleContainer.Children.Add(Sample)
    End Sub

    Dim Active As Boolean
    Dim Lock As New System.Threading.ManualResetEvent(False)
    Private Sub GameLoop_Tick(sender As Object, e As GameLoop.TickEventArgs) Handles GameLoop.Tick
        Static Count As Integer = 0
        Count = Count + 1
        If Count > 10 Then
            Lock.Reset
            Dispatcher.BeginInvoke(AddressOf Tick)
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
        Stop
    End Sub
End Class