package UI;

import Messages.CartHandler;
import Messages.ProductHandler;
import data.CartUpdate;
import data.Gridclass;
import data.Product;
import data.ProductUpdate;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.Pane;
import org.controlsfx.control.PopOver;
import org.json.JSONException;

import java.util.ArrayList;

public class LoginController extends AbstractSceneController {

    private static final double popupOffsetX = 10;
    private static final double popupOffsetY = 5;
    private Button seeCart;
    private Button signOut;
    private String usersEmail;
    private Label greetLabel = new Label();
    private double mouseX;
    private double mouseY;
    private ListView productList = new ListView();
    private ArrayList<Product> productUpdate;
    private GridPane grid = new GridPane();
    private PopOver popUp = new PopOver();
    private Gridclass[] desgrid;
    private ScrollPane scrollPane;

    public LoginController() throws JSONException{
        seeCart = new Button("Go To My Cart");
        signOut = new Button("Sign Out");
        seeCart.setStyle("-fx-font-weight: bold");
        signOut.setStyle("-fx-font-weight: bold");
        grid.setHgap(3);
        grid.setVgap(3);
        grid.setAlignment(Pos.CENTER);
        grid.setStyle("-fx-background-color: linear-gradient(to bottom, #000066 44%, #9999ff 100%)");
        greetLabel.setStyle("-fx-font: normal bold 13px 'arial'; -fx-text-fill: white");
        populateListView();
        setUpPopUp();
        scrollPane = new ScrollPane(productList);

        grid.add(signOut, 16, 0, 1, 2);
        grid.add(scrollPane, 0, 5, 2, 5);
        grid.add(seeCart, 2, 0, 1 ,2);
        grid.add(greetLabel,0, 0, 1, 2);


        productList.setOnMouseMoved(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                mouseX = event.getScreenX();
                mouseY = event.getScreenY();
            }
        });

        setRoot(grid);

        }
    public void setEmail(String email) {
        this.usersEmail = email;
        greetLabel.setText("Hello and Welcome!" + "\n" +
                "Your email is " + this.usersEmail + "\n" +
                "Select a product and type in how many" + "\n" +
                "you would like to add to your cart");
        for(int i = 0; i < productUpdate.size(); i++) {
            desgrid[i] = new Gridclass(productUpdate.get(i).productId, this.usersEmail);
            desgrid[i].setDescriptionObj(productUpdate.get(i).description);
            desgrid[i].setPriceObj(productUpdate.get(i).price);
            desgrid[i].setQuantityObj(productUpdate.get(i).quantity);
        }
        productList.getSelectionModel().selectedItemProperty().addListener( ov -> {
            popUp.setContentNode(desgrid[productList.getSelectionModel().getSelectedIndex()]);
            popUp.show(grid, mouseX + popupOffsetX, mouseY + popupOffsetY);
        });
    }

    public void setUpPopUp() {
        popUp.setTitle("Product Information");
        popUp.setArrowLocation(PopOver.ArrowLocation.LEFT_CENTER);
        popUp.setAutoHide(false);
        popUp.setDetachable(true);

        desgrid = new Gridclass[productUpdate.size()];
    }

    public void getProducts() {
        this.productUpdate = ProductHandler.GetProducts();
    }

    public void populateListView() {
        getProducts();
        productList.getItems().clear();
        productList.setPrefSize(400, 400);
        for(int i = 0; i < productUpdate.size(); i++) {
            System.out.println(productUpdate.get(i).description);

            productList.getItems().addAll(productUpdate.get(i).description);

        }
    }
    public Button getSeeCart() {
        return seeCart;
    }
    public Button getSignOut() {
        return signOut;
    }

}