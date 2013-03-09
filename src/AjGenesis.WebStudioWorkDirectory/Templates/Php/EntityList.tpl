<#
	include "Templates/EntityFunctions.tpl"

	Entity = List.Entity
	EntityIdProperty = IdProperty(Entity)
#>
<?
	$Page->Title = '${List.Title}';

	include_once($Page->Prefix . 'ajfwk/Database.inc.php');
	include_once($Page->Prefix . 'ajfwk/Tables.inc.php');
	include_once($Page->Prefix . 'ajfwk/Pages.inc.php');
	include_once($Page->Prefix . 'ajfwk/Session.inc.php');

	include_once($Page->Prefix . 'includes/Enumerations.inc.php');
<#
	if Technology.Programming.Strategy="Functions" then
#>
	include_once($Page->Prefix . 'includes/${Entity.Name}Functions.inc.php');
<#
		for each Column in List.Columns where Column.Property.Reference
#>
	include_once($Page->Prefix . 'includes/${Column.Property.Reference.Name}Functions.inc.php');
<#
		end for
	end if
#>

	SessionPut('${Entity.Name}Link',PageCurrent());

	DbConnect();

<#
	if Technology.Programming.Strategy="Functions" then
#>
	$rs = ${Entity.Name}GetList${List.QueryName}();
<#
	else
#>
	$sql = "select <# 
		print EntityIdProperty.Name
		for each Column in List.Columns
			print ", "
			print Column.Property.SqlColumn
			if Column.Property.SqlColumn <> Column.Property.Name then
				print " as " & Column.Property.Name
			end if
		end for #> from $Cfg[SqlPrefix]${Entity.SqlTable}";

<#
		if List.Order then
#>
	$sql .= " order by ${List.Order}";

<#
		end if
#>
	$rs = DbExecuteQuery($sql);
<#
	end if
#>

	$titles = array(<# 
		ncol = 0
		for each Column in List.Columns
			if ncol then
				print ", "
			end if
			print "'" & Column.Title & "'"
			ncol = ncol+1
		end for #>);

	include_once($Page->Prefix . 'includes/Header.inc.php');
?>

<center>

<p>
<a href="${Entity.Name}Form.php">New ${Entity.Descriptor}...</a>
<p>

<?		
	TableOpen($titles,"98%");

	while ($reg=DbNextRow($rs)) {
		RowOpen();
<#
	ncol = 0
	for each Column in List.Columns
		if ncol=0 then
#>
		DatumLinkGenerate($reg['${Column.Property.Name}'],"${Entity.Name}View.php?Id=".$reg['Id']);
<#
		else
			if Column.Property.Reference then
#>
		$ColumnDescription = ${Column.Property.Reference.Name}Translate($reg['${Column.Property.Name}']);
		DatumLinkGenerate($ColumnDescription, "${Column.Property.Reference.Name}View.php?Id=".$reg['${Column.Property.Name}']);
<#
			else
#>
		DatumGenerate($reg['${Column.Property.Name}']);
<#
			end if
		end if
		ncol=ncol+1
	end for
#>
		RowClose();
	}

	TableClose();
?>

</center>

<?
	include_once($Page->Prefix . 'includes/Footer.inc.php');
	DbDisconnect();
?>