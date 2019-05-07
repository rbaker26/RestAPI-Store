package UI;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.GridPane;

public class LoginController extends AbstractSceneController {

    private TextField emailField;
    private Button enterButton;

    public LoginController() {

        GridPane grid = new GridPane();

        grid.setPadding(new Insets(10, 10, 10, 10));
        grid.setHgap(5);
        grid.setVgap(5);
        grid.setAlignment(Pos.CENTER);

        Label emailLabel = new Label("Email: ");

        emailField = new TextField();
        emailField.setPrefColumnCount(10);


        GridPane.setConstraints(emailLabel, 0, 0);

        grid.getChildren().add(emailLabel);


        GridPane.setConstraints(emailField, 1, 0);

        grid.getChildren().add(emailField);


        GridPane.setConstraints(enterButton, 0, 1);

        grid.getChildren().add(enterButton);


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