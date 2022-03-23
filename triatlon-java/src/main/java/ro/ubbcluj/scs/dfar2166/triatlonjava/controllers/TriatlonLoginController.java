package ro.ubbcluj.scs.dfar2166.triatlonjava.controllers;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Modality;
import javafx.stage.Stage;
import ro.ubbcluj.scs.dfar2166.triatlonjava.TriatlonApplication;
import ro.ubbcluj.scs.dfar2166.triatlonjava.service.TriatlonService;
import ro.ubbcluj.scs.dfar2166.triatlonjava.utils.DependencyProvider;

import java.io.IOException;

// TriatlonLoginController

public class TriatlonLoginController {

    // Private Properties

    private final TriatlonService service = DependencyProvider.getInstance().getSharedService();

    // Outlets

    @FXML
    public TextField emailTextfield;

    @FXML
    public PasswordField passwordTextfield;

    // Private Methods

    private void showDashboard() {
        FXMLLoader fxmlLoader = new FXMLLoader(TriatlonApplication.class.getResource("triatlon-view.fxml"));
        Scene scene = null;
        try {
            scene = new Scene(fxmlLoader.load(), 480, 320);
        } catch (IOException e) {
            e.printStackTrace();
        }
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
        if (service.logInReferee(emailTextfield.getText(), passwordTextfield.getText())) showDashboard();
        else {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Eroare Login");
            alert.setHeaderText("Credentiale gresite");
            alert.setContentText("Combinatia email - parola nu este asociata niciunui cont de arbitru.");
            alert.showAndWait();
        }
    }

}
