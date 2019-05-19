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
    private String usersEmail;

    private double mouseX;
    private double mouseY;

    private String[] productArr = {};

    public LoginController(String email) throws JSONException{

            this.usersEmail = email;
            ListView productList = new ListView();

            seeCart = new Button("Checkout");
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

            Label emailLabel = new Label("Email: ");

            Label instructLabel = new Label("Sign In to view cart and orders");

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

            grid.add(instructLabel, 18, 0, 1, 2);

            grid.add(emailLabel, 16, 2, 1, 2);

            grid.add(emailField, 18, 2, 2, 2);

            grid.add(enterButton, 20, 2, 1, 2);

            grid.add(scrollPane, 0, 5, 2, 5);

            grid.add(seeCart, 0, 0, 1 ,2);

            //grid.add(test, 1, 5, 5, 5);

            productList.setOnMouseMoved(new EventHandler<MouseEvent>() {
                @Override
                public void handle(MouseEvent event) {
                    mouseX = event.getScreenX();
                    mouseY = event.getScreenY();
                }
            });

            Label blankLabel = new Label("No content");

         /** //PROBLEM: popOver shows up blank for every item clicked in the listView.***************
         * //PROBLEM 2: WHEN USING THE SELECTION METHOD IN THE JAVA TEXT BOOK, PG. 650,
          *             Integer i is suppose to be an Object i,
          *             but interator for desgrid[] should be int or Integer.
         * THIS METHOD SHOULD ALLOW US TO DISPLAY THE CONTENTS OF POPOVER
         * WHICH CHANGES FOR DIFFERENT SELECTED ITEMS IN THE LIST VIEW.
         */


            productList.getSelectionModel().selectedItemProperty().addListener( ov -> {

                /**
                popUp.setContentNode(blankLabel);
                ObservableList<Integer> test = productList.getSelectionModel().getSelectedIndices();
                for(Integer i: test) {
                    popUp.setContentNode(desgrid[i]);
                }

 */             //test.getChildren().add(desgrid[productList.getSelectionModel().getSelectedIndex()]);

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

    public void addToCart(Product product, int qty) {

    }


}