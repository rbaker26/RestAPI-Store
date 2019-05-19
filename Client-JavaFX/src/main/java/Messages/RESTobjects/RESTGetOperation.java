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
    public String AbstractExecute() {
        String result = null;

        DebugMsg("GET to " + GetURI());
        try
        {
            URL url = new URL(GetURI());
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            conn.setRequestProperty("Accept", "application/json");

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Failed : REST error code : "
                        + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(
                    conn.getInputStream()
            ));

            String output;
            DebugMsg("Output from Server .... \n");
            String json= "";
            while ((output = br.readLine()) != null) {
                //System.out.println(output);
                json += output;
            }
            //System.out.println(output);
            DebugMsg(json);
            conn.disconnect();

            result = json;

        } catch (MalformedURLException e) {

            e.printStackTrace();

        } catch (IOException e) {

            e.printStackTrace();

        }

        return result;
    }

}
