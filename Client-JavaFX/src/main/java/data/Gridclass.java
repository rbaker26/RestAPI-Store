package data;

import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.GridPane;
import data.Gridclass;
import data.Product;
import javafx.scene.control.TextField;

import java.util.Formatter;

public class Gridclass extends GridPane {

    private Label descriptionObj = new Label();

    private Label priceObj = new Label();

    private Label quantityObj = new Label();

    private Label qtyEnteredLabel;


    private Label descriptionLabel;

    private Label priceLabel;

    private Label quantityLabel;

    private TextField emailField = new TextField();

    private TextField quantityField;

    private Button addToCartButton;


    public Gridclass() {

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


    @Override
    public String toString() {
        StringBuilder str = new StringBuilder();
        Formatter form = new Formatter(str);

        form.format("Product: %s%n Price: %s%n", descriptionObj.getText(), priceObj.getText());

        return form.toString();
    }
}
