Imports System.Threading
Imports OBSWebsocketDotNet
Imports OBSWebsocketDotNet.Types

Public Class Form1

    Private OBS As OBSWebsocket
    Private OBSmutex As Mutex
    Private OBSsocketString = "ws://192.168.1.205:4455"
    Private CurrentScene As String
    Private WaitForConnect As Boolean
    Private OBSstarted As TaskCompletionSource(Of Boolean)
    Private WaitForDisconnect As Boolean
    Private OBSended As TaskCompletionSource(Of Boolean)
    Private MyVolume As VolumeInfo
    Private VideoTestInput As String = "Ember Gears"
    Private AudioTestInput As String = "Ember's PC Audio"
    Private TextTestInput As String = "GlobalCounterTitle"
    Private TestScene1 As String = "Single Screen Mode"
    Private TestScene2 As String = "Split Screen Mode"

    Private Structure MediaActions
        Public Const Restart As String = "OBS_WEBSOCKET_MEDIA_INPUT_ACTION_RESTART"
        Public Const Play As String = "OBS_WEBSOCKET_MEDIA_INPUT_ACTION_PLAY"
        Public Const Pause As String = "OBS_WEBSOCKET_MEDIA_INPUT_ACTION_PAUSE"
        Public Const Stopp As String = "OBS_WEBSOCKET_MEDIA_INPUT_ACTION_STOP"
        Public Const Playing As String = "OBS_MEDIA_STATE_PLAYING"
        Public Const Paused As String = "OBS_MEDIA_STATE_PAUSED"
        Public Const Stopped As String = "OBS_MEDIA_STATE_STOPPED"
        Public Const Ended As String = "OBS_MEDIA_STATE_ENDED"
        Public Const Openning As String = "OBS_MEDIA_STATE_OPENNING"
        Public Const Bufferring As String = "OBS_MEDIA_STATE_BUFFERRING"
        Public Const Errorr As String = "OBS_MEDIA_STATE_ERROR"
        Public Const None As String = "OBS_MEDIA_STATE_NONE"
    End Structure


    Private Structure MonitorType
        Public Const None As String = "OBS_MONITORING_TYPE_NONE"
        Public Const Enabled As String = "OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT"
        Public Const Only As String = "OBS_MONITORING_TYPE_MONITOR_ONLY"

    End Structure

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(465, 81)

        OBS = New OBSWebsocket
        OBSmutex = New Mutex
        AddHandler OBS.SceneTransitionEnded, AddressOf HandleSceneChanged
        AddHandler OBS.Connected, AddressOf OBSconnected
        AddHandler OBS.Disconnected, AddressOf OBSdisconnected
        AddHandler OBS.MediaInputPlaybackStarted, AddressOf MediaStarted
        AddHandler OBS.MediaInputPlaybackEnded, AddressOf MediaEnded
        AddHandler OBS.MediaInputActionTriggered, AddressOf MediaEvent
    End Sub

    Private UpTime As Integer
    Private StopCount As Boolean

    Private Async Function CountUpTime() As Task
        UpTime = 0
        StopCount = False
        Await Task.Delay(1000)
        Do Until StopCount = True
            UpTime = UpTime + 1
            Await Task.Delay(1000)
        Loop
        StopCount = False
        SendMessage(ErrorMessage & " after " & UpTime & " seconds")
    End Function

    Private Sub Start_Click(sender As Object, e As EventArgs) Handles Start.Click
        Enabled = False
        Dim StartTask As Task = Startup()
    End Sub


    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        TextBox2.Text = OBSsocketString
    End Sub

    Private Async Function Startup() As Task
        If Await CheckOBSconnect() Then Enabled = True
        Me.Size = New Size(465, 223)
        Await CountUpTime()
    End Function

    Private Sub OBSconnected(sender As Object, e As EventArgs)
        If WaitForConnect Then OBSstarted.SetResult(False)
    End Sub

    Private Sub MediaEvent(Sender As OBSWebsocket, e As Events.MediaInputActionTriggeredEventArgs)
        If e.InputName = VideoTestInput Then
            Select Case e.MediaAction
                Case MediaActions.Restart, MediaActions.Play
                    Button5.BackColor = SystemColors.ActiveCaption
                    Button6.BackColor = SystemColors.Control
                    Button7.BackColor = SystemColors.Control
                Case MediaActions.Paused
                    Button5.BackColor = SystemColors.Control
                    Button6.BackColor = SystemColors.ActiveCaption
                    Button7.BackColor = SystemColors.Control
            End Select
        End If
    End Sub

    Private Sub MediaStarted(Sender As OBSWebsocket, e As Events.MediaInputPlaybackStartedEventArgs)
        If e.InputName = VideoTestInput Then
            SendMessage("Started")
        End If
    End Sub
    Private Sub MediaEnded(Sender As OBSWebsocket, e As Events.MediaInputPlaybackEndedEventArgs)
        If e.InputName = VideoTestInput Then
            Button5.BackColor = SystemColors.Control
            Button6.BackColor = SystemColors.Control
            Button7.BackColor = SystemColors.ActiveCaption
        End If
    End Sub

    Private ErrorMessage As String
    Private Sub OBSdisconnected(sender As Object, e As Communication.ObsDisconnectionInfo)

        ErrorMessage = e.DisconnectReason
        StopCount = True
        If WaitForDisconnect Then OBSended.SetResult(False)
    End Sub

    Private Sub HandleSceneChanged(Sender As OBSWebsocket, e As Events.SceneTransitionEndedEventArgs)
        OBSmutex.WaitOne()
        CurrentScene = OBS.GetCurrentProgramScene
        Dim MyList As List(Of SceneItemDetails) = OBS.GetSceneItemList(CurrentScene)
        Dim OutputString As String
        If MyList IsNot Nothing Then
            OutputString = MyList(0).ItemId & "_" & MyList(0).SourceName
            For i As Integer = 1 To MyList.Count - 1
                OutputString = OutputString & vbCrLf & MyList(i).ItemId & "_" & MyList(i).SourceName
            Next
        Else
            OutputString = "No Items in Scene?!"
        End If

        SendMessage(OutputString)
        OBSmutex.ReleaseMutex()
        SceneChanged(CurrentScene)
    End Sub

    Private Sub SceneChanged(SceneName As String)


        BeginInvoke(
            Sub()
                Select Case SceneName
                    Case TestScene1
                        Button1.BackColor = SystemColors.ActiveCaption
                        Button2.BackColor = SystemColors.Control
                    Case TestScene2
                        Button1.BackColor = SystemColors.Control
                        Button2.BackColor = SystemColors.ActiveCaption
                    Case Else
                        Button1.BackColor = SystemColors.Control
                        Button2.BackColor = SystemColors.Control
                End Select
                Button1.Enabled = True
                Button2.Enabled = True
            End Sub)
    End Sub

    Public Async Function DisconnectOBS() As Task
        OBSmutex.WaitOne()
        If OBS.IsConnected = True Then
            OBSended = New TaskCompletionSource(Of Boolean)
            WaitForDisconnect = True
            OBS.Disconnect()
            WaitForDisconnect = Await OBSended.Task
        End If
        OBSmutex.ReleaseMutex()
        Me.Close()
    End Function

    Public Async Function CheckOBSconnect() As Task(Of Boolean)
        OBSmutex.WaitOne()
        If OBS.IsConnected = False Then
            Try
                OBSstarted = New TaskCompletionSource(Of Boolean)
                WaitForConnect = True
                OBS.ConnectAsync(TextBox2.Text, TextBox3.Text)
                WaitForConnect = Await OBSstarted.Task
                CurrentScene = OBS.GetCurrentProgramScene
                'Select Case OBS.GetMediaInputStatus(VideoTestInput).State
                '    Case MediaActions.Playing, MediaActions.Openning, MediaActions.Bufferring
                '        Button5.BackColor = SystemColors.ActiveCaption
                '        Button6.BackColor = SystemColors.Control
                '        Button7.BackColor = SystemColors.Control
                '    Case MediaActions.Paused
                '        Button5.BackColor = SystemColors.Control
                '        Button6.BackColor = SystemColors.ActiveCaption
                '        Button7.BackColor = SystemColors.Control
                '    Case MediaActions.Stopped, MediaActions.Ended, MediaActions.Errorr, MediaActions.None
                '        Button5.BackColor = SystemColors.Control
                '        Button6.BackColor = SystemColors.Control
                '        Button7.BackColor = SystemColors.ActiveCaption
                '    Case Else
                '        SendMessage(OBS.GetMediaInputStatus(VideoTestInput).State)
                'End Select
                'MyVolume = OBS.GetInputVolume(AudioTestInput)

                'NumericUpDown1.Value = MyVolume.VolumeDb

                'If OBS.GetInputMute("Ember's PC Audio") Then
                '    Button8.BackColor = SystemColors.ActiveCaption
                'Else
                '    Button8.BackColor = SystemColors.Control
                'End If

                'Select Case OBS.GetInputAudioMonitorType(AudioTestInput)
                '    Case MonitorType.None
                '        Button9.BackColor = SystemColors.Control
                '    Case Else
                '        Button9.BackColor = SystemColors.ActiveCaption
                'End Select

                OBSmutex.ReleaseMutex()
                'SceneChanged(CurrentScene.Name)
                Return True
            Catch ex As Exception
                OBSmutex.ReleaseMutex()
                Return False
            End Try
        Else
            OBSmutex.ReleaseMutex()
            Return True
        End If
    End Function

    Private Sub Messenger(MessageText As String)
        Dim SplitString() As String
        SplitString = Split(MessageText, ":<>:")
        MsgBox(SplitString(0), , SplitString(1))
    End Sub
    Public Sub SendMessage(MessageText As String, Optional TitleString As String = "Something Happened!!")
        Dim MessageSender As Thread
        MessageSender = New Thread(AddressOf Messenger)
        MessageSender.Start(MessageText & ":<>:" & TitleString)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'SendMessage(CurrentScene.Name)
        If CurrentScene <> TestScene1 Then
            OBSmutex.WaitOne()
            Button1.Enabled = False
            Button2.Enabled = False
            OBS.SetCurrentProgramScene(TestScene1)
            OBSmutex.ReleaseMutex()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'SendMessage(CurrentScene.Name)
        If CurrentScene <> TestScene2 Then
            OBSmutex.WaitOne()
            Button1.Enabled = False
            Button2.Enabled = False
            OBS.SetCurrentProgramScene(TestScene2)
            OBSmutex.ReleaseMutex()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) And TextBox1.Text <> "" Then
            OBSmutex.WaitOne()
            Dim InputSettings As InputSettings = OBS.GetInputSettings(TextTestInput)
            InputSettings.Settings.Property("text").Value = TextBox1.Text
            OBS.SetInputSettings(InputSettings)
            OBSmutex.ReleaseMutex()
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If OBS.IsConnected Then
            e.Cancel = True
            Dim EndTask As Task = DisconnectOBS()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OBSmutex.WaitOne()
        Dim MyID As Integer = OBS.GetSceneItemId(CurrentScene, "Ember", 0)
        'SendMessage(MyID)
        If OBS.GetSceneItemEnabled(CurrentScene, MyID) Then
            OBS.SetSceneItemEnabled(CurrentScene, MyID, False)
        Else
            OBS.SetSceneItemEnabled(CurrentScene, MyID, True)
        End If
        OBSmutex.ReleaseMutex()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'OBSmutex.WaitOne()
        'Dim InputSettings As InputSettings = OBS.GetInputSettings(VideoTestInput)
        'Dim OutputString As String = InputSettings.Settings.ToString

        'Dim LocalFile As String = InputSettings.Settings.Property("local_file").Value
        'If InStr(LocalFile, "blink") > 0 Then
        '    LocalFile = Replace(LocalFile, "blink", "happy")
        'ElseIf InStr(LocalFile, "happy") > 0 Then
        '    LocalFile = Replace(LocalFile, "happy", "blink")
        'End If
        'InputSettings.Settings.Property("local_file").Value = LocalFile
        'OBS.SetInputSettings(InputSettings)

        'OBSmutex.ReleaseMutex()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        OBSmutex.WaitOne()
        OBS.TriggerMediaInputAction(VideoTestInput, MediaActions.Restart)
        OBSmutex.ReleaseMutex()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        OBSmutex.WaitOne()
        OBS.TriggerMediaInputAction(VideoTestInput, MediaActions.Pause)
        OBSmutex.ReleaseMutex()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        OBSmutex.WaitOne()
        OBS.TriggerMediaInputAction(VideoTestInput, MediaActions.Stopp)
        OBSmutex.ReleaseMutex()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        OBSmutex.WaitOne()
        If OBS.GetInputMute(AudioTestInput) Then
            OBS.SetInputMute(AudioTestInput, False)
            Button8.BackColor = SystemColors.Control
        Else
            OBS.SetInputMute(AudioTestInput, True)
            Button8.BackColor = SystemColors.ActiveCaption
        End If
        OBSmutex.ReleaseMutex()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        OBSmutex.WaitOne()
        Select Case OBS.GetInputAudioMonitorType(AudioTestInput)
            Case MonitorType.None
                OBS.SetInputAudioMonitorType(AudioTestInput, MonitorType.Enabled)
                Button9.BackColor = SystemColors.ActiveCaption
            Case Else
                OBS.SetInputAudioMonitorType(AudioTestInput, MonitorType.None)
                Button9.BackColor = SystemColors.Control
        End Select
        OBSmutex.ReleaseMutex()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If Enabled Then
            OBSmutex.WaitOne()
            OBS.SetInputVolume(AudioTestInput, NumericUpDown1.Value, True)
            OBSmutex.ReleaseMutex()
        End If
    End Sub

End Class
