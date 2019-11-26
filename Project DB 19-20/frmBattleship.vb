Public Class frmBattleship
	Private Sub frmBattleship_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim map As Bitmap = pbxTest.Image
		Dim Pth As New System.Drawing.Drawing2D.GraphicsPath
		Dim Reg As Region
		For I = 1 To pbxTest.Image.Width - 1
			For J = 1 To pbxTest.Image.Height - 1
				If map.GetPixel(I, J).ToArgb <> 0 Then
					Pth.AddRectangle(New Rectangle(I, J, 1, 1))
				End If
			Next
		Next
		Reg = New Region(Pth)
		pbxTest.Region = Reg
	End Sub
End Class