package triatlon.client.controllers;

import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Scene;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Modality;
import javafx.stage.Stage;
import triatlon.client.DependencyProvider;
import triatlon.client.TriatlonApplication;
import triatlon.model.Result;
import triatlon.proto.Triatlon;
import triatlon.service.TriatlonObserverInterface;
import triatlon.service.TriatlonObserverProto;
import triatlon.service.TriatlonServiceProto;

import java.io.IOException;
import java.net.URL;
import java.util.List;
import java.util.ResourceBundle;

public class TriatlonController implements Initializable, TriatlonObserverProto {

    // Private Properties

    private final TriatlonServiceProto server = DependencyProvider.getInstance().getServer();
    ObservableList<Triatlon.Result> athletesResults = FXCollections.observableArrayList();
    private Stage addResultStage = null;

    // Outlets

    @FXML
    public Label refereeNameLabel;

    @FXML
    public Label refereeSubtitleLabel;

    @FXML
    public TableColumn<Triatlon.Result, Integer> idColumn;

    @FXML
    public TableColumn<Triatlon.Result, String> nameColumn;

    @FXML
    public TableColumn<Triatlon.Result, Integer> pointsColumn;

    @FXML
    public TableView<Triatlon.Result> athletesTable;

    // Lifecycle

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        idColumn.setCellValueFactory(new PropertyValueFactory<>("id"));
        nameColumn.setCellValueFactory(new PropertyValueFactory<>("name"));
        pointsColumn.setCellValueFactory(new PropertyValueFactory<>("points"));
        athletesResults.setAll(server.getAthletesWithTotalPoints());
        athletesTable.setItems(athletesResults);
    }

    public void refereeDidSet() {
        Triatlon.Referee referee = DependencyProvider.getInstance().getReferee();
        refereeNameLabel.setText(referee.getFirstName() + " " + referee.getLastName());
        refereeSubtitleLabel.setText("Arbitru " + referee.getRaceType());
    }

    // Private Methods

    private void showAddResult() {
        FXMLLoader fxmlLoader = new FXMLLoader(TriatlonApplication.class.getResource("/addresult-view.fxml"));
        Scene scene = null;
        try {
            scene = new Scene(fxmlLoader.load(), 320, 240);
        } catch (IOException e) {
            e.printStackTrace();
        }
        Stage stage = new Stage();
        stage.setTitle("Adaugare rezultat");
        stage.setScene(scene);
        stage.initModality(Modality.APPLICATION_MODAL);
        addResultStage = stage;
        stage.show();
    }

    private void showLeaderboard() {
        FXMLLoader fxmlLoader = new FXMLLoader(TriatlonApplication.class.getResource("/results-view.fxml"));
        Scene scene = null;
        try {
            scene = new Scene(fxmlLoader.load(), 600, 400);
        } catch (IOException e) {
            e.printStackTrace();
        }
        Stage stage = new Stage();
        stage.setTitle("Rezultate");
        stage.setScene(scene);
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.show();
    }

    // Actions

    public void onLogoutButtonClicked(ActionEvent event) {
        server.logOutReferee(DependencyProvider.getInstance().getReferee().getEmail());
        Stage stage = (Stage) refereeNameLabel.getScene().getWindow();
        stage.close();
    }

    public void onAddResultButtonClicked(ActionEvent event) {
        showAddResult();
    }

    public void onLeaderboardButtonClicked(ActionEvent event) {
        showLeaderboard();
    }

    @Override
    public void resultAdded(List<Triatlon.Result> results) {
        athletesResults.setAll(results);
        Platform.runLater(() -> {
            if (addResultStage != null) addResultStage.close();
            athletesTable.setItems(athletesResults);
        });
    }
}