'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'You may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class GameLoop

    Public Class TickEventArgs
        Inherits EventArgs
        
        Public Sub New(TimeElapsed As TimeSpan)
            Me.TimeElapsed = TimeElapsed
        End Sub
        
        Public Property TimeElapsed As TimeSpan
    End Class

    ''' <summary>
    ''' Occurs at regular intervals when this instance has been started.
    ''' </summary>
    Public Event Tick As EventHandler(Of TickEventArgs)
    Protected Overridable Sub OnTick(TimeElapsed As TimeSpan)
        RaiseEvent Tick(Me, New TickEventArgs(TimeElapsed))
    End Sub
    ''' <summary>
    ''' Occurs at regular intervals when this instance has been started.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event UITick As EventHandler(Of TickEventArgs)
    Protected Overridable Sub OnUITick(TimeElapsed As TimeSpan)
        RaiseEvent UITick(Me, New TickEventArgs(TimeElapsed))
    End Sub

    Public Class ExceptionEventArgs
        Inherits EventArgs
        
        Public Sub New(Ex As Exception)
            Exception = Ex
        End Sub
        
        Public Property Exception As Exception
    End Class

    ''' <summary>
    ''' Occurs when there is an exception during a tick.
    ''' </summary>
    Public Event Exception As EventHandler(Of ExceptionEventArgs)
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
                Throw New ArgumentOutOfRangeException("Interval must be greater then 0.")
            End If
            LInterval = Value
        End Set
    End Property

    Dim LUISteps As Integer = 1
    ''' <summary>
    ''' The amount of ticks before the UI tick
    ''' </summary>
    Public Property UISteps() As Integer
        Get
            Return LUISteps
        End Get
        Set
            If Value <= 0 Then
                Throw New ArgumentOutOfRangeException("UISteps must be greater then 0.")
            End If
            LUISteps = Value
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
    Public Sub [Stop](Wait As Boolean)
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
            
                TickLoop(NewTime - LastTime)
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

    Private Sub TickLoop(TimeElapsed As TimeSpan)
        Static Wait As Integer = 10
        If Wait > 0 Then
            Wait = Wait - 1
            Return
        End If

        Static UIFrameCount As Integer = 1
        OnTick(TimeElapsed)

        UIFrameCount = UIFrameCount - 1
        If UIFrameCount = 0 Then
            UIFrameCount = UISteps
            OnUITick(New TimeSpan(TimeElapsed.Ticks * UISteps))
        End If
    End Sub
End Class