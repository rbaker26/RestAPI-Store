package UI;

import Messages.CartHandler;
import Messages.ProductHandler;
import data.*;
import javafx.beans.property.StringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.GridPane;
import java.util.ArrayList;

import static java.lang.Integer.parseInt;

public class CartController extends AbstractSceneController {
    private GridPane cartGrid;
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
    private Label emailLabel;

    public void getPurchases() {



        this.cartUpdate = CartHandler.PurchaseCart(email);
        this.productList = ProductHandler.GetProducts();
        for (ProductUpdate p : cartUpdate) {
            //Product product = ProductHandler.GetProduct(p.productId);
            Product product = getProduct(p.productId);
            System.out.println("Product: " + product.description);
            //array list
            purchaseList.add(new PurchaseInfo(product, p.quantityToBeRemoved));
        }
        System.out.println("ADDING TO TABLEVIEW: size=" + purchaseList.size());
        for(PurchaseInfo p : purchaseList) {
            System.out.println("IN LOOP Product: " + p.getDescription());
            System.out.println("IN LOOP Qty: " + p.getQty());
            System.out.println("IN LOOP Price: " + p.getPrice());
            productInfoTableView.getItems().add(p);
        }
    }

    public Product getProduct(int productId) {
        System.out.println("Looking for productId: " + productId);
        for(Product p : productList) {
            if(p.productId == productId) {
                return p;
            }
        }
        return null;
    }
    public CartController(String email) {



        this.email = email;
        cartGrid = new GridPane();
        deleteItemButton = new Button("Delete Item From Cart");
        deleteItemButton.setStyle("-fx-font-weight: bold");
        checkoutButton = new Button("Proceed To Checkout Cart");
        checkoutButton.setStyle("-fx-font-weight: bold");
        yourCartLabel = new Label("My Cart");
        yourCartLabel.setStyle("-fx-font: normal bold 15px 'arial'; -fx-text-fill: white");
        emailLabel = new Label("Email: " + this.email);
        emailLabel.setStyle("-fx-font: normal bold 10px 'arial'; -fx-text-fill: white");
        backButton = new Button("Back");
        backButton.setStyle("-fx-font-weight: bold");
        productInfoTableView = new TableView<>();
        scrollPane = new ScrollPane(productInfoTableView);

        Label blank1 = new Label(" ");
        Label blank2 = new Label(" ");

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
}
