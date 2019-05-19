package Messages.RESTobjects;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

class RESTGetOperation extends AbstractRESTOperation {

    RESTGetOperation () {}

    @Override
    public void SetupConnection(HttpURLConnection conn) throws java.io.IOException {

        DebugMsg("GET to " + GetURI());

        conn.setRequestMethod("GET");
        conn.setRequestProperty("Accept", "application/json");
    }

}
