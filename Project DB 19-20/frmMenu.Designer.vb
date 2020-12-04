<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMenu
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
		Dim btnConnect4 As System.Windows.Forms.Button
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
		Dim btnBattleship As System.Windows.Forms.Button
		Dim btnClose As System.Windows.Forms.Button
		btnConnect4 = New System.Windows.Forms.Button()
		btnBattleship = New System.Windows.Forms.Button()
		btnClose = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'btnConnect4
		'
		btnConnect4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		btnConnect4.BackgroundImage = CType(resources.GetObject("btnConnect4.BackgroundImage"), System.Drawing.Image)
		btnConnect4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		btnConnect4.FlatAppearance.BorderSize = 0
		btnConnect4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		btnConnect4.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		btnConnect4.Location = New System.Drawing.Point(200, 200)
		btnConnect4.Name = "btnConnect4"
		btnConnect4.Size = New System.Drawing.Size(400, 225)
		btnConnect4.TabIndex = 0
		btnConnect4.Text = "Connect4"
		btnConnect4.UseVisualStyleBackColor = False
		AddHandler btnConnect4.Click, AddressOf Me.btnBConnect4_Click
		'
		'btnBattleship
		'
		btnBattleship.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		btnBattleship.BackgroundImage = CType(resources.GetObject("btnBattleship.BackgroundImage"), System.Drawing.Image)
		btnBattleship.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		btnBattleship.FlatAppearance.BorderSize = 0
		btnBattleship.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		btnBattleship.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		btnBattleship.Location = New System.Drawing.Point(1000, 200)
		btnBattleship.Name = "btnBattleship"
		btnBattleship.Size = New System.Drawing.Size(400, 225)
		btnBattleship.TabIndex = 3
		btnBattleship.Text = "Battleship"
		btnBattleship.UseVisualStyleBackColor = False
		AddHandler btnBattleship.Click, AddressOf Me.BtnBattleship_Click
		'
		'btnClose
		'
		btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
		btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		btnClose.FlatAppearance.BorderSize = 0
		btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		btnClose.Location = New System.Drawing.Point(600, 500)
		btnClose.Name = "btnClose"
		btnClose.Size = New System.Drawing.Size(400, 225)
		btnClose.TabIndex = 4
		btnClose.Text = "Close"
		btnClose.UseVisualStyleBackColor = False
		AddHandler btnClose.Click, AddressOf Me.btnClose_Click
		'
		'frmMenu
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.MenuHighlight
		Me.ClientSize = New System.Drawing.Size(1600, 900)
		Me.Controls.Add(btnClose)
		Me.Controls.Add(btnBattleship)
		Me.Controls.Add(btnConnect4)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmMenu"
		Me.ShowIcon = False
		Me.Text = "Menu"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.ResumeLayout(False)

	End Sub
End Class
