package data;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class Order {

    // TODO WHY ARE THESE ALL PUBLIC!???
    public int orderID;
    public String email;
    public long timeStamp;
    public List<ProductUpdate> shoppingCart;

    public Order() {
        this(0, "", 0);
    }

    public Order(int orderID, String email, long timeStamp) {
        this.orderID = orderID;
        this.email = email;
        this.timeStamp = timeStamp;

        shoppingCart = new ArrayList<ProductUpdate>();
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Order order = (Order) o;
        return orderID == order.orderID &&
                timeStamp == order.timeStamp &&
                Objects.equals(email, order.email) &&
                Objects.equals(shoppingCart, order.shoppingCart);
    }

    @Override
    public int hashCode() {
        return Objects.hash(orderID, email, timeStamp, shoppingCart);
    }

    @Override
    public String toString() {
        return "Order{" +
                "orderID=" + orderID +
                ", email='" + email + '\'' +
                ", timeStamp=" + timeStamp +
                ", shoppingCart=" + shoppingCart +
                '}';
    }

    public static void TestJSONConversion() {

        String orderSrcJSON = "[{\"orderID\":5,\"email\":\"007dsi@gmail.com\",\"timeStamp\":0,\"shoppingCart\":[{\"productId\":1,\"quantityToBeRemoved\":11},{\"productId\":2,\"quantityToBeRemoved\":22},{\"productId\":3,\"quantityToBeRemoved\":33},{\"productId\":4,\"quantityToBeRemoved\":44},{\"productId\":5,\"quantityToBeRemoved\":55},{\"productId\":6,\"quantityToBeRemoved\":66}]},{\"orderID\":6,\"email\":\"007dsi@gmail.com\",\"timeStamp\":0,\"shoppingCart\":[{\"productId\":1,\"quantityToBeRemoved\":11},{\"productId\":2,\"quantityToBeRemoved\":22},{\"productId\":3,\"quantityToBeRemoved\":33},{\"productId\":4,\"quantityToBeRemoved\":44},{\"productId\":5,\"quantityToBeRemoved\":55},{\"productId\":6,\"quantityToBeRemoved\":66}]},{\"orderID\":7,\"email\":\"007dsi@gmail.com\",\"timeStamp\":0,\"shoppingCart\":[{\"productId\":1,\"quantityToBeRemoved\":11},{\"productId\":2,\"quantityToBeRemoved\":22},{\"productId\":3,\"quantityToBeRemoved\":33},{\"productId\":4,\"quantityToBeRemoved\":44},{\"productId\":5,\"quantityToBeRemoved\":55},{\"productId\":6,\"quantityToBeRemoved\":66}]},{\"orderID\":8,\"email\":\"007dsi@gmail.com\",\"timeStamp\":0,\"shoppingCart\":[{\"productId\":1,\"quantityToBeRemoved\":11},{\"productId\":2,\"quantityToBeRemoved\":22},{\"productId\":3,\"quantityToBeRemoved\":33},{\"productId\":4,\"quantityToBeRemoved\":44},{\"productId\":5,\"quantityToBeRemoved\":55},{\"productId\":6,\"quantityToBeRemoved\":66}]},{\"orderID\":9,\"email\":\"007dsi@gmail.com\",\"timeStamp\":0,\"shoppingCart\":[]},{\"orderID\":10,\"email\":\"007dsi@gmail.com\",\"timeStamp\":0,\"shoppingCart\":[]}]";
        Gson gson = new Gson();
        Type collectionType = new TypeToken<ArrayList<Order>>(){}.getType();
        ArrayList<Order> orders = gson.fromJson(orderSrcJSON, collectionType);
        //Order o = Converter.fromJson(orderSrcJSON, Order.class);
        //System.out.println()
        for(Order o : orders) {
            System.out.println(o);
        }

    }
}
