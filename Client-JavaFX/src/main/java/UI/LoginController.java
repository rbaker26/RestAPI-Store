package UI;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import data.ProductUpdate;
import javafx.beans.InvalidationListener;
import javafx.beans.Observable;
import javafx.beans.property.SimpleStringProperty;
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

    TableView<Product> productInfoTableView;

    private String[] productArr = {};

    public LoginController() throws JSONException{


            ListView productList = new ListView();

            productList.getItems().add("Item 1");

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




        for(int i = 0; i < productUpdate.size(); i++) {
            System.out.println(productUpdate.get(i).Description);

            productList.getItems().addAll(productUpdate.get(i).Description);

        }


            GridPane grid = new GridPane();
            GridPane desGrid = new GridPane();

            productList.setPrefSize(400, 400);

            //  grid.setPadding(new Insets(10, 10, 10, 10));
            grid.setHgap(3);
            grid.setVgap(3);
            grid.setAlignment(Pos.CENTER);

            Label emailLabel = new Label("Email: ");

            Label instructLabel = new Label("Sign In to view cart and orders");

            emailField = new TextField();
            emailField.setPrefColumnCount(10);

            PopOver popUp = new PopOver();

            popUp.setTitle("Product Information");
            popUp.setArrowLocation(PopOver.ArrowLocation.LEFT_CENTER);
            popUp.setAutoHide(false);
            popUp.setDetachable(true);

            enterButton = new Button("Sign In");



            String value = "hello there dof\n" +
                    "osjdofjddfsdfds\n" +
                    "oifjdosifjosdij\n" +
                    "isdjdfdsfdsfdsf\n" +
                    "foijsdodsfdsfds";

            //Labels for JSon objects to get stored:



            //testing purposes
            //priceObj.setText("" + 2);
           // quantityObj.setText("" + 33);

          //  for(int i = 0 ; i < productUpdate.size(); i++) {
           //     descriptionLabel.setText(String.valueOf(productUpdate.get(i)));

          //  }


            TableColumn<Product, String> product_idCol = new TableColumn<>("Product ID");
            product_idCol.setMinWidth(150);
            product_idCol.setCellValueFactory(new PropertyValueFactory<>("ProductId"));

            TableColumn<Product, String> descriptionCol = new TableColumn<>("Description");
            descriptionCol.setMinWidth(150);
            descriptionCol.setCellValueFactory(new PropertyValueFactory<>("Description"));

            TableColumn<Product, String> quantityCol = new TableColumn<>("Quantity");
            quantityCol.setMinWidth(150);
            quantityCol.setCellValueFactory(new PropertyValueFactory<>("Quantity"));

            TableColumn<Product, String> priceCol = new TableColumn<>("Price");
            priceCol.setMinWidth(150);
            priceCol.setCellValueFactory(new PropertyValueFactory<>("Price"));
            productInfoTableView = new TableView<>();
            productInfoTableView.getColumns().addAll(product_idCol, descriptionCol, quantityCol, priceCol);



            popUp.setContentNode(desGrid);

            ScrollPane scrollPane = new ScrollPane(productList);

            grid.add(instructLabel, 18, 0, 1, 2);

            grid.add(emailLabel, 16, 2, 1, 2);

            grid.add(emailField, 18, 2, 2, 2);

            grid.add(enterButton, 20, 2, 1, 2);

            grid.add(scrollPane, 0, 5, 2, 5);


//PROBLEM - select item in listview and popup should display out, but error is that the popOver does not point to the right
            //row in listview which i can't figure out why the parameters in show function are the way they are.

            productList.setOnMouseMoved(new EventHandler<MouseEvent>() {
                @Override
                public void handle(MouseEvent event) {
                    mouseX = event.getScreenX();
                    mouseY = event.getScreenY();
                }
            });

            productList.getSelectionModel().selectedItemProperty().addListener(new InvalidationListener() {
                @Override
                public void invalidated(Observable observable) {

                    popUp.show(grid, mouseX + popupOffsetX, mouseY + popupOffsetY);


                   // for(Integer i: productList.getSelectionModel().getSelectedIndices()) {

                  //  }

                }
            });

            productList.getSelectionModel().selectedItemProperty().addListener( ov -> {

                popUp.show(grid, mouseX + popupOffsetX, mouseY + popupOffsetY);
               // for(Object i: productList()) {
                     //desGrid.getChildren().add(productUpdate.get(i));

               // }

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