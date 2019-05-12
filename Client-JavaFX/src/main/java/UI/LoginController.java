package UI;

import javafx.beans.InvalidationListener;
import javafx.beans.Observable;
import javafx.collections.ObservableList;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import org.controlsfx.control.PopOver;

public class LoginController extends AbstractSceneController {

    private static final double popupOffsetX = 10;
    private static final double popupOffsetY = 5;

    private TextField emailField;
    private TextField quantityField;
    private Button enterButton;
    private Button addToCartButton;

    private double mouseX;
    private double mouseY;


    public LoginController() {

        GridPane grid = new GridPane();
        GridPane desGrid = new GridPane();
        ListView productList = new ListView();
        productList.getItems().add("Item 1");
        productList.getItems().add("Item 2");
        productList.getItems().add("Item 3");
        productList.setPrefSize(400, 400);

      //  grid.setPadding(new Insets(10, 10, 10, 10));
        grid.setHgap(3);
        grid.setVgap(3);
        grid.setAlignment(Pos.CENTER);

        Label emailLabel = new Label("Email: ");

        Label instructLabel = new Label("Sign In to view cart and orders");

        Label qtyEnteredLabel = new Label("How many would you like to order?: ");

        Label descriptionLabel = new Label("Description: ");

        Label priceLabel = new Label("Price: ");

        Label quantityLabel = new Label("Quantity: ");

        emailField = new TextField();
        emailField.setPrefColumnCount(10);
        quantityField = new TextField();
        quantityField.setPrefColumnCount(4);

        PopOver popUp = new PopOver();

        popUp.setTitle("Product Information");
        popUp.setArrowLocation(PopOver.ArrowLocation.LEFT_CENTER);
        popUp.setAutoHide(false);
        popUp.setDetachable(true);


        enterButton = new Button("Sign In");


        addToCartButton = new Button("Add To Cart");

        desGrid.setAlignment(Pos.CENTER);
        desGrid.add(descriptionLabel,5, 0, 1, 2);
        desGrid.add(priceLabel, 5, 2, 1, 2);
        desGrid.add(quantityLabel, 5, 4, 1, 1);
        desGrid.add(qtyEnteredLabel, 5, 6, 1, 1);
        desGrid.add(quantityField, 7, 6, 1, 1);
        desGrid.add(addToCartButton, 5, 8, 1, 2);

        desGrid.setMinSize(300, 200);

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
            @Override public void handle(MouseEvent event) {
                mouseX = event.getScreenX();
                mouseY = event.getScreenY();
            }
        });

        productList.getSelectionModel().selectedItemProperty().addListener(new InvalidationListener() {
            @Override
            public void invalidated(Observable observable) {
                System.out.println("SELECTED MEEEE");

                   popUp.show(grid, mouseX + popupOffsetX, mouseY + popupOffsetY);


            }
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