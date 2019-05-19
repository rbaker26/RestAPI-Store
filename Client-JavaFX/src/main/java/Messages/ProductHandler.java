package Messages;

import Messages.RESTobjects.REST;
import Messages.RESTobjects.RESTConfig;
import com.google.gson.reflect.TypeToken;
import data.Product;

import java.util.ArrayList;

public final class ProductHandler {

    private ProductHandler() {}

    public static ArrayList<Product> GetProducts()
    {
        return REST.Get()
                .URI(RESTConfig.productsURI)
                //.Verbose()
                .ExecuteAndConvert(
                        new TypeToken<ArrayList<Product>>() { }.getType()
                );
    }


}
