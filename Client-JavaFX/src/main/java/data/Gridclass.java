package data;

import Messages.CartHandler;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.layout.GridPane;
import data.Gridclass;
import data.Product;
import javafx.scene.control.TextField;

import java.util.Formatter;

import static java.lang.Integer.parseInt;

public class Gridclass extends GridPane {

    private Label descriptionObj = new Label();

    private Label priceObj = new Label();

    private Label quantityObj = new Label();

    private Label qtyEnteredLabel;

    private final int productId;
    private final String email;

    private Label descriptionLabel;

    private Label priceLabel;

    private Label quantityLabel;

    private TextField quantityField;

    private Button addToCartButton;


    public Gridclass(int productId, String email) {
        this.productId = productId;
        this.email = email;
        qtyEnteredLabel = new Label("How many would you like to order?: ");

        descriptionLabel = new Label("Description: ");
        descriptionLabel.setStyle("-fx-font-weight: bold");

        priceLabel = new Label("Price: ");
        priceLabel.setStyle("-fx-font-weight: bold");

        quantityLabel = new Label("Quantity: ");
        quantityLabel.setStyle("-fx-font-weight: bold");

        addToCartButton = new Button("Add To Cart");

        quantityField = new TextField();
        quantityField.setPrefColumnCount(4);

        quantityField.setPrefColumnCount(4);

        this.setAlignment(Pos.CENTER);
        this.add(descriptionLabel,5, 0, 1, 2);
        this.add(descriptionObj, 7, 0, 1 , 3);
        this.add(priceLabel, 5, 6, 1, 2);
        this.add(priceObj, 7, 6,  1, 2);
        this.add(quantityLabel, 5, 8, 1, 2);
        this.add(quantityObj, 7, 8, 1, 2);

        //desGrid.add(productInfoTableView, 5, 1, 1, 1);
        this.add(qtyEnteredLabel, 5, 10, 1, 1);
        this.add(quantityField, 7, 10, 1, 1);
        this.add(addToCartButton, 5, 12, 1, 2);

        this.setMinSize(300, 200);

        this.addToCartButton.setOnAction(event -> {

            if(Integer.parseInt(getQuantityObj()) <= 0) {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("We currently do not have " + getDescriptionObj() + " in stock!");
                alert.setContentText("We are sorry for the inconvenience.");

                alert.showAndWait();

            }

            else if(Integer.parseInt(getQuantityField()) > Integer.parseInt(getQuantityObj()) ) {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setHeaderText("We currently do not have enough " + getDescriptionObj() + " in stock!");
                alert.setContentText("We are sorry for the inconvenience.");

                alert.showAndWait();

            }

            else {
                // this is where we need to add to a local cart
                CartHandler.SendCartUpdate(new CartUpdate(this.email, new ProductUpdate(this.productId, parseInt(this.quantityField.getText()))));
                System.out.println("Added to cart");
            }
        });
    }

    public String getDescriptionObj() {

        return descriptionObj.getText();
    }

    public String getPriceObj() {

        return priceObj.getText();
    }

    public String getQuantityObj() {

        return quantityObj.getText();
    }

    public void setDescriptionObj(String description) {

        descriptionObj.setText(description);

    }

    public void setPriceObj(float price) {


        priceObj.setText(String.valueOf(price));

    }

    public void setQuantityObj(int quantity) {

        quantityObj.setText(String.valueOf(quantity));

    }

    public String getQuantityField() {
        return quantityField.getText();
    }

    @Override
    public String toString() {
        StringBuilder str = new StringBuilder();
        Formatter form = new Formatter(str);

        form.format("Product: %s%n Price: %s%n", descriptionObj.getText(), priceObj.getText());

        return form.toString();
    }
}
