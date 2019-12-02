Public Class frmMenu
    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

        Dim bt As Button
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
    End Sub

    Private Sub btnBConnect4_Click(sender As Object, e As EventArgs)
        frmConnect4.Show()
    End Sub

    Private Sub BtnBattleship_Click(sender As Object, e As EventArgs)
        frmBattleship.Show()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class