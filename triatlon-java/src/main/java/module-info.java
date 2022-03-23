module ro.ubbcluj.scs.dfar2166.triatlonjava {
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;
    requires org.kordamp.bootstrapfx.core;
    requires org.apache.logging.log4j;
    requires java.sql;

    opens ro.ubbcluj.scs.dfar2166.triatlonjava to javafx.fxml;
    exports ro.ubbcluj.scs.dfar2166.triatlonjava;

    opens ro.ubbcluj.scs.dfar2166.triatlonjava.model to javafx.fxml;
    exports ro.ubbcluj.scs.dfar2166.triatlonjava.model;
    exports ro.ubbcluj.scs.dfar2166.triatlonjava.controllers;
    opens ro.ubbcluj.scs.dfar2166.triatlonjava.controllers to javafx.fxml;
}