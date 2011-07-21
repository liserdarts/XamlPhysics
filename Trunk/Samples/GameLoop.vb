Public Class GameLoop

    ''' <summary>
    ''' Occurs when this instance has been started at regular intervals.
    ''' </summary>
    Public Event Tick As EventHandler(Of TickEventArgs)
    Public Class TickEventArgs
        Inherits EventArgs
        
        Public Sub New(TimeElapsed As TimeSpan)
            Me.TimeElapsed = TimeElapsed
        End Sub
        
        Public Property TimeElapsed As TimeSpan
    End Class
    Protected Overridable Sub OnTick(TimeElapsed As TimeSpan)
        RaiseEvent Tick(Me, New TickEventArgs(TimeElapsed))
    End Sub

    ''' <summary>
    ''' Occurs when there is an exception during a tick.
    ''' </summary>
    Public Event Exception As EventHandler(Of ExceptionEventArgs)
    Public Class ExceptionEventArgs
        Inherits EventArgs
        
        Public Sub New(Ex As Exception)
            Exception = Ex
        End Sub
        
        Public Property Exception As Exception
    End Class
    Protected Overridable Sub OnException(Ex As Exception)
        RaiseEvent Exception(Me, New ExceptionEventArgs(Ex))
    End Sub

    Dim Active As Boolean
    Dim Thread As System.Threading.Thread
    Dim ResetEvent As New System.Threading.AutoResetEvent(False)
    
    Dim LInterval As TimeSpan = TimeSpan.FromMilliseconds(1)
    ''' <summary>
    ''' Gets or sets the time between ticks.
    ''' </summary>
    Public Property Interval() As TimeSpan
        Get
            Return LInterval
        End Get
        Set
            If Value <= TimeSpan.Zero Then
                Throw New ArgumentOutOfRangeException("The interval must be greater then 0.")
            End If
            LInterval = Value
        End Set
    End Property
    
    ''' <summary>
    ''' Gets a value indicating whether this instance is running.
    ''' </summary>
    ''' <value>
    ''' <c>True</c> if this instance is running; otherwise, <c>False</c>.
    ''' </value>
    Public ReadOnly Property IsRunning() As Boolean
        Get
            Return Thread IsNot Nothing
        End Get
    End Property
    
    ''' <summary>
    ''' Starts this instance.
    ''' </summary>
    Public Sub Start()
        SyncLock Me
            Active = True
            
            If Thread Is Nothing Then
                Thread = New System.Threading.Thread(AddressOf TickLoop)
                Thread.IsBackground = True
                Thread.Start
            End If
        End SyncLock
    End Sub
    
    ''' <summary>
    ''' Stops the specified wait.
    ''' </summary>
    ''' <param name="Wait">if set to <c>true</c> will wait for the current tick to finish.</param>
    Public Sub [Stop](ByVal Wait As Boolean)
        SyncLock Me
            Active = False
            ResetEvent.Set
            If Thread Is Nothing Then Return
            If Wait Then
                Thread.Join
            End If
            Thread = Nothing
        End SyncLock
    End Sub
    
    Private Sub TickLoop()
        Try
            Dim LastTime As Date = Date.Now
            Dim WaitTime As TimeSpan
        
            RaiseEvent Tick(Me, New TickEventArgs(TimeSpan.Zero))
        
            Do Until Active = False
                Dim NewTime As Date = Date.Now
            
                OnTick(NewTime - LastTime)
                LastTime = NewTime
            
                WaitTime = Interval - (Date.Now - LastTime)

                If WaitTime > TimeSpan.Zero Then
                    ResetEvent.WaitOne(WaitTime)
                    WaitTime = TimeSpan.Zero
                End If
            Loop
        Catch Ex As Exception
            Diagnostics.Debug.WriteLine(Ex.ToString)
            OnException(Ex)
        End Try
    End Sub
End Class