package UI;

import Messages.CartHandler;
import Messages.ProductHandler;
import data.CartUpdate;
import data.Gridclass;
import data.Product;
import data.ProductUpdate;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.Pane;
import org.controlsfx.control.PopOver;
import org.json.JSONException;

import java.util.ArrayList;

public class LoginController extends AbstractSceneController {

    private static final double popupOffsetX = 10;
    private static final double popupOffsetY = 5;

    private TextField emailField;
    private TextField quantityField;
    private Button enterButton;
    private Button addToCartButton;
    private Button seeCart;
    private Button signOut;
    private String usersEmail;

    private double mouseX;
    private double mouseY;

    private String[] productArr = {};

    public LoginController(String email) throws JSONException{

            this.usersEmail = email;
            ListView productList = new ListView();


            seeCart = new Button("Go To My Cart");
            signOut = new Button("Sign Out");

            seeCart.setStyle("-fx-font-weight: bold");
            signOut.setStyle("-fx-font-weight: bold");

        /**
         * USING GSON FOR JSON STRING
         */
        ArrayList<Product> productUpdate = ProductHandler.GetProducts();


        /**
         * PRINTING OUT ALL THE NAMES OF PRODUCTS GIVEN
         */

        for(int i = 0; i < productUpdate.size(); i++) {
            System.out.println(productUpdate.get(i).description);

            productList.getItems().addAll(productUpdate.get(i).description);

        }

        System.out.println();


        /**
         * GRIDPANE DECLARATION FOR FIRST WINDOW
         */
            GridPane grid = new GridPane();


            productList.setPrefSize(400, 400);

            //  grid.setPadding(new Insets(10, 10, 10, 10));
            grid.setHgap(3);
            grid.setVgap(3);
            grid.setAlignment(Pos.CENTER);
            grid.setStyle("-fx-background-color: linear-gradient(to bottom, #000066 44%, #9999ff 100%)");

            Label emailLabel = new Label("Email: ");

           // Label instructLabel = new Label("Hello " + this.usersEmail);
          //  Label instructLabel2 = new Label("Select a product and type in how many");
           // Label instructLabel3 = new Label("you would like to add to your cart.");

            Label greetLabel = new Label("Hello and Welcome!" + "\n" +
                                        "Your email is " + this.usersEmail + "\n" +
                                        "Select a product and type in how many" + "\n" +
                                        "you would like to add to your cart");

            greetLabel.setStyle("-fx-font: normal bold 13px 'arial'; -fx-text-fill: white");

            emailField = new TextField();
            emailField.setPrefColumnCount(10);

        /**
         * POPOVER DECLARATION
          */
            PopOver popUp = new PopOver();

            popUp.setTitle("Product Information");
            popUp.setArrowLocation(PopOver.ArrowLocation.LEFT_CENTER);
            popUp.setAutoHide(false);
            popUp.setDetachable(true);

            enterButton = new Button("Sign In");


        /**
         * AN ARRAY OF GRIDPANE AND GRIDPANE CONTENT IS CREATED.
         * DEPENDANT ON THE SIZE AND CONTENT OF JSON STRING.
          */

        Gridclass[] desgrid = new Gridclass[productUpdate.size()];

        for(int i = 0; i < productUpdate.size(); i++) {
            desgrid[i] = new Gridclass(productUpdate.get(i).productId, this.usersEmail);
            desgrid[i].setDescriptionObj(productUpdate.get(i).description);
            desgrid[i].setPriceObj(productUpdate.get(i).price);
            desgrid[i].setQuantityObj(productUpdate.get(i).quantity);
        }

        /**
         * THIS CONSOLE OUTPUT ALLOWS US TO SEE THAT THE CONTENT OF THE
         * GRIDPANE AND SPECIFIC LABELS HAVE BEEN SUCCESSFULLY INITIALIZED
         * WITH THE JSON STRING CONTENTS.
         */

        for(int i = 0; i < productUpdate.size(); i++) {
            System.out.println(desgrid[i].getDescriptionObj());
            System.out.println(desgrid[i].getPriceObj());
            System.out.println(desgrid[i].getQuantityObj());
        }


            //popUp.setContentNode(desGrid);
            Pane test = new Pane();
            ScrollPane scrollPane = new ScrollPane(productList);

            grid.add(signOut, 16, 0, 1, 2);

           // grid.add(instructLabel2, 16, 2, 1, 2);

           // grid.add(instructLabel3, 16, 4, 2, 2);

           // grid.add(enterButton, 20, 2, 1, 2);

            grid.add(scrollPane, 0, 5, 2, 5);

            grid.add(seeCart, 2, 0, 1 ,2);

            grid.add(greetLabel,0, 0, 1, 2);


            productList.setOnMouseMoved(new EventHandler<MouseEvent>() {
                @Override
                public void handle(MouseEvent event) {
                    mouseX = event.getScreenX();
                    mouseY = event.getScreenY();
                }
            });

            Label blankLabel = new Label("No content");


            productList.getSelectionModel().selectedItemProperty().addListener( ov -> {


                popUp.setContentNode(desgrid[productList.getSelectionModel().getSelectedIndex()]);
                popUp.show(grid, mouseX + popupOffsetX, mouseY + popupOffsetY);


            });


            setRoot(grid);

        }



    public Button getEnterButton() {
        return enterButton;
    }

    public String getEmailField() {
        return emailField.getText();
    }

    public void clearField() {
        emailField.clear();
    }

    public void setEmailField(TextField emailField) {
        this.emailField = emailField;
    }

    public Button getSeeCart() {
        return seeCart;
    }

    public Button getSignOut() {
        return signOut;
    }




}