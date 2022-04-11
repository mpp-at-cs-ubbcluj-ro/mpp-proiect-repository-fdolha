package triatlon.client.controllers;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import triatlon.client.DependencyProvider;
import triatlon.model.Result;
import triatlon.model.activity.RaceType;
import triatlon.service.TriatlonServiceInterface;

import java.net.URL;
import java.util.ResourceBundle;

// ResultsController

public class ResultsController implements Initializable {

    // Private Properties

    private final TriatlonServiceInterface server = DependencyProvider.getInstance().getServer();
    ObservableList<Result> results = FXCollections.observableArrayList();

    // Outlets

    @FXML
    public Label resultsLabel;

    @FXML
    public TableColumn<Result, Integer> idColumn;

    @FXML
    public TableColumn<Result, String> nameColumn;

    @FXML
    public TableColumn<Result, Integer> pointsColumn;

    @FXML
    public TableView<Result> resultsTableView;

    // Lifecycle

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        idColumn.setCellValueFactory(new PropertyValueFactory<>("id"));
        nameColumn.setCellValueFactory(new PropertyValueFactory<>("name"));
        pointsColumn.setCellValueFactory(new PropertyValueFactory<>("points"));
        RaceType raceType = DependencyProvider.getInstance().getReferee().getRaceType();
        resultsLabel.setText("Rezultate " + raceType.toString());
        results.setAll(server.getParticipantsWithResultInRace(raceType));
        resultsTableView.setItems(results);
    }
}
