package Messages;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import data.*;

import java.io.*;
import java.lang.reflect.Type;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Arrays;

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

    public static ArrayList<ProductUpdate> PurchaseCart(String email)
    {

        System.out.println("In request body");
        try
        {
            URL url = new URL("http://68.5.123.182:5002/api/cart/" + email);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            conn.setRequestProperty("Accept", "application/json");

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Failed : HTTP error code : "
                        + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(
                    (conn.getInputStream())));

            String output;
            System.out.println("Output from Server .... \n");
            String json= "";
            while ((output = br.readLine()) != null) {
                System.out.println(output);
                json = output;
            }
            System.out.println(output);
            System.out.println("json " + json);
            conn.disconnect();

            Gson gson = new Gson();
            /**
             * once bobby is able to pull the request, comment out the Cart cart = gson.... line of code
             * and UNCOMMENT Type collectionType, and ArrayList<ProductUpdate> productUpdate
             */
            Type collectionType = new TypeToken<ArrayList<ProductUpdate>>(){}.getType();
            ArrayList<ProductUpdate> productUpdate = gson.fromJson(json, collectionType);
            System.out.println("**********&&&&&&&&&&&&&&&&&&&&&&&&&&************************");
            System.out.println(productUpdate);
            System.out.println("**********************************");

            return productUpdate;

        } catch (MalformedURLException e) {

            e.printStackTrace();

        } catch (IOException e) {

            e.printStackTrace();

        }
        return null;
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
