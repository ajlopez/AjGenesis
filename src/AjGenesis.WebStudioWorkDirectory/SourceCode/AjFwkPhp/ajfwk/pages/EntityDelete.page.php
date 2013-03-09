<?
	if (!$Entity)
		PageRedirect($Page->Prefix.'index.php');

	if (empty($Id))
		PageRedirect($Entity->SetDescriptor . '.php');

	include_once($Page->Prefix.'ajfwk/includes/Connection.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Pages.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Session.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Errors.inc.php');

	Connect();

	$sql = "Delete from $Cfg[SqlPrefix]$Entity->Table where Id = $Id";

	ExecuteQuery($sql);

	Disconnect();

	$EntityLink = SessionGet($Entity->Descriptor . "Link");
	SessionRemove($Entity->Descriptor . "Link");

	PageRedirect($Entity->SetDescriptor . '.php');
	exit;
?>