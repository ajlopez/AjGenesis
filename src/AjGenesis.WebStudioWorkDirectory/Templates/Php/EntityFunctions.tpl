<#
	include "Templates/EntityFunctions.tpl"
#>
<?

/*
 *	Functions
 * for Entity ${Entity.Name}
 *
 */

	include_once($Page->Prefix.'ajfwk/Database.inc.php');
	include_once($Page->Prefix.'ajfwk/Translations.inc.php');

function ${Entity.Name}GetById($Id) {
	global $Cfg;

	$sql = "select <#
	nprop = 0
	for each Property in Entity.Properties where Property.SqlColumn
		if nprop then
			print ", "
		end if
		print Property.SqlColumn
		if Property.SqlColumn <> Property.Name then
			print " as " & Property.Name
		end if
		nprop = nprop + 1
	end for
#> from $Cfg[SqlPrefix]${Entity.SqlTable} where Id = $Id";

	$rs = DbExecuteQuery($sql);
	return DbNextRow($rs);
}

function ${Entity.Name}GetList($where='',$order='') {
	global $Cfg;

	$sql = "select <#
	nprop = 0
	for each Property in Entity.Properties where Property.SqlColumn
		if nprop then
			print ", "
		end if
		print Property.SqlColumn
		if Property.SqlColumn <> Property.Name then
			print " as " & Property.Name
		end if
		nprop = nprop + 1
	end for
#> from $Cfg[SqlPrefix]${Entity.SqlTable}";

	if ($where)
		$sql .= " where $where";
	if (!$order)
		$order = 'Id';
	$sql .= " order by $order";

	return DbExecuteQuery($sql);
}

<#
	for each List in Entity.Lists where List.QueryName
#>
function ${Entity.Name}GetList${List.QueryName}($where='',$order='') {
	global $Cfg;

	$sql = "select <# 
		print Entity.IdProperty.Name
		for each Column in List.Columns
			print ", "
			print Column.Property.SqlColumn
			if Column.Property.SqlColumn <> Column.Property.Name then
				print " as " & Column.Property.Name
			end if
		end for #> from $Cfg[SqlPrefix]${Entity.SqlTable}";

	if ($where)
		$sql .= " where $where";
	if (!$order)
		$order = 'Id';
	$sql .= " order by $order";

	return DbExecuteQuery($sql);
}
<#
	end for
#>

function ${Entity.Name}GetView($where='',$order='') {
	global $Cfg;

}

//	function GetListBy...
//	function GetViewBy...

function ${Entity.Name}Insert(<#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn and not Property.Type="Id"
		if nprops then
			print ", "
		end if
		print "$" & Property.Name
		nprops = nprops + 1
	end for
#>) {
	global $Cfg;

	$sql = "insert $Cfg[SqlPrefix]${Entity.SqlTable} set
<#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn and not Property.Type="Id"
		if nprops then
			printline ","
		end if
		if Property.Type="Number" or Property.Type="IdRef" or Property.Type="Id" then
#>
		${Property.SqlColumn} = $${Property.Name}<#
	else
#>
		${Property.SqlColumn} = '$${Property.Name}'<#
	end if
	nprops = nprops + 1
end for
#>";

	DbExecuteUpdate($sql);

	return DbLastId();
}

function ${Entity.Name}Update(<#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn
		if nprops then
			print ", "
		end if
		print "$" & Property.Name
		nprops = nprops + 1
	end for
#>) {
	global $Cfg;

	$sql = "update $Cfg[SqlPrefix]${Entity.SqlTable} set
<#
	nprops = 0
	for each Property in Entity.Properties where Property.SqlColumn and not Property.Type="Id"
		if nprops then
			printline ","
		end if
		if Property.Type="Number" or Property.Type="IdRef" or Property.Type="Id" then
#>
		${Property.SqlColumn} = $${Property.Name}<#
	else
#>
		${Property.SqlColumn} = '$${Property.Name}'<#
	end if
	nprops = nprops + 1
end for
#> where ${Entity.IdProperty.SqlColumn} = $${Entity.IdProperty.Name}";

	DbExecuteUpdate($sql);
}

function ${Entity.Name}Delete($Id) {
	global $Cfg;

	$sql = "delete from $Cfg[SqlPrefix]${Entity.SqlTable} where ${Entity.IdProperty.SqlColumn} = $Id";
	DbExecuteUpdate($sql);
}

function ${Entity.Name}Translate($Id) {
	global $${Entity.Name}Names;
	global $Cfg;

	if ($${Entity.Name}Names[$Id])
		return $${Entity.Name}Names[$Id];
<#
	DescriptorProperty = GetDescriptorProperty(Entity)
#>

	$description = TranslateDescription("$Cfg[SqlPrefix]${Entity.SqlTable}",$Id,"${DescriptorProperty.SqlColumn}");

	$${Entity.Name}Names[$Id] = $description;

	return $description;
}

<#
	for each Property in Entity.Properties where Property.Reference
#>

function ${Entity.Name}GetBy${Property.Reference.Name}($${Property.Name}) {
	return ${Entity.Name}GetList("${Property.Name} = $${Property.Name}");
}
<#
	end for
#>

?>