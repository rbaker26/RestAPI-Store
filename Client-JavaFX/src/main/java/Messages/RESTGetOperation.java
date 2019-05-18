package Messages;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class RESTGetOperation extends AbstractRESTOperation {

    /*
    String urlString;

    RESTGetOperation(String urlString) {
        //"http://68.5.123.182:5001/api/products/");
        this.urlString = urlString;
    }
     */

    @Override
    public String AbstractExecute() {
        String result = null;

        System.out.println("In request body");
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
            System.out.println("Output from Server .... \n");
            String json= "";
            while ((output = br.readLine()) != null) {
                System.out.println(output);
                json = output;
            }
            System.out.println(output);
            System.out.println(json);
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
