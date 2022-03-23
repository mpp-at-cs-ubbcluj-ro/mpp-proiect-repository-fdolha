package ro.ubbcluj.scs.dfar2166.triatlonjava.controllers;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.Result;
import ro.ubbcluj.scs.dfar2166.triatlonjava.service.TriatlonService;
import ro.ubbcluj.scs.dfar2166.triatlonjava.utils.DependencyProvider;

import java.net.URL;
import java.util.ResourceBundle;

// ResultsController

public class ResultsController implements Initializable {

    // Private Properties

    private final TriatlonService service = DependencyProvider.getInstance().getSharedService();
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
        resultsLabel.setText("Rezultate " + service.getCurrentReferee().getRaceType().toString());
        results.setAll(service.getParticipantsWithResultInRace());
        resultsTableView.setItems(results);
    }
}
