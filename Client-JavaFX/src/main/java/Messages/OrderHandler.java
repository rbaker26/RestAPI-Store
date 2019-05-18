package Messages;

import com.google.gson.reflect.TypeToken;
import data.Order;
import data.Product;

import java.util.ArrayList;

public final class OrderHandler {

    @Deprecated // This is a static class; it should not be constructed
    public OrderHandler() {}

    public static ArrayList<Order> GetOrders(String email) {

        return REST.Get()
                .URI(RESTConfig.ordersURI)
                .URIPostfix(email)
                .ExecuteAndConvert(
                        new TypeToken<ArrayList<Order>>() { }.getType()
                );
    }
}
