<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(210, 36)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "scene1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 54)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(210, 36)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "scene2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 96)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(210, 23)
        Me.TextBox1.TabIndex = 2
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(228, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(210, 36)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Hide/Show Ember"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(228, 54)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(210, 36)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Change Mood"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(228, 96)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(66, 52)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "PLAY"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(300, 96)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(66, 52)
        Me.Button6.TabIndex = 6
        Me.Button6.Text = "PAUSE"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(372, 96)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(66, 52)
        Me.Button7.TabIndex = 7
        Me.Button7.Text = "STOP"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.DecimalPlaces = 2
        Me.NumericUpDown1.Location = New System.Drawing.Point(12, 125)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(66, 23)
        Me.NumericUpDown1.TabIndex = 8
        Me.NumericUpDown1.Tag = ""
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(84, 125)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(66, 23)
        Me.Button8.TabIndex = 9
        Me.Button8.Text = "MUTE"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(156, 125)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(66, 23)
        Me.Button9.TabIndex = 10
        Me.Button9.Text = "MON"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 159)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "OBS WS TEST"
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
End Class
