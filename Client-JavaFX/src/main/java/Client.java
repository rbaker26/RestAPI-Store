import com.google.gson.reflect.TypeToken;
import data.ProductUpdate;
import javafx.stage.Stage;
import javafx.application.Application;
import javafx.application.Platform;
import javafx.scene.control.Alert;
import UI.*;
import com.google.gson.*;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Collection;


public class Client extends Application {

    private static final double initWidth = 800;
    private static final double initHeight = 600;

    private LoginController loginController;
    private CartController cartController;


    @Override
    public void start(Stage primaryStage) throws Exception {

//        String jsonStr = "[{\"productId\":22,\"quantityToBeRemoved\":645},{\"productId\":34,\"quantityToBeRemoved\":100},{\"productId\":99,\"quantityToBeRemoved\":10},{\"productId\":63,\"quantityToBeRemoved\":69},{\"productId\":69,\"quantityToBeRemoved\":69}]";
//       // String jsonStr = "{\"productId\":22,\"quantityToBeRemoved\":645}";
//        Gson gson = new Gson();
//        Type collectionType = new TypeToken<ArrayList<ProductUpdate>>(){}.getType();
//        ArrayList<ProductUpdate> productUpdate = gson.fromJson(jsonStr, collectionType);
//        //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);
//        System.out.println("**********************************");
//        System.out.println(productUpdate);
//        System.out.println("**********************************");



        try {
            primaryStage.setTitle("SABRCATST RestAPI-Store");

            primaryStage.setHeight(initHeight);
            primaryStage.setWidth(initWidth);

            loginController = new LoginController();
            cartController = new CartController();


            loginController.getEnterButton().setOnAction(value -> {
                System.out.println("Entering with email...");
            });

            // System.out.println("\n\n****************** IT WORKS ******************\n\n");

            loginController.applyScene(primaryStage);
            primaryStage.show();

        } catch (Exception ex) {
            // This is so that we get better information on exceptions. By default,
            // JavaFX swallows the exception and closes without saying anything useful.
            ex.printStackTrace();
            throw ex;
        }


    }


}