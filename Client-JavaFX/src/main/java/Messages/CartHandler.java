package Messages;

import com.google.gson.Gson;
import data.CartUpdate;
import data.RemoveCartUpdate;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;

public class CartHandler {

    public static boolean SendCartUpdate(CartUpdate cartUpdate)
    {
        try {
            URL url = new URL("http://68.5.123.182:5002/api/cart/");
            Gson gson = new Gson();
            HttpURLConnection conn = (HttpURLConnection)url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json");
            String input = gson.toJson(cartUpdate);

            OutputStream os = conn.getOutputStream();
            os.write(input.getBytes());
            os.flush();

            if (conn.getResponseCode() != HttpURLConnection.HTTP_CREATED) {
                throw new RuntimeException("Failed : HTTP error code : "
                        + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(
                    (conn.getInputStream())));

            String output;
            System.out.println("Output from Server .... \n");
            while ((output = br.readLine()) != null) {
                System.out.println(output);
            }

            conn.disconnect();
        }
        catch(RuntimeException ex) {
            ex.printStackTrace();
            return false;
        }
        catch(Exception ex) {
            ex.printStackTrace();
            return false;
        }
        return true;
    }

    public static boolean  PurchaseCart(String email)
    {
        try {
            URL url = new URL("http://68.5.123.182:5002/api/cart/" + email);
            HttpURLConnection conn = (HttpURLConnection)url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("PUT");
            OutputStreamWriter out = new OutputStreamWriter(conn.getOutputStream());
            out.write("");
            out.close();
            if(conn.getResponseCode() != HttpURLConnection.HTTP_OK) {
                throw new Exception("Failed purchase..." + conn.getResponseCode());
            }

            conn.disconnect();
        }
        catch(Exception ex) {
            return false;
        }
        return true;
    }

    public static boolean RemoveCartUpdate(RemoveCartUpdate removeCartUpdate)
    {
        try {
            URL url = new URL("http://68.5.123.182:5002/api/cart/");
            Gson gson = new Gson();
            HttpURLConnection conn = (HttpURLConnection)url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("DELETE");
            conn.setRequestProperty("Content-Type", "application/json");
            String input = gson.toJson(removeCartUpdate);

            OutputStream os = conn.getOutputStream();
            os.write(input.getBytes());
            os.flush();

            if (conn.getResponseCode() != HttpURLConnection.HTTP_NO_CONTENT) {
                throw new RuntimeException("Failed : HTTP error code : "
                        + conn.getResponseCode());
            }

            conn.disconnect();
        }
        catch(RuntimeException ex) {
            ex.printStackTrace();
            return false;
        }
        catch(Exception ex) {
            ex.printStackTrace();
            return false;
        }
        return true;
    }


}
