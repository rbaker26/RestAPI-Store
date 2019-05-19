package UI;

import Messages.CartHandler;
import Messages.ProductHandler;
import data.Product;
import data.ProductUpdate;
import data.PurchaseInfo;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.GridPane;

import java.util.ArrayList;

public class CartController extends AbstractSceneController {



    GridPane cartGrid;
    TableView<Product> productInfoTableView;
    ArrayList<ProductUpdate> cartUpdate;
    ArrayList<PurchaseInfo> purchaseList = new ArrayList<>();
    ObservableList<PurchaseInfo> purchases;
    String email;

    Button deleteItemButton;
    Button checkoutButton;
    Button backButton;

    Label yourCartLabel;
    Label emailLabel;


    public CartController(String email) {
        this.email = email;

        cartGrid = new GridPane();
        deleteItemButton = new Button("Delete Item From Cart");
        checkoutButton = new Button("Proceed To Checkout Cart");

        yourCartLabel = new Label("My Cart");
        emailLabel = new Label();

        backButton = new Button("Back");


        TableColumn<PurchaseInfo, Integer> product_idCol = new TableColumn<>("Product ID");
        product_idCol.setMinWidth(150);
        product_idCol.setCellValueFactory(new PropertyValueFactory<>("productId"));

        TableColumn<PurchaseInfo, String> descriptionCol = new TableColumn<>("Description");
        descriptionCol.setMinWidth(150);
        descriptionCol.setCellValueFactory(new PropertyValueFactory<>("description"));

        TableColumn<PurchaseInfo, Integer> quantityCol = new TableColumn<>("Quantity Selected");
        quantityCol.setMinWidth(150);
        quantityCol.setCellValueFactory(new PropertyValueFactory<>("qty"));

        TableColumn<PurchaseInfo, Float> priceCol = new TableColumn<>("Price");
        priceCol.setMinWidth(150);
        priceCol.setCellValueFactory(new PropertyValueFactory<>("price"));
        productInfoTableView = new TableView<>();


       // productInfoTableView.getColumns().addAll(product_idCol, descriptionCol, quantityCol, priceCol);


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

    public void getPurchases() {
        this.cartUpdate = CartHandler.PurchaseCart(email);
        for (ProductUpdate p : cartUpdate) {
            Product product = ProductHandler.GetProduct(p.productId);
            purchaseList.add(new PurchaseInfo(product, p.quantityToBeRemoved));
        }
        purchases = FXCollections.observableArrayList(purchaseList);
    }

    public Button getBackButton() {
        return backButton;
    }
}
