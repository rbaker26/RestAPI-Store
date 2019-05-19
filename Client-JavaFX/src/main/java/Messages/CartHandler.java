package Messages;

import Messages.RESTobjects.REST;
import Messages.RESTobjects.RESTConfig;
import data.CartUpdate;
import data.ProductUpdate;

public final class CartHandler {
    private CartHandler() {}

    public static void AddToCart(CartUpdate update) {
        //CartUpdate update = new CartUpdate("Yahoo@gmail.com", new ProductUpdate(3, 2));

        //System.out.println(Converter.toJson(update).toString());

        REST.Post()
                .Verbose()
                .URI(RESTConfig.cartURI)
                .Body(Converter.toJson(update).toString())
                .Execute();

    }
}
