<#
	include "Templates/EntityFunctions.tpl"
#>
<?
	include_once($Page->Prefix.'ajfwk/Database.inc.php');
	include_once($Page->Prefix.'ajfwk/Errors.inc.php');
	include_once($Page->Prefix.'ajfwk/Pages.inc.php');
	include_once($Page->Prefix.'ajfwk/Session.inc.php');

	if (!isset($Id))
		PageExit();

	$sql = "delete from $Cfg[SqlPrefix]${Entity.SqlTable} where Id = $Id";

	DbConnect();
	DbExecuteUpdate($sql);
	DbDisconnect();

	$Link = SessionGet("${Entity.Name}DeleteLink");
	SessionRemove("${Entity.Name}DeleteLink");

	if ($Link)
		PageAbsoluteRedirect($Link);
	else
		PageRedirect('${Entity.Name}List.php');

	exit;
?>