var Enc = **Enc_Data**; // push encoded data
var val_Init = **Init_Data**; //push value in argument for the increment
function GetChar(obj,c) {return obj.charAt(c);}
function modl(b) {return b % (m+m);}
function addval(c,d) { return c+d; }
function f(inarg) {r=('');lim=val_Init;while (lim < arg) {Bs12=GetChar(inarg,lim);if (modl(lim)) r=addval(r,Bs12,r); else r=addval(Bs12,r,Bs12); lim++; }return r;}
var code = f(Enc);

console.log(code);
