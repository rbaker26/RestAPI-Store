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
        checkoutButton = new Button("Proceed To Checkout Cart");
        yourCartLabel = new Label("My Cart");
        emailLabel = new Label();
        backButton = new Button("Back");
        productInfoTableView = new TableView<>();
        scrollPane = new ScrollPane(productInfoTableView);

        deleteItemButton.setOnAction(e -> deleteItemButtonClicked());

        TableColumn<PurchaseInfo, String> descriptionCol = new TableColumn<>("Description");
        descriptionCol.setMinWidth(150);
        descriptionCol.setCellValueFactory(new PropertyValueFactory<>("description"));
        TableColumn<PurchaseInfo, Integer> quantityCol = new TableColumn<>("Quantity Selected");
        quantityCol.setMinWidth(150);
        quantityCol.setCellValueFactory(new PropertyValueFactory<>("qty"));
        TableColumn<PurchaseInfo, Float> priceCol = new TableColumn<>("Price");
        priceCol.setMinWidth(150);
        priceCol.setCellValueFactory(new PropertyValueFactory<>("price"));


        productInfoTableView.getColumns().addAll(descriptionCol, quantityCol, priceCol);

//        productInfoTableView.getItems().add(new PurchaseInfo(new Product(1, "test 1", 5, 4.99f), 3));
//        productInfoTableView.getItems().add(new PurchaseInfo(new Product(2, "test 2", 7, 3.99f), 2));

        cartGrid.getChildren().add(yourCartLabel);
        yourCartLabel.setAlignment(Pos.CENTER);
        cartGrid.add(emailLabel, 0, 2, 1, 1);
        //cartGrid.add(productInfoTableView, 0, 4, 5, 5);
        cartGrid.add(scrollPane, 0, 4, 5, 5);
        cartGrid.add(backButton, 0, 9, 1, 2);
        cartGrid.add(deleteItemButton, 4, 9, 1, 2);
        cartGrid.add(checkoutButton, 8, 9, 1, 2);

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

    public void deleteItemButtonClicked() {


        ProductUpdate p = new ProductUpdate();
        Product product = getProduct(p.productId);

        CartHandler.RemoveCartUpdate(new RemoveCartUpdate(this.email, product.productId));

        System.out.println("Removing: " + product.productId);

        ObservableList<PurchaseInfo> productSelected, allProducts;


        allProducts = productInfoTableView.getItems();

        productSelected = productInfoTableView.getSelectionModel().getSelectedItems();

        productSelected.forEach(allProducts::remove);


    }
}
