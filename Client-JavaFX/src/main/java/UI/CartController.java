package UI;

import data.Product;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.GridPane;

public class CartController extends AbstractSceneController {



    GridPane cartGrid;
    TableView<Product> productInfoTableView;

    Button deleteItemButton;
    Button checkoutButton;
    Button backButton;

    Label yourCartLabel;
    Label emailLabel;


    public CartController() {

        cartGrid = new GridPane();
        deleteItemButton = new Button("Delete Item From Cart");
        checkoutButton = new Button("Proceed To Checkout Cart");

        yourCartLabel = new Label("My Cart");
        emailLabel = new Label();

        backButton = new Button("Back");


        TableColumn<Product, String> product_idCol = new TableColumn<>("Product ID");
        product_idCol.setMinWidth(150);
        product_idCol.setCellValueFactory(new PropertyValueFactory<>("productId"));

        TableColumn<Product, String> descriptionCol = new TableColumn<>("Description");
        descriptionCol.setMinWidth(150);
        descriptionCol.setCellValueFactory(new PropertyValueFactory<>("Description"));

        TableColumn<Product, String> quantityCol = new TableColumn<>("Quantity Selected");
        quantityCol.setMinWidth(150);
        quantityCol.setCellValueFactory(new PropertyValueFactory<>("Quantity"));

        TableColumn<Product, String> priceCol = new TableColumn<>("Price");
        priceCol.setMinWidth(150);
        priceCol.setCellValueFactory(new PropertyValueFactory<>("Price"));
        productInfoTableView = new TableView<>();


        productInfoTableView.getColumns().addAll(product_idCol, descriptionCol, quantityCol, priceCol);


        cartGrid.getChildren().add(yourCartLabel);
        yourCartLabel.setAlignment(Pos.CENTER);
        cartGrid.add(emailLabel, 0, 2, 1, 1);
        cartGrid.add(productInfoTableView, 0, 4, 5, 5);
        cartGrid.add(backButton, 0, 9, 1, 2);
        cartGrid.add(deleteItemButton, 4, 9, 1, 2);
        cartGrid.add(checkoutButton, 8, 9, 1, 2);

        cartGrid.setAlignment(Pos.CENTER);

        setRoot(cartGrid);

    }

    public Button getBackButton() {
        return backButton;
    }
}
