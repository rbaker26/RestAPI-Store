import Messages.CartHandler;
import Messages.ProductHandler;
import UI.CartController;
import UI.LoginController;
import UI.RealLogin;
import data.CartUpdate;
import data.ProductUpdate;
import data.RemoveCartUpdate;
import javafx.application.Application;
import javafx.stage.Stage;

public class Client extends Application {

    private static final double initWidth = 800;
    private static final double initHeight = 600;
    private String userEmail = "helloitsme@adele.com";

    private LoginController loginController;
    private CartController cartController;
    private RealLogin realLogin;



    @Override
    public void start(Stage primaryStage) throws Exception {
        System.out.println("*******************************************************************");
        System.out.println("Get Request Test");
//        ProductHandler ph = new ProductHandler();
//        ph.GetProducts();

        System.out.println("*******************************************************************");

//        String jsonStr = "[{\"productId\":22,\"quantityToBeRemoved\":645},{\"productId\":34,\"quantityToBeRemoved\":100},{\"productId\":99,\"quantityToBeRemoved\":10},{\"productId\":63,\"quantityToBeRemoved\":69},{\"productId\":69,\"quantityToBeRemoved\":69}]";
//       // String jsonStr = "{\"productId\":22,\"quantityToBeRemoved\":645}";
//        Gson gson = new Gson();
//        Type collectionType = new TypeToken<ArrayList<ProductUpdate>>(){}.getType();
//        ArrayList<ProductUpdate> productUpdate = gson.fromJson(jsonStr, collectionType);
//        //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);
//        System.out.println("**********************************");
//        System.out.println(productUpdate);
//        System.out.println("**********************************");

        //PUT THIS INSIDE THE LOGINCONTROLLER AND CARTCONTROLLER CONSTRUCTOR CALL



        try {
            primaryStage.setTitle("SABRCATST RestAPI-Store");

            primaryStage.setHeight(initHeight);
            primaryStage.setWidth(initWidth);
            loginController = new LoginController();
            cartController = new CartController();


            realLogin = new RealLogin();

            userEmail = realLogin.getEmailField();

            realLogin.getEnterButton().setOnAction(value -> {
                userEmail = realLogin.getEmailField();
                if(!userEmail.equals("")) {
                    loginController.setEmail(userEmail);
                    cartController.setEmail(userEmail);
                    loginController.applyScene(primaryStage);
                }
                else {
                    System.out.println("Please enter email");
                }
            });

            /**
             * THIS IS WHERE THE CUSTOMER GETS TO VIEW THEIR CART
             * ORDERS. GETPURCHASE FUNCTION SHOULD FILL THE ARRAYLIST
             * WITH CART ITEMS. JSON CODE SHOULD GIVE US BACK AN ARRAYLIST.
             */
            loginController.getSeeCart().setOnAction(value -> {

                cartController.getPurchases();
                cartController.applyScene(primaryStage);
            });

            cartController.getBackButton().setOnAction(value -> {
                loginController.applyScene(primaryStage);
            });

            loginController.getSignOut().setOnAction(value -> {
                realLogin.applyScene(primaryStage);
            });


            // System.out.println("\n\n****************** IT WORKS ******************\n\n");

            realLogin.applyScene(primaryStage);
            primaryStage.show();

        } catch (Exception ex) {
            // This is so that we get better information on exceptions. By default,
            // JavaFX swallows the exception and closes without saying anything useful.
            ex.printStackTrace();
            throw ex;
        }


    }


}