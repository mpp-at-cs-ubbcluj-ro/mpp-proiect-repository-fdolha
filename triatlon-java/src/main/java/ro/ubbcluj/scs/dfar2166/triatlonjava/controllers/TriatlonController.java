package ro.ubbcluj.scs.dfar2166.triatlonjava.controllers;

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
import ro.ubbcluj.scs.dfar2166.triatlonjava.TriatlonApplication;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.Result;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.person.Referee;
import ro.ubbcluj.scs.dfar2166.triatlonjava.service.TriatlonService;
import ro.ubbcluj.scs.dfar2166.triatlonjava.utils.DependencyProvider;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class TriatlonController implements Initializable {

    // Private Properties

    private final TriatlonService service = DependencyProvider.getInstance().getSharedService();
    ObservableList<Result> athletesResults = FXCollections.observableArrayList();

    // Outlets

    @FXML
    public Label refereeNameLabel;

    @FXML
    public Label refereeSubtitleLabel;

    @FXML
    public TableColumn<Result, Integer> idColumn;

    @FXML
    public TableColumn<Result, String> nameColumn;

    @FXML
    public TableColumn<Result, Integer> pointsColumn;

    @FXML
    public TableView<Result> athletesTable;

    // Lifecycle

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        idColumn.setCellValueFactory(new PropertyValueFactory<>("id"));
        nameColumn.setCellValueFactory(new PropertyValueFactory<>("name"));
        pointsColumn.setCellValueFactory(new PropertyValueFactory<>("points"));
        Referee referee = service.getCurrentReferee();
        refereeNameLabel.setText(referee.getFirstName() + " " + referee.getLastName());
        refereeSubtitleLabel.setText("Arbitru " + referee.getRaceType().toString());
        athletesResults.setAll(service.getAthletesWithTotalPoints());
        athletesTable.setItems(athletesResults);
    }

    // Private Methods

    private void showAddResult() {
        FXMLLoader fxmlLoader = new FXMLLoader(TriatlonApplication.class.getResource("addresult-view.fxml"));
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
        stage.show();
    }

    private void showLeaderboard() {
        FXMLLoader fxmlLoader = new FXMLLoader(TriatlonApplication.class.getResource("results-view.fxml"));
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
        service.logOutReferee();
        Stage stage = (Stage) refereeNameLabel.getScene().getWindow();
        stage.close();
    }

    public void onAddResultButtonClicked(ActionEvent event) {
        showAddResult();
    }

    public void onLeaderboardButtonClicked(ActionEvent event) {
        showLeaderboard();
    }
}