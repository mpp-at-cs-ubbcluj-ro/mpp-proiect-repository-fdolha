package ro.ubbcluj.scs.dfar2166.triatlonjava.repository.db;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.person.Athlete;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.AthleteRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.utils.JDBCUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

// AthleteDBRepository

public class AthleteDBRepository implements AthleteRepository {

    // Private Properties

    private final JDBCUtils databaseUtils;

    // Private Class Properties

    private static final Logger logger = LogManager.getLogger();

    // Lifecycle

    public AthleteDBRepository(Properties properties) {
        logger.info("Initialising AthleteDBRepository with properties: {}", properties);
        databaseUtils = new JDBCUtils(properties);
    }

    // Override Methods (Repository)

    @Override
    public Athlete findOne(Integer integer) {
        logger.traceEntry("Finding task {}", integer);
        Connection connection = databaseUtils.getConnection();
        Athlete athlete = null;
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from athletes where id=?")) {
            preparedStatement.setInt(1, integer);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    var id = resultSet.getInt("id");
                    var firstName = resultSet.getString("firstName");
                    var lastName = resultSet.getString("lastName");
                    athlete = new Athlete(firstName, lastName);
                    athlete.setId(id);
                }
                logger.traceExit("Found 1 instance");
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return athlete;
    }

    @Override
    public Iterable<Athlete> findAll() {
        logger.traceEntry("Finding task");
        Connection connection = databaseUtils.getConnection();
        List<Athlete> athletes = new ArrayList<>();
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from athletes")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    var id = resultSet.getInt("id");
                    var firstName = resultSet.getString("firstName");
                    var lastName = resultSet.getString("lastName");
                    Athlete athlete = new Athlete(firstName, lastName);
                    athlete.setId(id);
                    athletes.add(athlete);
                }
                logger.traceExit("Found {} instances", athletes.size());
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return athletes;
    }

    @Override
    public Athlete save(Athlete entity) {
        logger.traceEntry("Saving task {}", entity);
        Connection connection = databaseUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("insert into athletes (firstName, lastName) values (?, ?)")) {
            preparedStatement.setString(1, entity.getFirstName());
            preparedStatement.setString(2, entity.getLastName());
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
        try (PreparedStatement preparedStatement = connection.prepareStatement("delete from athletes where id=?")) {
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
    public Athlete update(Athlete entity) {
        logger.traceEntry("Updating task {}", entity);
        Connection connection = databaseUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement("update athletes set firstName=?, lastName=? where id=?")) {
            preparedStatement.setString(1, entity.getFirstName());
            preparedStatement.setString(2, entity.getLastName());
            preparedStatement.setInt(3, entity.getId());
            int result = preparedStatement.executeUpdate();
            logger.traceExit("Updated {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return entity;
    }

}
