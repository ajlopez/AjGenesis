<#
	include "Templates/EntityFunctions.tpl"

	Entity = Form.Entity
#>
<?
	include_once($Page->Prefix.'ajfwk/Database.inc.php');
	include_once($Page->Prefix.'ajfwk/PostParameters.inc.php');
	include_once($Page->Prefix.'ajfwk/Session.inc.php');
	include_once($Page->Prefix.'ajfwk/Errors.inc.php');
	include_once($Page->Prefix.'ajfwk/Validations.inc.php');
	include_once($Page->Prefix.'ajfwk/Pages.inc.php');

<#
	for each Field in Form.Fields where Field.Required
#>
	if (empty($${Field.Property.Name}))
		ErrorAdd('Debe ingresar ${Field.Property.Description}');

<#
	end for
#>
	DbConnect();

	if (ErrorHas()) {
		DbDisconnect();
		include('${Entity.Name}Form.php');
		exit;
	}

	if (empty($Id))
		$sql = "Insert";
	else
		$sql = "Update";

	$sql .= " $Cfg[SqlPrefix]${Entity.SqlTable} set
<#
	nflds = 0
	for each Field in Form.Fields where Field.Property.Type<>"Id"
		if nfld then
			PrintLine ", "
		end if
		nfld = nfld+1
		if Field.Property.Type="IdRef" then
#>
		${Field.Property.Name} = $${Field.Property.Name} <#
		else
#>
		${Field.Property.Name} = '$${Field.Property.Name}' <#
		end if
	end for
#>
		";

	if (!empty($Id))
		$sql .= " where Id=$Id";

	DbExecuteUpdate($sql);

	DbDisconnect();

	$Link = SessionGet("${Entity.Name}Link");
	SessionRemove("${Entity.Name}Link");

	PageAbsoluteRedirect($Link);
	exit;
?>