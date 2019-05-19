package Messages.RESTobjects;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import static java.lang.String.join;

class RESTPostOperation extends AbstractRESTOperation {

    RESTPostOperation() {}

    @Override
    public String AbstractExecute() {
        String result = null;

        if(GetBody() == null) {
            throw new IllegalStateException("Cannot make a POST request with no body");
        }

        DebugMsg("POST to " + GetURI());
        DebugMsg("  body: " + GetBody());

        try
        {
            // Some of this code has been adapted from
            // https://www.mkyong.com/webservices/jax-rs/restfull-java-client-with-java-net-url/
            URL url = new URL(GetURI());
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json");

            OutputStream os = conn.getOutputStream();
            os.write(GetBody().getBytes());
            os.flush();

            if(conn.getResponseCode() != HttpURLConnection.HTTP_CREATED) {
                throw new RuntimeException("Failed : REST error code : "
                    + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(
                    conn.getInputStream()
            ));

            result = "";
            DebugMsg("Output from Server .... \n");
            String line = br.readLine();
            while(line != null) {
                result = String.join("\n", result, line);
                line = br.readLine();
            }

            conn.disconnect();

        } catch (MalformedURLException e) {

            e.printStackTrace();
            result = null;

        } catch (IOException e) {

            e.printStackTrace();
            result = null;

        }

        return result;
    }

}
