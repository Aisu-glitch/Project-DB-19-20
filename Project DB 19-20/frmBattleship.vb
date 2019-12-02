Public Class frmBattleship
    Dim backupmap
    Private Sub frmBattleship_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim map As Bitmap = New Bitmap(pbxTest.Image, pbxTest.Width, pbxTest.Height)
        Dim Pth As New System.Drawing.Drawing2D.GraphicsPath
        Dim Reg As Region
        For J = 1 To pbxTest.Height - 1
            For I = 1 To pbxTest.Width - 1
                If map.GetPixel(I, J).ToArgb <> 0 Then
                    Pth.AddRectangle(New Rectangle(I, J, 1, 1))
                End If
            Next
        Next
        Reg = New Region(Pth)
        pbxTest.Region = Reg
        pbxTest.AllowDrop = True
        PictureBox1.AllowDrop = True
        For Each box As PictureBox In Me.Controls
            AddHandler box.DragEnter, AddressOf dd_enter
            AddHandler box.DragLeave, AddressOf dd_leave
            AddHandler box.MouseDown, AddressOf pbxTest_Click
        Next

    End Sub

    Private Sub pbxTest_Click(sender As PictureBox, e As EventArgs)
        If sender.Image IsNot Nothing Then
            sender.DoDragDrop(New Bitmap(sender.Image), DragDropEffects.Move)
        End If
    End Sub

    Private Sub dd_enter(sender As PictureBox, e As DragEventArgs)
        If (e.Data.GetDataPresent(DataFormats.Bitmap)) Then
            e.Effect = DragDropEffects.Move
            If sender.Image Is Nothing Then
                sender.Image = e.Data.GetData(DataFormats.Bitmap)
            End If
        End If
    End Sub

    Private Sub dd_leave(sender As PictureBox, e As EventArgs)
        sender.Image = Nothing
    End Sub

    'https://stackoverflow.com/questions/51560830/show-dragged-item-while-dragging-vb-net
    'http://www.vbforums.com/showthread.php?262883-Determining-the-source-object-in-a-drag-and-drop

End Class