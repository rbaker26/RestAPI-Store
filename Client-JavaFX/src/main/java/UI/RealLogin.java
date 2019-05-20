package UI;

import data.Gridclass;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.GridPane;

public class RealLogin extends AbstractSceneController{
    GridPane grid = new GridPane();

    private TextField emailField;
    private Button enterButton;

    public RealLogin() {

        Label emailLabel = new Label("Enter Email: ");
        emailLabel.setStyle("-fx-font: normal bold 14px 'arial'; -fx-text-fill: white");

        Label blank = new Label("  ");
        Label title = new Label("Welcome To Our E-Commerce Shop");
        title.setStyle("-fx-font: normal bold 20px 'arial'; -fx-text-fill: white");
        Label blank2 = new Label("  ");
        Label blank3 = new Label("  ");

        Label instructLabel = new Label("Sign In to view products and cart");

        instructLabel.setStyle("-fx-font: normal bold 14px 'arial'; -fx-text-fill: white");

        emailField = new TextField();
        emailField.setPrefColumnCount(10);

        enterButton = new Button("Sign In");

        enterButton.setStyle("-fx-font-weight: bold");

        grid.setStyle("-fx-background-color: linear-gradient(to bottom, #000066 44%, #9999ff 100%)");

        grid.add(title, 0, 0, 1, 2);
        grid.add(blank2, 0, 2, 1, 2);
        grid.add(blank3, 0, 4, 1, 2);

        grid.add(instructLabel, 0, 6, 1 , 2);
        grid.add(emailLabel, 0, 8, 1 , 2);
        grid.add(emailField, 4, 8, 1 , 2);
        grid.add(blank, 10, 8, 1, 2);
        grid.add(enterButton, 12, 8, 1, 2);
        grid.setAlignment(Pos.CENTER);

        setRoot(grid);

    }

    public String getEmailField() {

        return emailField.getText();
    }

    public Button getEnterButton() {
        return enterButton;
    }
}
