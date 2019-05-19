package Messages;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import data.Product;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.lang.reflect.Type;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;

public class ProductHandler {

    public static ArrayList<Product> GetProducts()
    {
        System.out.println("In request body");
        try
        {
            URL url = new URL("http://68.5.123.182:5001/api/products/");
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
            System.out.println(json);
            conn.disconnect();


                   // String jsonStr = "[{\"productId\":22,\"quantityToBeRemoved\":645},{\"productId\":34,\"quantityToBeRemoved\":100},{\"productId\":99,\"quantityToBeRemoved\":10},{\"productId\":63,\"quantityToBeRemoved\":69},{\"productId\":69,\"quantityToBeRemoved\":69}]";
       // String jsonStr = "{\"productId\":22,\"quantityToBeRemoved\":645}";
        Gson gson = new Gson();
        Type collectionType = new TypeToken<ArrayList<Product>>(){}.getType();
        ArrayList<Product> productUpdate = gson.fromJson(json, collectionType);
        //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);
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

    public static Product GetProduct(int productId) {
        try
        {
            URL url = new URL("http://68.5.123.182:5001/api/products/" + productId);
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
            System.out.println(json);
            conn.disconnect();

            Gson gson = new Gson();
            Product product = gson.fromJson(json, Product.class);
            //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);

            return product;

        } catch (MalformedURLException e) {

            e.printStackTrace();

        } catch (IOException e) {

            e.printStackTrace();

        }
        return null;
    }

}
