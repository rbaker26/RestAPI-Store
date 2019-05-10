package UI;

import javafx.beans.InvalidationListener;
import javafx.beans.Observable;
import javafx.collections.ObservableList;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.layout.GridPane;
import org.controlsfx.control.PopOver;

public class LoginController extends AbstractSceneController {

    private TextField emailField;
    private Button enterButton;

    public LoginController() {

        GridPane grid = new GridPane();
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

        emailField = new TextField();
        emailField.setPrefColumnCount(10);

        PopOver popUp = new PopOver();

        popUp.setArrowLocation(PopOver.ArrowLocation.LEFT_CENTER);
        popUp.setAutoHide(false);
        popUp.setDetachable(true);

        enterButton = new Button("Sign In");

        ScrollPane scrollPane = new ScrollPane(productList);

        grid.add(instructLabel, 18, 0, 1, 2);

        grid.add(emailLabel, 16, 2, 1, 2);

        grid.add(emailField, 18, 2, 2, 2);

        grid.add(enterButton, 20, 2, 1, 2);

        grid.add(scrollPane, 0, 5, 2, 5);


//PROBLEM - select item in listview and popup should display out, but error is that the popOver does not point to the right
        //row in listview which i can't figure out why the parameters in show function are the way they are.

        productList.getSelectionModel().selectedItemProperty().addListener(new InvalidationListener() {
            @Override
            public void invalidated(Observable observable) {
                System.out.println("SELECTED MEEEE");

                   popUp.show(grid, 5,5);


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