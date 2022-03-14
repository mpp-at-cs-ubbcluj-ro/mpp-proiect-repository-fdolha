package utils;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;

// JDBCUtils

public class JDBCUtils {

    // Private Properties

    private Properties properties;
    private Connection instance = null;

    // Private Class Properties

    private static final Logger logger = LogManager.getLogger();

    // Lifecycle

    public JDBCUtils(Properties properties) {
        this.properties = properties;
    }

    // Instance Methods

    public Connection getConnection() {
        logger.traceEntry();
        try {
            if (instance == null | instance.isClosed()) instance = getNewConnection();
        } catch (SQLException ex) {
            logger.error(ex);
        }
        logger.traceExit(instance);
        return instance;
    }

    // Private Methods

    private Connection getNewConnection() {
        logger.traceEntry();
        var url = properties.getProperty("jdbc.url");
        var user = properties.getProperty("jdbc.user");
        var password = properties.getProperty("jdbc.pass");
        logger.info("Trying to connect to database ... {}", url);
        logger.info("User: {}", user);
        logger.info("Password: {}", password);
        Connection connection = null;
        try {
            if (user != null && password != null) connection = DriverManager.getConnection(url, user, password);
            else connection = DriverManager.getConnection(url);
        } catch (SQLException ex) {
            logger.error(ex);
        }
        return connection;
    }
    
}