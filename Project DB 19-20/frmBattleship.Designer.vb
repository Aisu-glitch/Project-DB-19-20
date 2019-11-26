<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBattleship
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
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
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBattleship))
		Me.pbxTest = New System.Windows.Forms.PictureBox()
		Me.txtDebug = New System.Windows.Forms.TextBox()
		CType(Me.pbxTest, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'pbxTest
		'
		Me.pbxTest.Image = CType(resources.GetObject("pbxTest.Image"), System.Drawing.Image)
		Me.pbxTest.Location = New System.Drawing.Point(143, 75)
		Me.pbxTest.Name = "pbxTest"
		Me.pbxTest.Size = New System.Drawing.Size(295, 228)
		Me.pbxTest.TabIndex = 0
		Me.pbxTest.TabStop = False
		'
		'txtDebug
		'
		Me.txtDebug.BackColor = System.Drawing.SystemColors.HotTrack
		Me.txtDebug.Location = New System.Drawing.Point(317, 75)
		Me.txtDebug.Multiline = True
		Me.txtDebug.Name = "txtDebug"
		Me.txtDebug.Size = New System.Drawing.Size(295, 228)
		Me.txtDebug.TabIndex = 1
		'
		'frmBattleship
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(800, 450)
		Me.Controls.Add(Me.pbxTest)
		Me.Controls.Add(Me.txtDebug)
		Me.Name = "frmBattleship"
		Me.Text = "Form2"
		CType(Me.pbxTest, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents pbxTest As PictureBox
	Friend WithEvents txtDebug As TextBox
End Class
