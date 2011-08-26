Partial Public Class CollisionEvents
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UXBox1_Collide(Sender As Object, e As XamlPhysics.CollideEventArgs)
        If e.GeometryB.Body.Element Is UxBox2 Then
            Dispatcher.BeginInvoke(AddressOf ChangeColor)
        End If
    End Sub

    Private Sub ChangeColor()
        Static ColorIndex As Integer
        ColorIndex = ColorIndex + 1
        If ColorIndex > 4 Then ColorIndex = 0

        Dim BlockColoes = New Color(){Colors.Green, Colors.Purple, Colors.Magenta, Colors.Gray, Colors.Brown}

        UxBox1.Fill = New SolidColorBrush(BlockColoes(ColorIndex))
    End Sub
End Class