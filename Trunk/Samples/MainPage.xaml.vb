Partial Public Class MainPage
    Inherits UserControl

    Public Sub New()
        InitializeComponent
    End Sub

    Public Sub Init(Params As IDictionary(Of String, String))
        If Params.ContainsKey("Sample") Then
            Dim Sample = Params("Sample")
            If Not String.IsNullOrEmpty(Sample) Then
                UxSampleViewer.JournalOwnership = Navigation.JournalOwnership.OwnsJournal
                UxSampleViewer.Navigate(New Uri("/Samples/XamlPhysics.Samples." & Sample, UriKind.Relative))
                Viewer.AutoPlay = False
            End If
        End If
    End Sub
End Class