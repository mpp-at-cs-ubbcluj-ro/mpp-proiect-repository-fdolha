package triatlon.repository.db;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import triatlon.model.activity.AthletePoints;
import triatlon.model.activity.Race;
import triatlon.model.activity.RaceType;
import triatlon.repository.JDBCUtils;
import triatlon.repository.RaceRepository;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

// RaceDBRepository

public class RaceDBRepository extends RaceRepository {

    // Private Properties

    private final JDBCUtils databaseUtils;

    // Private Class Properties

    private static final Logger logger = LogManager.getLogger();

    // Lifecycle

    public RaceDBRepository(Properties properties, RaceType raceType) {
        super(raceType);
        logger.info("Initialising RaceDBRepository with properties: {}", properties);
        databaseUtils = new JDBCUtils(properties);
    }

    public RaceDBRepository(Properties properties, Race race) {
        super(race);
        logger.info("Initialising RaceDBRepository with properties: {}", properties);
        databaseUtils = new JDBCUtils(properties);
    }

    // Private Methods

    private String tableNameForRaceType(RaceType raceType) {
        switch (raceType) {
            case SWIMMING: return "swimming_race";
            case CYCLING: return "cycling_race";
            case RUNNING: return "running_race";
            default: return "";
        }
    }

    // Private Methods

    private AthletePoints buildAthletePointsFromResultSet(ResultSet resultSet) throws SQLException {
        AthletePoints athletePoints = null;
        var id = resultSet.getInt("id");
        var athleteId = resultSet.getInt("athleteid");
        var points = resultSet.getInt("points");
        athletePoints = new AthletePoints(athleteId, points);
        athletePoints.setId(id);
        return athletePoints;
    }

    // Override Methods (RaceRepository)

    @Override
    public AthletePoints findByAthleteId(Integer athleteId) {
        logger.traceEntry();
        Connection connection = databaseUtils.getConnection();
        AthletePoints athletePoints = null;
        var tableName = tableNameForRaceType(getRaceType());
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from " + tableName + " where athleteId=?")) {
            preparedStatement.setInt(1, athleteId);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    athletePoints = buildAthletePointsFromResultSet(resultSet);
                }
                logger.traceExit("Found 1 instance");
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return athletePoints;
    }

    @Override
    public Iterable<AthletePoints> findAllWithPoints() {
        logger.traceEntry("Finding task");
        Connection connection = databaseUtils.getConnection();
        List<AthletePoints> athletesPoints = new ArrayList<>();
        var tableName = tableNameForRaceType(getRaceType());
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from " + tableName + " where points > 0")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    AthletePoints athletePoints = buildAthletePointsFromResultSet(resultSet);
                    athletesPoints.add(athletePoints);
                }
                logger.traceExit("Found {} instances", athletesPoints.size());
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return athletesPoints;
    }

    // Override Methods (Repository)

    @Override
    public AthletePoints findOne(Integer integer) {
        logger.traceEntry();
        Connection connection = databaseUtils.getConnection();
        AthletePoints athletePoints = null;
        var tableName = tableNameForRaceType(getRaceType());
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from " + tableName + " where id=?")) {
            preparedStatement.setInt(1, integer);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    athletePoints = buildAthletePointsFromResultSet(resultSet);
                }
                logger.traceExit("Found 1 instance");
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return athletePoints;
    }

    @Override
    public Iterable<AthletePoints> findAll() {
        logger.traceEntry("Finding task");
        Connection connection = databaseUtils.getConnection();
        List<AthletePoints> athletesPoints = new ArrayList<>();
        var tableName = tableNameForRaceType(getRaceType());
        try (PreparedStatement preparedStatement = connection.prepareStatement("select * from " + tableName)) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    AthletePoints athletePoints = buildAthletePointsFromResultSet(resultSet);
                    athletesPoints.add(athletePoints);
                }
                logger.traceExit("Found {} instances", athletesPoints.size());
            }
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit();
        return athletesPoints;
    }

    @Override
    public AthletePoints save(AthletePoints entity) {
        logger.traceEntry("Saving task {}", entity);
        Connection connection = databaseUtils.getConnection();
        var tableName = tableNameForRaceType(getRaceType());
        try (PreparedStatement preparedStatement = connection.prepareStatement("insert into " + tableName + " (athleteId, points) values (?, ?)")) {
            preparedStatement.setInt(1, entity.getAthleteId());
            preparedStatement.setInt(2, entity.getPoints());
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
        var tableName = tableNameForRaceType(getRaceType());
        try (PreparedStatement preparedStatement = connection.prepareStatement("delete from " + tableName + " where id=?")) {
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
    public AthletePoints update(AthletePoints entity) {
        logger.traceEntry("Updating task {}", entity);
        Connection connection = databaseUtils.getConnection();
        var tableName = tableNameForRaceType(getRaceType());
        try (PreparedStatement preparedStatement = connection.prepareStatement("update " + tableName + " set athleteId=?, points=? where id=?")) {
            preparedStatement.setInt(1, entity.getAthleteId());
            preparedStatement.setInt(2, entity.getPoints());
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
