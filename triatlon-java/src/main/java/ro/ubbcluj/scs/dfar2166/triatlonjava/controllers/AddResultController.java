package ro.ubbcluj.scs.dfar2166.triatlonjava.controllers;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import ro.ubbcluj.scs.dfar2166.triatlonjava.service.TriatlonService;
import ro.ubbcluj.scs.dfar2166.triatlonjava.utils.DependencyProvider;

import java.net.URL;
import java.util.ResourceBundle;

// AddResultController

public class AddResultController implements Initializable {

    // Private Properties

    private final TriatlonService service = DependencyProvider.getInstance().getSharedService();
    ObservableList<String> athletesName = FXCollections.observableArrayList();

    // Outlets

    @FXML
    public ComboBox<String> athleteComboBox;

    @FXML
    public TextField pointsTextfield;

    // Lifecycle

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        pointsTextfield.textProperty().addListener((observable, oldValue, newValue) -> {
            if (!newValue.matches("\\d*")) {
                pointsTextfield.setText(newValue.replaceAll("[^\\d]", ""));
            }
        });
        service.getAthletes().forEach(athlete -> athletesName.add(athlete.getId().toString() + " - " + athlete.getFullName()));
        athleteComboBox.setItems(athletesName);
    }

    // Private Methods

    private void dismissScreen() {
        Stage stage = (Stage) pointsTextfield.getScene().getWindow();
        stage.close();
    }

    private void showEmptyAlert() {
        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setTitle("Eroare");
        alert.setHeaderText("Niciun participant selectat");
        alert.setContentText("Te rugam sa alegi un participant pentru a introduce un rezultat.");
        alert.showAndWait();
    }

    // Actions

    public void onAddButtonClicked(ActionEvent event) {
        if (athleteComboBox.getSelectionModel().isEmpty()) { showEmptyAlert(); return; }
        String value = athleteComboBox.getValue();
        var end = 1;
        for (int i = 2; i < value.length(); i++) {
            if (value.charAt(i-1) == ' ') break;
            end = i;
        }
        Integer athleteId = Integer.parseInt(value.substring(0, end));
        int points = 0;
        try {
            points = Integer.parseInt(pointsTextfield.getText());
        } catch (Exception ignored) {}
        service.addResult(athleteId, points);
        dismissScreen();
    }

    public void onCloseButtonClicked(ActionEvent event) {
        dismissScreen();
    }
}
