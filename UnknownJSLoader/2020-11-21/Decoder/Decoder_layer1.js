var Enc = **Enc_Data**; // push encoded data
var val_Init = **Init_Data**; //push value in argument for the increment
var val_In = **In_Data**; ; //push value in argument for the calc of index
var Sp = **Sp_Data**; // Separator for the split op
var val_SP =  **SP_Data**; // Argument of the function split
var val_Index = **Index_Data**; // Argument of the function Index
var val_lim = **lim_Data**; // Lim for loop
function GetChar(obj,c) {return obj.charAt(c);}
function f(inarg) {r=('');lim=p;while (lim < arg) {Bs12=GetChar(inarg,lim);if (modl(lim)) r=addval(r,Bs12,r); else r=addval(Bs12,r,Bs12); lim++; }return r;}
function modl(b) {return b % (m+m);}
function addval(c,d) { return c+d; }
function SplitData() {code = f(base).split(Sp);}
function GetIndex() {code[Index] = init[code[p]];}
function initVal(argp1)
{
  m=argp1;
  Index=m+argp1*m+argp1;
  arg=val_lim;
}
function init(a)
{
	base = Enc
	p=a;
}
init(val_Init);
initVal(val_In);
SplitData(val_SP);
GetIndex(val_Index);

console.log(code);
