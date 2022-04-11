package triatlon.repository;

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
            if (instance == null) {
                instance = getNewConnection();
            }
        logger.traceExit(instance);
        return instance;
    }

    // Private Methods

    private Connection getNewConnection() {
        logger.traceEntry();
        var url = properties.getProperty("jdbc.url");
        var username = properties.getProperty("jdbc.username");
        var password = properties.getProperty("jdbc.password");
        var driver = properties.getProperty("jdbc.driver");
        logger.info("Trying to connect to database ... {}", url);
        logger.info("Username: {}", username);
        logger.info("Password: {}", password);
        Connection connection = null;
        try { Class.forName(driver); } catch (Exception ignored) {}
        try {
            if (username != null && password != null) connection = DriverManager.getConnection(url, username, password);
            else connection = DriverManager.getConnection(url);
        } catch (SQLException ex) {
            logger.error(ex);
        }
        return connection;
    }
    
}