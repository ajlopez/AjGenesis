<?
	if (!$Entity)
		PageRedirect($Page->Prefix.'index.php');

	include_once($Page->Prefix.'ajfwk/includes/Connection.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Users.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Session.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Errors.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Validations.inc.php');

	if (ErrorHas()) {
		include($Entity->Descriptor . 'form.php');
		exit;
	}

	Connect();

	if (empty($Id))
		$sql = "Insert";
	else
		$sql = "Update";

	$sql .= " $Cfg[SqlPrefix]$Entity->Table set ";

	foreach ($Entity->Fields as $fld)
		if ($fld->Name != 'Id') {
			$name = $fld->Name;
			$value = $$name;
			$sql .= " $name = '$value'";
		}

	if (!empty($Id))
		$sql .= " where Id=$Id";

	ExecuteQuery($sql);

	Disconnect();

	$EntityLink = SessionGet($Entity->Descriptor . "Link");
	SessionRemove($Entity->Descriptor . "Link");

	if (empty($EntityLink))
		PageRedirect($Entity->SetDescriptor . '.php');
	else
		PageAbsoluteRedirect($EntityLink);

	exit;
?>