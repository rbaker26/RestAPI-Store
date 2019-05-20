package UI;

import Messages.CartHandler;
import Messages.ProductHandler;
import data.*;
import javafx.collections.ObservableList;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.GridPane;
import java.util.ArrayList;

public class CartController extends AbstractSceneController {
    private GridPane cartGrid = new GridPane();
    private TableView productInfoTableView;
    private ArrayList<ProductUpdate> cartUpdate;
    private ArrayList<Product> productList;
    private ArrayList<PurchaseInfo> purchaseList = new ArrayList<>();
    private ScrollPane scrollPane;
    private String email;
    private Button deleteItemButton;
    private Button checkoutButton;
    private Button backButton;
    private Label yourCartLabel;
    private Label emailLabel = new Label();

    public void getPurchases() {
        System.out.println("In get purchases requesting for email: " + email);
        this.cartUpdate = CartHandler.GetCart(email);
        productInfoTableView.getItems().clear();
        for (ProductUpdate p : cartUpdate) {
            System.out.println("Requesting productId: " + p.productId);
            Product product = ProductHandler.GetProduct(p.productId);
            productInfoTableView.getItems().add(new PurchaseInfo(product, p.quantityToBeRemoved));
        }
    }
    public CartController() {
        deleteItemButton = new Button("Delete Item From Cart");
        deleteItemButton.setStyle("-fx-font-weight: bold");
        checkoutButton = new Button("Proceed To Checkout Cart");
        checkoutButton.setStyle("-fx-font-weight: bold");
        yourCartLabel = new Label("My Cart");
        yourCartLabel.setStyle("-fx-font: normal bold 15px 'arial'; -fx-text-fill: white");
        emailLabel.setStyle("-fx-font: normal bold 10px 'arial'; -fx-text-fill: white");
        backButton = new Button("Back");
        backButton.setStyle("-fx-font-weight: bold");
        productInfoTableView = new TableView<>();
        scrollPane = new ScrollPane(productInfoTableView);
        deleteItemButton.setOnAction(e -> deleteItemButtonClicked());

        TableColumn<PurchaseInfo, String> descriptionCol = new TableColumn<>("Description");
        descriptionCol.setMinWidth(155);
        descriptionCol.setCellValueFactory(new PropertyValueFactory<>("description"));
        TableColumn<PurchaseInfo, Integer> quantityCol = new TableColumn<>("Quantity Selected");
        quantityCol.setMinWidth(155);
        quantityCol.setCellValueFactory(new PropertyValueFactory<>("qty"));
        TableColumn<PurchaseInfo, Float> priceCol = new TableColumn<>("Price For One");
        priceCol.setMinWidth(155);
        priceCol.setCellValueFactory(new PropertyValueFactory<>("price"));


        productInfoTableView.getColumns().addAll(descriptionCol, quantityCol, priceCol);


        cartGrid.getChildren().add(yourCartLabel);
        yourCartLabel.setAlignment(Pos.CENTER);
        cartGrid.add(emailLabel, 0, 2, 1, 1);
        //cartGrid.add(productInfoTableView, 0, 4, 5, 5);
        cartGrid.add(scrollPane, 0, 4, 5, 5);
        cartGrid.add(backButton, 0, 9, 1, 2);
        cartGrid.add(deleteItemButton, 4, 9, 1, 2);
        cartGrid.add(checkoutButton, 8, 9, 1, 2);

        cartGrid.setStyle("-fx-background-color: linear-gradient(to bottom, #000066 44%, #9999ff 100%)");

        cartGrid.setAlignment(Pos.CENTER);

        setRoot(cartGrid);

        for(int i = 0; i < purchaseList.size(); i++) {
            System.out.println(purchaseList.get(i).getDescription());
            System.out.println(purchaseList.get(i).getProductId());
            System.out.println(purchaseList.get(i).getPrice());
            System.out.println(purchaseList.get(i).getQty());

        }

    }

    public Button getBackButton() {
        return backButton;
    }

    public void setEmail(String email) {
        this.email = email;
        emailLabel.setText("Email: " + this.email);
    }

    //Remove the item selected from tableview
    public void deleteItemButtonClicked() {
        ObservableList<PurchaseInfo> productSelected, allProducts;
        allProducts = productInfoTableView.getItems();
        productSelected = productInfoTableView.getSelectionModel().getSelectedItems();
        PurchaseInfo item = (PurchaseInfo) productInfoTableView.getSelectionModel().getSelectedItem();
        //Removing the item through the email used and the item's product id, from the cart
        CartHandler.RemoveCartUpdate(new RemoveCartUpdate(this.email, item.getProductId()));
        //Checking to make sure the right product id is being removed
        System.out.println("Removing ProductID: " + item.getProductId());
        //Seeing the item removed from tableview cart visually
        productSelected.forEach(allProducts::remove);
    }

    public Button getPurchaseButton() {
        return this.checkoutButton;
    }
}
