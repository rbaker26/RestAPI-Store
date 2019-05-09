import javafx.stage.Stage;
import javafx.application.Application;
import javafx.application.Platform;
import javafx.scene.control.Alert;
import UI.*;

public class Client extends Application {

    private static final double initWidth = 800;
    private static final double initHeight = 600;

    private LoginController loginController;

    @Override
    public void start(Stage primaryStage) throws Exception {



        try {
            primaryStage.setTitle("SABRCATST RestAPI-Store");

       loginController = new LoginController();

        loginController.getEnterButton().setOnAction(value -> {
            System.out.println("Entering with email...");
        });

           // System.out.println("\n\n****************** IT WORKS ******************\n\n");

            loginController.applyScene(primaryStage);
            primaryStage.setWidth(initHeight);
            primaryStage.setHeight(initHeight);
            primaryStage.show();

        }

        catch(Exception ex) {
            // This is so that we get better information on exceptions. By default,
            // JavaFX swallows the exception and closes without saying anything useful.
            ex.printStackTrace();
            throw ex;
        }


    }


}