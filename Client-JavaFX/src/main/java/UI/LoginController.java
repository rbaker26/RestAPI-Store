package UI;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import data.Gridclass;
import data.ProductUpdate;
import data.Product;
import javafx.beans.InvalidationListener;
import javafx.beans.Observable;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.ObservableList;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import org.controlsfx.control.PopOver;
import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Collection;

public class LoginController extends AbstractSceneController {

    private static final double popupOffsetX = 10;
    private static final double popupOffsetY = 5;

    private TextField emailField;
    private TextField quantityField;
    private Button enterButton;
    private Button addToCartButton;

    private double mouseX;
    private double mouseY;

    private String[] productArr = {};

    public LoginController() throws JSONException{


            ListView productList = new ListView();

        /**
         * USING GSON FOR JSON STRING
         */
        String jsonStr = "[{\"ProductId\":1,\"Description\":\"hammer\",\"Quantity\":46,\"Price\":4.5}," +
                "{\"ProductId\":2,\"Description\":\"box\",\"Quantity\":2,\"Price\":99.89},{\"ProductId\":3," +
                "\"Description\":\"C#\",\"Quantity\":3,\"Price\":50.0},{\"ProductId\":4,\"Description\":\"Java\"," +
                "\"Quantity\":1,\"Price\":0.02}]";

        Gson gson = new Gson();

        Type collectionType = new TypeToken<ArrayList<Product>>(){}.getType();
        ArrayList<Product> productUpdate = gson.fromJson(jsonStr, collectionType);
        //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);
        System.out.println("**********************************");
        System.out.println(productUpdate);
        System.out.println("**********************************");


        /**
         * PRINTING OUT ALL THE NAMES OF PRODUCTS GIVEN
         */

        for(int i = 0; i < productUpdate.size(); i++) {
            System.out.println(productUpdate.get(i).Description);

            productList.getItems().addAll(productUpdate.get(i).Description);

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
            desgrid[i] = new Gridclass();
            desgrid[i].setDescriptionObj(productUpdate.get(i).Description);
            desgrid[i].setPriceObj(productUpdate.get(i).Price);
            desgrid[i].setQuantityObj(productUpdate.get(i).Quantity);
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

            ScrollPane scrollPane = new ScrollPane(productList);

            grid.add(instructLabel, 18, 0, 1, 2);

            grid.add(emailLabel, 16, 2, 1, 2);

            grid.add(emailField, 18, 2, 2, 2);

            grid.add(enterButton, 20, 2, 1, 2);

            grid.add(scrollPane, 0, 5, 2, 5);



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

 */
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



}