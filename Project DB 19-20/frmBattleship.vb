Public Class frmBattleship
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
    End Sub

    Private Sub pbxTest_Click(sender As PictureBox, e As EventArgs) Handles pbxTest.MouseDown, PictureBox1.MouseDown
        If sender.Image IsNot Nothing Then
            sender.DoDragDrop(New Bitmap(sender.Image), DragDropEffects.Move)
        End If
    End Sub

    Private Sub PictureBox1_dd_enter(sender As PictureBox, e As DragEventArgs) Handles PictureBox1.DragEnter, pbxTest.DragEnter
        If (e.Data.GetDataPresent(DataFormats.Bitmap)) Then
            '---determine if this is a copy or move---
            e.Effect = DragDropEffects.Move
            sender.Image = e.Data.GetData(DataFormats.Bitmap)
        End If
    End Sub

    Private Sub PictureBox1_dd_leave(sender As PictureBox, e As EventArgs) Handles PictureBox1.DragLeave, pbxTest.DragLeave
        sender.Image = Nothing
    End Sub


End Class