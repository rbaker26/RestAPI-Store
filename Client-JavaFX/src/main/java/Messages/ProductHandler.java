package Messages;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import data.Product;

import java.io.IOException;
import java.lang.reflect.Type;
import java.net.MalformedURLException;
import java.util.ArrayList;

public class ProductHandler {

    public static ArrayList<Product> GetProducts()
    {
        ArrayList<Product> result = null;

        System.out.println("In request body");

        String json = REST.Get().URI(RESTConfig.productsURI).Execute();

        if(json != null) {
            Gson gson = new Gson();
            Type collectionType = new TypeToken<ArrayList<Product>>() { }.getType();
            result = gson.fromJson(json, collectionType);
            //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);
            System.out.println("**********&&&&&&&&&&&&&&&&&&&&&&&&&&************************");
            System.out.println(result);
            System.out.println("**********************************");

            //return productUpdate;
        }

        return result;
    }
}
