Public Class frmConnect4
	Dim endbox As TextBox
	Dim GameRun As Boolean
	Dim Discs As SortedList(Of String, Graphics) = New SortedList(Of String, Graphics)

    '*** On Load
    Private Sub frm4OpEenRij_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.MaximumSize = Me.Size
		Me.MinimumSize = Me.Size
		Timer.Enabled = True
        GameRun = True
        Dim tb As TextBox
        Dim bt As Button
        For Each tb In grbSelectie.Controls
            AddHandler tb.MouseEnter, AddressOf grpSelectie_TextBox_MouseEnter
            AddHandler tb.MouseLeave, AddressOf grpSelectie_TextBox_MouseLeave
            AddHandler tb.MouseClick, AddressOf grpSelectie_TextBox_MouseClick
        Next
        '*** Make all playfields round
        Dim Pth As New System.Drawing.Drawing2D.GraphicsPath
        Dim Reg As New Region(Pth)
        For Each obj As Object In Me.Controls
            If TypeOf obj Is GroupBox Then
                Pth.AddEllipse(New Rectangle(0, 0, 50, 50))
                Reg = New Region(Pth)
                Dim gb As GroupBox = obj
                For Each tb In gb.Controls
                    tb.Region = Reg
                Next
            End If
            If TypeOf obj Is TextBox Then
                Pth.AddEllipse(New Rectangle(0, 0, 50, 50))
                Reg = New Region(Pth)
                obj.Region = Reg
            End If
            If TypeOf obj Is Button Then
                bt = obj
                If bt.BackgroundImage IsNot Nothing Then
                    Dim map As Bitmap = New Bitmap(bt.BackgroundImage, bt.Width - 1, bt.Height - 1)
                    '*** Voor elke pixel
                    For I = 1 To map.Width - 1
                        For J = 1 To map.Height - 1
                            '*** Als de pixel leeg is
                            If map.GetPixel(I, J).ToArgb <> 0 Then
                                '*** Teken hem bij de graphische tekening
                                Pth.AddRectangle(New Rectangle(I, J, 1, 1))
                            End If
                        Next
                    Next
                    Reg = New Region(Pth)
                    bt.Region = Reg
                    bt.BackgroundImage = Nothing
                End If
                If BackgroundImage IsNot Nothing Then
                    bt.Image = New Bitmap(bt.Image, bt.Width - 1, bt.Height - 1)
                End If
            End If
            Pth.Reset()
        Next
        '*** Start global randomizer
        Randomize()
	End Sub
    '*** animation step timer
    Private Sub Timer_Timer() Handles Timer.Tick
		'*** Animation trigger
		DropDisc()
	End Sub
    '*** Sub To add discs to animation
    Private Sub AddDisc(sender As Object)
		'*** Clearing Disc list
		Discs.Clear()
		'*** declaration of variables
		Dim Ystart As Integer
		Dim tb As TextBox
		Dim Name As String = sender.Name.Split("Y")(0)
		endbox.BackColor = Color.White
		'*** Get every field in the row and draw an object to start with at the top
		For i = 5 To 0 Step -1
			For Each obj As Object In grbVeld.Controls
				If TypeOf obj Is TextBox Then
					tb = obj
					If tb.Name = Name & "Y" & CStr(i) AndAlso i >= sender.Name.Split("Y")(1) AndAlso
						tb.Text = "" Then
						Ystart = -(5 - tb.Name.Split("Y")(1)) * 56
						Dim Key As String = tb.Name & ":Y" & Ystart
						Dim g As Graphics = tb.CreateGraphics
						Discs.Add(Key, g)
					End If
				End If
			Next
		Next
	End Sub
    '*** Animation function for discs
    Private Sub DropDisc()
		'*** Declaration of the variables
		Dim TempDiscs As SortedList(Of String, Graphics) = New SortedList(Of String, Graphics)
		Dim Xstart, Ystart As Integer
		Dim Brush As Brush = New SolidBrush(txtBeurt.BackColor)
		Dim Pen As Pen = New Pen(Color.White)
		Dim Name As String
		Dim Key As String
		Dim g As Graphics
		Dim Size As Rectangle
		Xstart = 0
		Ystart = 0
		'*** For each disc
		For Each disc As KeyValuePair(Of String, Graphics) In Discs
			'*** Gather information about the disc
			Name = disc.Key.Split(":")(0)
			Key = disc.Key.Split(":")(1)
			g = disc.Value
			Ystart = Key.Split("Y")(1)
			'*** Build a new disc, overwrite and replace the old one
			Size = New Rectangle(0, Ystart, 50, 50)
			g.DrawPie(Pen, New Rectangle(0, Ystart - 8, 50, 50), 0, 0)
			g.FillEllipse(New SolidBrush(Color.White), New Rectangle(0, Ystart - 8, 50, 50))
			g.DrawPie(Pen, Size, 0, 0)
			g.FillEllipse(Brush, Size)

			'*** Check if it is the last field
			If Name = endbox.Name Then
				'*** Check if last field has his disc yet to be centered
				If Ystart < 0 Then
					TempDiscs.Add(Name & ":Y" & Ystart + 8, g)
				Else
					endbox.BackColor = txtBeurt.BackColor
					EndRound(endbox)
				End If
			Else
				'*** Check disc is still visable
				If Ystart <= 50 Then
					TempDiscs.Add(Name & ":Y" & Ystart + 8, g)
				End If
			End If
		Next
		'*** Update disc list to only contain the still needing to be animated discs
		Discs.Clear()
		For Each Disc As KeyValuePair(Of String, Graphics) In TempDiscs
			Discs.Add(Disc.Key, Disc.Value)
		Next
		TempDiscs.Clear()
	End Sub
    '*** Begin button
    Private Sub btnBegin_Click(sender As Object, e As EventArgs) Handles btnBegin.Click
		'*** Turn game progression on
		GameRun = True
		'*** Choose randomly what player begins
		If Fix(Rnd() * 2) = 1 Then
			txtBeurt.BackColor = Color.Red
		Else
			txtBeurt.BackColor = Color.Yellow
		End If
		'*** Disable / enable appropriate controls
		btnBegin.Enabled = False
		btnStop.Enabled = True
		'*** Reset playing field
		Dim tb As TextBox
		For Each obj As Object In grbSelectie.Controls
			If TypeOf obj Is TextBox Then
				tb = obj
				tb.Enabled = True
				tb.Tag = ""
				tb.BackColor = Color.White
			End If
		Next
		For Each obj As Object In grbVeld.Controls
			If TypeOf obj Is TextBox Then
				tb = obj
				tb.BackColor = Color.White
				tb.Clear()
			End If
		Next
	End Sub
    '*** Stop button
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
		'*** Turn game progression off
		GameRun = False
		'*** Disable / enable appropriate controls
		lblWinner.Text = ""
		btnBegin.Enabled = True
		btnStop.Enabled = False
		txtBeurt.BackColor = Color.White
		For Each obj As Object In grbSelectie.Controls
			If TypeOf obj Is TextBox Then
				obj.Enabled = False
			End If
		Next
	End Sub
    '*** Menu button
    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Me.Close()
    End Sub
    '*** Mouse enter playfields
    Private Sub grpSelectie_TextBox_MouseEnter(sender As TextBox, e As EventArgs)
		'*** Make function only run when game progression is on
		If (GameRun = False) Then
			Exit Sub
		End If
		'*** Let player know where the player is hovering by changing the background color
		sender.BackColor = txtBeurt.BackColor
		'*** Get lowest empty field
		Dim tb As TextBox = GetLowestEmptyField(sender)
		'*** Set preview color of where the disc would land
		If txtBeurt.BackColor.ToArgb = Color.Red.ToArgb Then
			tb.BackColor = Color.OrangeRed
		Else
			tb.BackColor = Color.YellowGreen
		End If
	End Sub
    '*** Mouse leave playfields
    Private Sub grpSelectie_TextBox_MouseLeave(sender As TextBox, e As EventArgs)
		'*** Search for preview
		Dim tb As TextBox = GetHighestPlayerField(sender)
		'*** If he lowest field is indeed a preview, set it to white
		If txtBeurt.BackColor.ToArgb = sender.BackColor.ToArgb Then
			tb.BackColor = Color.White
		End If
		'*** Make the selection field white
		sender.BackColor = Color.White
	End Sub
    '*** Mouse click playfields
    Private Sub grpSelectie_TextBox_MouseClick(sender As TextBox, e As EventArgs)
		'*** If the clicked field is white, cancel
		If sender.BackColor = Color.White Then
			Exit Sub
		End If
		'*** Search for preview
		Dim tb As TextBox = GetHighestPlayerField(sender)
		'*** If preview is the selected field, cancel
		If tb.Name = sender.Name Then
			Exit Sub
		End If
		'*** Disable all selectable fields
		For Each textb As TextBox In grbSelectie.Controls
			textb.Enabled = False
		Next
		'*** Disc drop animation
		endbox = tb
		AddDisc(tb)
		'*** Add Verification data
		tb.Text = " "

	End Sub
    '*** Gettingthe lowest empty field
    Function GetLowestEmptyField(Beginbox As TextBox) As TextBox
		'*** Create verifaction data
		Dim Name As String = Beginbox.Name.Split("Y")(0)
		'*** Check all fields in a row and return the lowest not verified field
		For i = 0 To 5 Step 1
			For Each tb As Control In grbVeld.Controls
				If TypeOf tb Is TextBox Then
					If tb.Name = Name & "Y" & CStr(i) And tb.BackColor.ToArgb = Color.White.ToArgb And tb.Name <> " " Then
						Return tb
					End If
				End If
			Next
		Next
		Return Beginbox
	End Function
    '*** Getting highest played field
    Function GetHighestPlayerField(Beginbox As TextBox) As TextBox
		'*** Declaration of variables
		Dim tb As TextBox = Beginbox
		Dim Name As String = Beginbox.Name.Split("Y")(0)
		'*** Find the last player field (This is a preview)
		For i = 5 To 0 Step -1
			For Each obj As Object In grbVeld.Controls
				If TypeOf obj Is TextBox Then
					tb = obj
					If tb.BackColor.ToArgb = Color.YellowGreen.ToArgb Or tb.BackColor.ToArgb = Color.OrangeRed.ToArgb AndAlso
						tb.Name = Name & "Y" & CStr(i) AndAlso
						tb.Text <> " " Then
						Return tb
					End If
				End If
			Next
		Next
		'*** If there is no preview return itself
		Return Beginbox
	End Function
    '*** Endround
    Private Sub EndRound(PlayedField As TextBox)
		'*** Declaration of winner variable
		Dim strPoint As String = PointCheck(PlayedField)
		'*** Get sender
		Dim sender As TextBox = New TextBox()
		For Each tb As TextBox In grbSelectie.Controls
			tb.Enabled = True
			If tb.Name.Split("Y")(0) = PlayedField.Name.Split("Y")(0) Then
				sender = tb
			End If
		Next
		'*** Check who made a point, if noone skip to next round
		Select Case strPoint
			Case "Red"
				Exit Select
			Case "Yellow"
				Exit Select
			Case Else
                '*** Switch player color
                RoleSwap()
                For Each tb As TextBox In grbSelectie.Controls
                    tb.Enabled = True
                Next
                Exit Sub
        End Select
        GameRun = False
        '*** Empty all selection controls
        For Each tb As TextBox In grbSelectie.Controls
            tb.Clear()
            tb.Enabled = False
        Next
        '*** Show who won
        lblWinner.Text = strPoint & " Wins"
        '*** Simulate a new mouse enter event
        grpSelectie_TextBox_MouseEnter(sender, New EventArgs())
    End Sub
    '*** Pointcheck
    Function PointCheck(PlayedField As TextBox) As String
        '*** Make viables to manage coördinates or points
        Dim Point As String = "No Point"
        Dim PFCoords As String = PlayedField.Name.Split("X")(1)
        Dim PFNameX As String = PlayedField.Name.Split("X")(0) & "X"
        Dim PFNameY As String = PlayedField.Name.Split("Y")(0) & "Y"
        Dim PFX As Integer = CInt(PFCoords.Split("Y")(0))
        Dim PFY As Integer = CInt(PFCoords.Split("Y")(1))
        Dim Check(3, 6) As String
        Dim tb As TextBox
        '*** Build Checklist for all directions
        For i = PFX - 3 To PFX + 3 Step 1
            For Each obj As Object In grbVeld.Controls
                If TypeOf obj Is TextBox Then
                    tb = obj
                    '*** Horizontal check Check(0,x)
                    If tb.Name = PFNameX & CStr(i) & "Y" & PFY Then
                        Check(0, ((i - PFX) + 3)) = tb.Name
                    End If
                    '*** Diagonal check / Check(1,x)
                    If tb.Name = PFNameX & CStr(i) & "Y" & (PFY + (i - PFX)) Then
                        Check(1, ((i - PFX) + 3)) = tb.Name
                    End If
                    '*** Diagonal check \ Check(2,x)
                    If tb.Name = PFNameX & CStr(i) & "Y" & (PFY - (i - PFX)) Then
                        Check(2, ((i - PFX) + 3)) = tb.Name
                    End If
                End If
            Next
        Next
        '*** Vertical check Check(3,x)
        For i = PFY - 3 To PFY + 3 Step 1
            For Each obj As Object In grbVeld.Controls
                If TypeOf obj Is TextBox Then
                    tb = obj
                    If tb.Name = PFNameY & CStr(i) Then
                        Check(3, ((i - PFY) + 3)) = tb.Name
                    End If
                End If
            Next
        Next
        '*** Declaration of variables to count point streaks
        Dim CurColor As Color = txtBeurt.BackColor
		Dim Count As Integer = 0
		Dim WinnerTemp(6) As String
		Dim Winners(6) As String
		Dim Winner(6) As String
		'*** Check all checklists if a player has won
		For i = 0 To 3 Step 1
			For j = 0 To 6 Step 1
				For Each obj As Object In grbVeld.Controls
					If TypeOf obj Is TextBox Then
						tb = obj
						If tb.Name = Check(i, j) Then
							'*** Check if the played field has the same color as the player
							If tb.BackColor = CurColor Then
								'*** Add field to point count, point + 1
								Winner(Count) = tb.Name
								Count += 1
							Else
								'*** Reset point count to 0
								Count = 0
							End If
							'*** If 4 or more points are made, mark them
							If Count >= 4 Then
								'*** Mark all point containing fields
								For k = 0 To Count - 1 Step 1
									For Each ob As Object In grbVeld.Controls
										If TypeOf ob Is TextBox Then
											tb = ob
											If tb.Name = Winner(k) Then
												tb.Tag = "1"
											End If
										End If
									Next
								Next
								'*** Set winner name
								Point = CurColor.ToString.Split("]")(0).Split("[")(1)
							End If
						End If
					End If
				Next
			Next
			Count = 0
		Next
		'*** color all the winnen fields
		For Each obj As Object In grbVeld.Controls
			If TypeOf obj Is TextBox Then
				tb = obj
				If tb.Tag = "1" Then
					tb.BackColor = Color.Green
				End If
			End If
		Next
		'*** Return winner
		Return Point
	End Function
    '*** Roleswap
    Private Sub RoleSwap()
        '*** Change current active player
        If txtBeurt.BackColor = Color.Yellow Then
            txtBeurt.BackColor = Color.Red
        Else
            txtBeurt.BackColor = Color.Yellow
        End If
    End Sub

End Class
