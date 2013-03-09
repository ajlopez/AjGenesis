<#

rem		Data Access Object Generartor
rem		for Java


include	"Templates/Java/JavaFunctions.tpl"
include	"Templates/EntityFunctions.tpl"

message	"Processing Entity DAO ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Data Access Object	${Entity.Name}DAO
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.data;

import java.sql.*;

public class Base {
    private Connection connection;
    
    static {
        try {
            Class.forName(getDriverName()).newInstance();        
        }
        catch (Exception e) {}
    }
    
    static String getDriverName() {
        return "${Technology.JDBC.Driver}";
    }
    
    static String getURL() {
        return "jdbc:${Technology.JDBC.Subprotocol}://${Technology.Database.Host}/${Technology.Database.Name}?user=${Technology.Database.Username}&password=${Technology.Database.Password}";
    }
    
    public Connection getConnection() throws Exception {
        if (connection!=null)
            return connection;
        
        try {
            connection = DriverManager.getConnection(getURL());
            return connection;
        }
        catch (SQLException e) {
            throw e;
        }
    }
    
    public void dispose() throws Exception {
        if (connection==null)
            return;
        
        try {
            connection.close();
            connection=null;
        }
        catch (SQLException e) {
            throw e;
        }
    }
}

