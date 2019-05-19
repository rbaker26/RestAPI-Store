package UI;

import Messages.CartHandler;
import Messages.ProductHandler;
import data.Gridclass;
import data.Product;
import data.ProductUpdate;
import data.PurchaseInfo;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import org.controlsfx.control.PopOver;

import java.util.ArrayList;

public class CartController extends AbstractSceneController {


    private static final double popupOffsetX = 10;
    private static final double popupOffsetY = 5;

    GridPane cartGrid;
    TableView<Product> productInfoTableView;
    ArrayList<ProductUpdate> cartUpdate;
    ArrayList<PurchaseInfo> purchaseList = new ArrayList<>();
    ObservableList<PurchaseInfo> purchases;

    ScrollPane scrollPane;

    String email;

    Button deleteItemButton;
    Button checkoutButton;
    Button backButton;

    Label yourCartLabel;
    Label emailLabel;

    private double mouseX;
    private double mouseY;

    public void getPurchases() {
        this.cartUpdate = CartHandler.PurchaseCart(email);
        for (ProductUpdate p : cartUpdate) {
            Product product = ProductHandler.GetProduct(p.productId);

            //array list
            purchaseList.add(new PurchaseInfo(product, p.quantityToBeRemoved));
        }

        //observable list
        purchases = FXCollections.observableArrayList(purchaseList);

       // return purchaseList;

    }

    public CartController(String email) {

        this.email = email;

        ListView productCartList = new ListView();

        cartGrid = new GridPane();
        deleteItemButton = new Button("Delete Item From Cart");
        checkoutButton = new Button("Proceed To Checkout Cart");

        yourCartLabel = new Label("My Cart");
        emailLabel = new Label();

        backButton = new Button("Back");

        productInfoTableView = new TableView<>();

       // ArrayList<PurchaseInfo> cartList = getPurchases();

        //Gridclass[] desgrid = new Gridclass[productUpdate.size()];

        Gridclass[] desgrid = new Gridclass[purchaseList.size()];


        for(int i = 0; i < purchaseList.size(); i++) {
            System.out.println("All my cart items here: ");
            System.out.println(purchaseList.get(i).getDescription());

            productCartList.getItems().addAll(purchaseList.get(i).getDescription());

        }

        for(int i = 0; i < purchaseList.size(); i++) {
            desgrid[i] = new Gridclass(purchaseList.get(i).getProductId(), this.email);
            desgrid[i].setDescriptionObj(purchaseList.get(i).getDescription());
            desgrid[i].setPriceObj(purchaseList.get(i).getPrice());
            desgrid[i].setQuantityObj(purchaseList.get(i).getQty());
        }

        //OUTPUT THE CART LIST HERE
        for (int i = 0 ; i < purchaseList.size(); i ++ ) {

            System.out.println(desgrid[i].getQuantityObj());
            System.out.println(desgrid[i].getPriceObj());
            System.out.println(desgrid[i].getDescriptionObj());

        }


        scrollPane = new ScrollPane(productCartList);


        /**
         * POPOVER DECLARATION
         */
        PopOver popUp = new PopOver();

        popUp.setTitle("In My Cart - Product Information");
        popUp.setArrowLocation(PopOver.ArrowLocation.LEFT_CENTER);
        popUp.setAutoHide(false);
        popUp.setDetachable(true);



        productCartList.setOnMouseMoved(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                mouseX = event.getScreenX();
                mouseY = event.getScreenY();
            }
        });

/**
        TableColumn<PurchaseInfo, Integer> product_idCol = new TableColumn<>("Product ID");
        product_idCol.setMinWidth(150);
        product_idCol.setCellValueFactory(new PropertyValueFactory<>("productId"));

        TableColumn<PurchaseInfo, String> descriptionCol = new TableColumn<>("Description");
        descriptionCol.setMinWidth(150);
        descriptionCol.setCellValueFactory(new PropertyValueFactory<>("description"));

        TableColumn<PurchaseInfo, String> quantityCol = new TableColumn<>("Quantity Selected");
        quantityCol.setMinWidth(150);
        quantityCol.setCellValueFactory(new PropertyValueFactory<>("qty"));

        TableColumn<PurchaseInfo, String> priceCol = new TableColumn<>("Price");
        priceCol.setMinWidth(150);
        priceCol.setCellValueFactory(new PropertyValueFactory<>("price"));
**/

        //productInfoTableView.getColumns().addAll(product_idCol, descriptionCol, quantityCol, priceCol);



       // productInfoTableView.getColumns().addAll(product_idCol, descriptionCol, quantityCol, priceCol);


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

        //SELECT ITEM FROM LISTVIEW TO VIEW ITS CONTENTS:
        productCartList.getSelectionModel().selectedItemProperty().addListener( ov -> {

            popUp.setContentNode(desgrid[productCartList.getSelectionModel().getSelectedIndex()]);
            popUp.show(cartGrid, mouseX + popupOffsetX, mouseY + popupOffsetY);


        });


    }



    public Button getBackButton() {
        return backButton;
    }
}
