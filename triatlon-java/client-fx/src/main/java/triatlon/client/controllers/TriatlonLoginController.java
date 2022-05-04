package triatlon.client.controllers;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Modality;
import javafx.stage.Stage;
import triatlon.client.DependencyProvider;
import triatlon.client.TriatlonApplication;
import triatlon.model.person.Referee;
import triatlon.proto.Triatlon;
import triatlon.service.TriatlonServiceInterface;
import triatlon.service.TriatlonServiceProto;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

// TriatlonLoginController

public class TriatlonLoginController implements Initializable {

    // Private Properties

    private TriatlonServiceProto server = DependencyProvider.getInstance().getServer();
    private FXMLLoader fxmlLoader = new FXMLLoader(TriatlonApplication.class.getResource("/triatlon-view.fxml"));
    private Parent root;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        try {
            root = fxmlLoader.load();
        } catch (Exception ex) {
            ex.printStackTrace();
            return;
        }
    }

    // Outlets

    @FXML
    public TextField emailTextfield;

    @FXML
    public PasswordField passwordTextfield;

    // Private Methods

    private void showDashboard(Parent root) {
        Scene scene = new Scene(root, 480, 320);
        Stage stage = new Stage();
        stage.setTitle("Triatlon");
        stage.setScene(scene);
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.show();
        emailTextfield.clear();
        passwordTextfield.clear();
    }

    // Actions

    @FXML
    void onLoginButtonClicked(ActionEvent event) {
        TriatlonController triatlonController = fxmlLoader.getController();
        Triatlon.Referee referee = server.logInReferee(emailTextfield.getText(), passwordTextfield.getText(), triatlonController);
        if (referee != null) {
            DependencyProvider.getInstance().setReferee(referee);
            triatlonController.refereeDidSet();
            showDashboard(root);
        }
        else {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Eroare Login");
            alert.setHeaderText("Credentiale gresite");
            alert.setContentText("Combinatia email - parola nu este asociata niciunui cont de arbitru.");
            alert.showAndWait();
        }
    }

}
