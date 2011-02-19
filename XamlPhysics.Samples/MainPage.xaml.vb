Partial Public Class MainPage
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Dim WithEvents GameLoop As New GameLoop

    Private Sub MainPage_Loaded(sender As Object, e As Windows.RoutedEventArgs) Handles Me.Loaded
        GameLoop.Interval = TimeSpan.FromMilliseconds(20)
        GameLoop.Start
    End Sub

    Private Sub GameLoop_Tick(sender As Object, e As GameLoop.TickEventArgs) Handles GameLoop.Tick
        Dispatcher.BeginInvoke(AddressOf Tick)
    End Sub
    Private Sub Tick()
        UxBox.World.Step(GameLoop.Interval.TotalSeconds)
        UxBox.Update(GameLoop.Interval)
        UxBox.UpdateUI(GameLoop.Interval)
    End Sub

End Class