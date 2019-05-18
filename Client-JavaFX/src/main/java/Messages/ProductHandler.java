package Messages;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import data.Order;
import data.Product;

import java.io.IOException;
import java.lang.reflect.Type;
import java.net.MalformedURLException;
import java.util.ArrayList;

public final class ProductHandler {

    private ProductHandler() {}

    public static ArrayList<Product> GetProducts()
    {
        return REST.Get()
                .URI(RESTConfig.productsURI)
                .ExecuteAndConvert(
                        new TypeToken<ArrayList<Product>>() { }.getType()
                );
    }


}
