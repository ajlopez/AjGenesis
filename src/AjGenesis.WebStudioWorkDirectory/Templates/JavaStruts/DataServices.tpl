/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Data Services
 *	
 */

package ${Project.PackageName}.services;

import ${Project.PackageName}.data.Base;

import javax.sql.DataSource;

public class DataServices {

    public static void setDataSource(DataSource ds) {
        Base.setDataSource(ds);
    }
}
