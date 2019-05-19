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
    public void SetupConnection(HttpURLConnection conn) throws java.io.IOException {

        if(GetBody() == null) {
            throw new IllegalStateException("Cannot make a POST request with no body");
        }

        DebugMsg("POST to " + GetURI());
        DebugMsg("  body: " + GetBody());

        conn.setDoOutput(true);
        conn.setRequestMethod("POST");
        conn.setRequestProperty("Content-Type", "application/json");

        OutputStream os = conn.getOutputStream();
        os.write(GetBody().getBytes());
        os.flush();
    }

}
