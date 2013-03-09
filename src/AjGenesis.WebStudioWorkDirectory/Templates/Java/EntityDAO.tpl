<#

'	Data Access Object for Entity Generator
'	for Java

message	"Processing Entity ${Entity.Name}"

ObjName = Entity.JavaObjectName

#>

/*
 *	Project	${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.data;

import java.util.*;
import java.sql.*;

import ${Project.PackageName}.model.*;

public class ${Entity.Name}DAO {
	private Base base;

	public ${Entity.Name}DAO(Base base) {
		this.base = base;
	}

	public ${Entity.Name} getBy${Entity.IdProperty.Name}(${Entity.IdProperty.JavaType} ${Entity.IdProperty.JavaName}) throws Exception {
		Connection con = base.getConnection();

		PreparedStatement stmt = con.prepareStatement("select <#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn
		if nprops then
			print ", "
		end if
		if Property.Name.ToLower()=Property.SqlColumn.ToLower() then
			print Property.Name
		else
			print Property.SqlColumn & " as " & Property.Name
		end if
		nprops = nprops + 1
	end for
#> from ${Technology.Database.Prefix}${Entity.SqlTable} where ${Entity.IdProperty.SqlColumn} = ?");

		stmt.setInt(1,${Entity.IdProperty.JavaName});

		ResultSet rs = stmt.executeQuery();

		${Entity.Name} ${ObjName};

		if (rs.next())
			${ObjName} = make(rs);
		else
			${ObjName} = null;

		rs.close();
		stmt.close();
		con.close();

		return ${ObjName};
	}
<#
	for each RefProperty in Entity.Properties where RefProperty.Reference
#>

	public List getBy${RefProperty.Reference.Name}(${RefProperty.JavaType} ${RefProperty.JavaName}) throws Exception {
		List list = new ArrayList();

		Connection con = base.getConnection();

		PreparedStatement stmt = con.prepareStatement("select <#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn
		if nprops then
			print ", "
		end if
		if Property.Name.ToLower()=Property.SqlColumn.ToLower() then
			print Property.Name
		else
			print Property.SqlColumn & " as " & Property.Name
		end if
		nprops = nprops + 1
	end for
#> from ${Technology.Database.Prefix}${Entity.SqlTable} where ${RefProperty.SqlColumn} = ?");

		stmt.setInt(1,${RefProperty.JavaName});

		ResultSet rs = stmt.executeQuery();

		while (rs.next())
			list.add(make(rs));

		rs.close();
		stmt.close();
		con.close();

		return list;
	}
<#
	end for
#>

	public List getAll() throws Exception {
		List list = new ArrayList();

		Connection con = base.getConnection();

		PreparedStatement stmt = con.prepareStatement("select <#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn
		if nprops then
			print ", "
		end if
		if Property.Name.ToLower()=Property.SqlColumn.ToLower() then
			print Property.Name
		else
			print Property.SqlColumn & " as " & Property.Name
		end if
		nprops = nprops + 1
	end for
#> from ${Technology.Database.Prefix}${Entity.SqlTable} order by ${Entity.IdProperty.SqlColumn}");

		ResultSet rs = stmt.executeQuery();

		while (rs.next())
			list.add(make(rs));

		rs.close();
		stmt.close();
		con.close();

		return list;
	}

	public void insert(${Entity.Name} ${ObjName}) throws Exception {
		Connection con = base.getConnection();

		PreparedStatement stmt = con.prepareStatement("insert into ${Technology.Database.Prefix}${Entity.SqlTable}(<#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn and Property.Type<>"Id"
		if nprops then
			print ", "
		end if
		print Property.SqlColumn
		nprops = nprops + 1
	end for
#>) values(<#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn and Property.Type<>"Id"
		if nprops then
			print ", "
		end if
		print "?"
		nprops = nprops + 1
	end for
#>)");

<#
	nprops = 1
	for each Property in Entity.Properties where Property.SqlColumn and Property.Type<>"Id"
#>
		stmt.set${Property.JavaSqlType}(${nprops},${ObjName}.get${Property.Name}());
<#
		nprops = nprops+1
	end for
#>

		stmt.executeUpdate();

		stmt.close();
		con.close();
	}

	public void update(${Entity.Name} ${ObjName}) throws Exception {
		Connection con = base.getConnection();

		PreparedStatement stmt = con.prepareStatement("update ${Technology.Database.Prefix}${Entity.SqlTable} set <#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn and Property.Type<>"Id"
		if nprops then
			print ", "
		end if
		print Property.SqlColumn & " = ?"
		nprops = nprops + 1
	end for
#> where ${Entity.IdProperty.SqlColumn} = ?");

<#
	nprops = 1
	for each Property in Entity.Properties where Property.SqlColumn and Property.Type<>"Id"
#>
		stmt.set${Property.JavaSqlType}(${nprops},${ObjName}.get${Property.Name}());
<#
		nprops = nprops+1
	end for
#>
		stmt.set${Entity.IdProperty.JavaSqlType}(${nprops},${ObjName}.get${Entity.IdProperty.Name}());

		stmt.executeUpdate();

		stmt.close();
		con.close();
	}

	public void delete(${Entity.Name} ${ObjName}) throws Exception {
	}

	private ${Entity.Name} make(ResultSet rs) throws Exception {
		${Entity.Name} ${ObjName} = new ${Entity.Name}();

<#
	for each Property in Entity.Properties where Property.SqlColumn
#>
		${ObjName}.set${Property.Name}(rs.get${Property.JavaSqlType}("${Property.Name}"));
<#
	end for
#>

		return ${ObjName};
	}
}

