/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Base
 *	
 */

package ${Project.PackageName}.data;

import java.sql.*;
import javax.sql.*;

public class Base {
    private static DataSource datasource;
    private Connection connection;
    
    
    public static void setDataSource(DataSource ds) {
        datasource = ds;
    }
    
    public Connection getConnection() throws Exception {
        if (connection!=null)
            return connection;
        
        connection = datasource.getConnection();
        return connection;
    }
    
    public void dispose() throws Exception {
        if (connection==null)
            return;
        
        connection.close();
        connection=null;
    }
}

