package Messages.RESTobjects;

public final class RESTConfig {

    private RESTConfig() {}

    public static final String masterHostIP = "68.5.123.182";
    public static final String localhostIP = "localhost";

    /**
     * Consider this the default host IP.
     */
    public static final String hostIP = masterHostIP;

    public static final String ordersURI = "http://" + hostIP + ":5000/api/orders";
    public static final String productsURI = "http://" + hostIP + ":5001/api/products";
    public static final String cartURI = "http://" + hostIP + ":5002/api/cart";


}

