Public Class frmConnect4

    Dim endbox As TextBox
    Dim GameRun As Boolean
    Dim Discs As SortedList(Of String, Graphics) = New SortedList(Of String, Graphics)

    Private Sub frm4OpEenRij_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer.Enabled = True
        GameRun = True
        '*** Voeg allen selectie velden toe aan de triggers
        For Each tb As TextBox In grbSelectie.Controls
            AddHandler tb.MouseEnter, AddressOf grpSelectie_TextBox_MouseEnter
            AddHandler tb.MouseLeave, AddressOf grpSelectie_TextBox_MouseLeave
            AddHandler tb.MouseClick, AddressOf grpSelectie_TextBox_MouseClick
        Next
        Dim Pth As New System.Drawing.Drawing2D.GraphicsPath
        Pth.AddEllipse(New Rectangle(0, 0, 50, 50))
        Dim Reg As New Region(Pth)
        For Each obj As Object In Me.Controls
            If TypeOf obj Is GroupBox Then
                Dim gb As GroupBox = obj
                For Each tb As TextBox In gb.Controls
                    tb.Region = Reg
                Next
            End If
            If TypeOf obj Is TextBox Then
                obj.Region = Reg
            End If
        Next
    End Sub

    Private Sub Timer_Timer() Handles Timer.Tick
        DropDisc()
    End Sub

    Private Sub AddDDisc(sender As Object)
        '*** Create a local version of the graphics object for the PictureBox.
        Discs.Clear()
        Dim Xstart, Ystart As Integer
        Xstart = 0
        Ystart = 0
        Dim tb As TextBox
        Dim Name As String = sender.Name.Split("Y")(0)
        For Each obj As Object In grbVeld.Controls
            If TypeOf obj Is TextBox Then
                tb = obj
                If tb.Text = "" Then
                    tb.BackColor = Color.White
                End If
            End If
        Next
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

    Private Async Sub DropDisc()
        '*** Create a local version of the graphics object for the PictureBox.
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
        For i = 0 To Discs.Count - 1 Step 1
            Name = Discs.Keys(i).Split(":")(0)
            Key = Discs.Keys(i).Split(":")(1)
            g = Discs.Values(i)
            Ystart = Key.Split("Y")(1)
            Size = New Rectangle(0, Ystart, 50, 50)
            g.DrawPie(Pen, New Rectangle(0, Ystart - 8, 50, 50), 0, 0)
            g.FillEllipse(New SolidBrush(Color.White), New Rectangle(0, Ystart - 8, 50, 50))
            g.DrawPie(Pen, Size, 0, 0)
            g.FillEllipse(Brush, Size)

            If Name = endbox.Name Then
                If Ystart < 0 Then
                    TempDiscs.Add(Name & ":Y" & Ystart + 8, g)
                Else
                    endbox.BackColor = txtBeurt.BackColor
                    EndRound(endbox)
                End If
            Else
                If Ystart <= 50 Then
                    TempDiscs.Add(Name & ":Y" & Ystart + 8, g)
                End If
            End If

        Next
        Discs.Clear()
        For Each Disc As KeyValuePair(Of String, Graphics) In TempDiscs
            Discs.Add(Disc.Key, Disc.Value)
        Next
        TempDiscs.Clear()

    End Sub

    Private Sub btnBegin_Click(sender As Object, e As EventArgs) Handles btnBegin.Click
        If (GameRun = False) Then
            GameRun = True
        End If
        '*** Kies een willekeurige begin-speler en start het spel
        Randomize()
        If Fix(Rnd() * 2) = 1 Then
            txtBeurt.BackColor = Color.Red
        Else
            txtBeurt.BackColor = Color.Yellow
        End If
        For Each tb As TextBox In grbSelectie.Controls
            tb.Enabled = True
        Next
        btnBegin.Enabled = False
        btnStop.Enabled = True
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If (GameRun) Then
            GameRun = False
        End If
        '*** Reset het spel veld en speler opties
        lblWinner.Text = ""
        btnBegin.Enabled = True
        btnStop.Enabled = False
        txtBeurt.BackColor = Color.White
        Dim tb As TextBox
        For Each obj As Object In grbSelectie.Controls
            If TypeOf obj Is TextBox Then
                tb = obj
                tb.Enabled = False
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

    Private Sub grpSelectie_TextBox_MouseEnter(sender As TextBox, e As EventArgs)
        If (GameRun = False) Then
            Exit Sub
        End If
        '*** Zet de achtergrond kleur naar de speler
        sender.BackColor = txtBeurt.BackColor
        '*** Zoek het laagste niet gespeelde veld en preview het als optie
        Dim tb As TextBox = GetLowestEmptyField(sender)
        Dim col As Color
        If txtBeurt.BackColor.ToArgb = Color.Red.ToArgb Then
            col = Color.OrangeRed
        Else
            col = Color.YellowGreen
        End If
        tb.BackColor = col
    End Sub

    Private Sub grpSelectie_TextBox_MouseLeave(sender As TextBox, e As EventArgs)
        '*** Zoek het gepreviewde vak
        Dim tb As TextBox = GetHighestPlayerField(sender)
        '*** als het dezelfde kleur is als de speler zijn kleur maak het gepreviewde vak wit
        If txtBeurt.BackColor.ToArgb = sender.BackColor.ToArgb Then
            tb.BackColor = Color.White
        End If
        '*** Maak het geselcteerde veld wit
        sender.BackColor = Color.White
    End Sub

    Private Sub grpSelectie_TextBox_MouseClick(sender As TextBox, e As EventArgs)
        '*** Als het geclickte veld wit is, stop
        If sender.BackColor = Color.White Then
            Exit Sub
        End If
        '*** Zoek het gepreviewde vak
        Dim tb As TextBox = GetHighestPlayerField(sender)
        '*** Als het zichzelf previewd, stop
        If tb.Name = sender.Name Then
            Exit Sub
        End If
        For Each textb As TextBox In grbSelectie.Controls
            textb.Enabled = False
        Next
        '*** Disc drop animatie
        endbox = tb
        AddDDisc(tb)
        '*** Verificatie data invullen
        tb.Text = " "

    End Sub

    Function GetLowestEmptyField(Beginbox As TextBox) As TextBox
        '*** Maak een variabelen aan om de coordinaten van de tekstvelden na te kijken
        Dim Name As String = Beginbox.Name.Split("Y")(0)
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

    Function GetHighestPlayerField(Beginbox As TextBox) As TextBox
        '*** Vind het laatste gespeelde vak
        Dim tb As TextBox = Beginbox
        Dim Name As String = Beginbox.Name.Split("Y")(0)
        '*** Vind het hoogste gekleurde vak (dit is her tijdelijk vak)
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
        '*** Als er geen tijdelijk vak is stuur het eigen vak terug
        Return Beginbox
    End Function

    Private Sub EndRound(PlayedField As TextBox)
        '*** Maken van variabele om mogelijk punt op te vangen
        Dim strPoint As String = PointCheck(PlayedField)
        '*** Pak het veld van sender
        Dim sender As TextBox
        For Each tb As TextBox In grbSelectie.Controls
            tb.Enabled = True
            If tb.Name.Split("Y")(0) = PlayedField.Name.Split("Y")(0) Then
                sender = tb
            End If
        Next
        '*** Kijken wie er een punt heeft gemaakt
        Select Case strPoint
            Case "Red"
                Exit Select
            Case "Yellow"
                Exit Select
            Case Else
                '*** Wissel speler kleur
                Beurtwissel()
                For Each tb As TextBox In grbSelectie.Controls
                    tb.Enabled = True
                Next
                Exit Sub
        End Select
        If (GameRun) Then
            GameRun = False
        End If
        '*** Wis alle veld inhouden
        For Each tb As TextBox In grbSelectie.Controls
            tb.Clear()
            tb.Enabled = False
        Next
        '*** Toon wie gewonnen heeft
        lblWinner.Text = strPoint & " Wins"
        '*** Simuleer een nieuwe enter event
        grpSelectie_TextBox_MouseEnter(sender, New EventArgs())
    End Sub

    Function PointCheck(PlayedField As TextBox) As String
        '*** Maken van variabelen om coördinaten en punt te managen
        Dim Point As String = "No Point"
        Dim PFCoords As String = PlayedField.Name.Split("X")(1)
        Dim PFNameX As String = PlayedField.Name.Split("X")(0) & "X"
        Dim PFNameY As String = PlayedField.Name.Split("Y")(0) & "Y"
        Dim PFX As Integer = CInt(PFCoords.Split("Y")(0))
        Dim PFY As Integer = CInt(PFCoords.Split("Y")(1))
        Dim Check(3, 6) As String
        Dim tb As TextBox
        '*** Verzamel alle veldkleuren die mogelijk punten kunnen zijn
        For i = PFX - 3 To PFX + 3 Step 1
            For Each obj As Object In grbVeld.Controls
                If TypeOf obj Is TextBox Then
                    tb = obj
                    '*** Horizontal check Check(x,0)
                    If tb.Name = PFNameX & CStr(i) & "Y" & PFY Then
                        Check(0, ((i - PFX) + 3)) = tb.Name
                    End If
                    '*** Diagonal check / Check(x,1)
                    If tb.Name = PFNameX & CStr(i) & "Y" & (PFY + (i - PFX)) Then
                        Check(1, ((i - PFX) + 3)) = tb.Name
                    End If
                    '*** Diagonal check \ Check(x,2)
                    If tb.Name = PFNameX & CStr(i) & "Y" & (PFY - (i - PFX)) Then
                        Check(2, ((i - PFX) + 3)) = tb.Name
                    End If
                End If
            Next
        Next
        '*** Vertical check Check(x,3)
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
        '*** Maak variabelen aan om de tijdelijke puntentelling te doen
        Dim CurColor As Color = txtBeurt.BackColor
        Dim Count As Integer = 0
        Dim Winner(6) As String
        '*** Kijk alle mogelijke velden na die een punt kunnen hebben gemaakt
        For i = 0 To 3 Step 1
            For j = 0 To 6 Step 1
                For Each obj As Object In grbVeld.Controls
                    If TypeOf obj Is TextBox Then
                        tb = obj
                        '*** Kijk of het veld in de mogelijke puntentelling hoort
                        If tb.Name = Check(i, j) Then
                            '*** Kijk of het veld de speler zijn kleur heeft
                            If tb.BackColor = CurColor Then
                                '*** Voeg het veld toe aan de punten telling en tel 1 punt op
                                Winner(Count) = tb.Name
                                Count += 1
                            Else
                                '*** Onderbroken puntentelling zet punten terug op 0
                                Count = 0
                            End If
                            '*** Als punten 4 of meer zijn duid aan waar de punten gemaakt is
                            If Count >= 4 Then
                                '*** Verander alle puntvelden naar groen
                                For k = 0 To Count - 1 Step 1
                                    For Each ob As Object In grbVeld.Controls
                                        If TypeOf ob Is TextBox Then
                                            tb = ob
                                            If tb.Name = Winner(k) Then
                                                tb.BackColor = Color.Green
                                            End If
                                        End If
                                    Next
                                Next
                                '*** Zet het punt naam
                                Point = CurColor.ToString.Split("]")(0).Split("[")(1)
                            End If
                        End If
                    End If
                Next
            Next
            Count = 0
        Next
        '*** Return het punt
        Return Point
    End Function

	Private Sub Beurtwissel()
		'*** Wissel de kleur van het beurtveld
		If txtBeurt.BackColor = Color.Yellow Then
			txtBeurt.BackColor = Color.Red
		Else
			txtBeurt.BackColor = Color.Yellow
		End If
	End Sub
End Class
