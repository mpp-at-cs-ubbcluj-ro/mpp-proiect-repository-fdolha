package repository.db;

import model.activity.RaceType;
import model.person.Referee;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import repository.RefereeRepository;
import utils.JDBCUtils;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

// RefereeDBRepository

public class RefereeDBRepository implements RefereeRepository {

    // Private Properties

    private final JDBCUtils databaseUtils;

    // Private Class Properties

    private static final Logger logger = LogManager.getLogger();

    // Lifecycle

    public RefereeDBRepository(Properties properties) {
        logger.info("Initialising RefereeDBRepository with properties: {}", properties);
        databaseUtils = new JDBCUtils(properties);
    }

    // Override Methods (Repository)

    @Override
    public Referee findOne(Integer integer) {
        logger.traceEntry("Finding task {}", integer);
        Connection connection = databaseUtils.getConnection();
        Referee referee = null;
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from referees where id=?")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    var id = resultSet.getInt("id");
                    var firstName = resultSet.getString("firstName");
                    var lastName = resultSet.getString("lastName");
                    var raceType = resultSet.getInt("raceType");
                    var email = resultSet.getString("email");
                    var password = resultSet.getString("password");
                    referee = new Referee(firstName, lastName, RaceType.fromValue(raceType), email, password);
                    referee.setId(id);
                }
                logger.traceExit("Found 1 instance");
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return referee;
    }

    @Override
    public Iterable<Referee> findAll() {
        logger.traceEntry("Finding task");
        Connection connection = databaseUtils.getConnection();
        List<Referee> referees = new ArrayList<>();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from referees")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    var id = resultSet.getInt("id");
                    var firstName = resultSet.getString("firstName");
                    var lastName = resultSet.getString("lastName");
                    var raceType = resultSet.getInt("raceType");
                    var email = resultSet.getString("email");
                    var password = resultSet.getString("password");
                    Referee referee = new Referee(firstName, lastName, RaceType.fromValue(raceType), email, password);
                    referee.setId(id);
                    referees.add(referee);
                }
                logger.traceExit("Found {} instances", referees.size());
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return referees;
    }

    @Override
    public Referee save(Referee entity) {
        logger.traceEntry("Saving task {}", entity);
        Connection connection = databaseUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("insert into referees (firstName, lastName, raceType, email, password) values (?, ?, ?, ?, ?)")) {
            preparedStatement.setString(1, entity.getFirstName());
            preparedStatement.setString(2, entity.getLastName());
            preparedStatement.setInt(3, entity.getRaceType().getValue());
            preparedStatement.setString(4, entity.getEmail());
            preparedStatement.setString(5, entity.getPassword());
            int result = preparedStatement.executeUpdate();
            logger.traceExit("Saved {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return entity;
    }

    @Override
    public Integer delete(Integer integer) {
        logger.traceEntry("Deleting task {}", integer);
        Connection connection = databaseUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("delete from referees where id=?")) {
            preparedStatement.setInt(1, integer);
            int result = preparedStatement.executeUpdate();
            logger.traceExit("Deleted {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return integer;
    }

    @Override
    public Referee update(Referee entity) {
        logger.traceEntry("Updating task {}", entity);
        Connection connection = databaseUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("update referees set firstName=?, lastName=?, raceType=?, email=?, password=? where id=?")) {
            preparedStatement.setString(1, entity.getFirstName());
            preparedStatement.setString(2, entity.getLastName());
            preparedStatement.setInt(3, entity.getRaceType().getValue());
            preparedStatement.setString(4, entity.getEmail());
            preparedStatement.setString(5, entity.getPassword());
            preparedStatement.setInt(6, entity.getId());
            int result = preparedStatement.executeUpdate();
            logger.traceExit("Updated {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return entity;
    }

}
