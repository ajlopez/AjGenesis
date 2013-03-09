<?
	if (!$Entity)
		PageRedirect($Page->Prefix.'index.php');

	include_once($Page->Prefix.'ajfwk/includes/Connection.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Users.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Session.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Errors.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Validations.inc.php');

	if (empty($Description))
		ErrorAdd('Debe ingresar Descripci&oacute;n');

	if (ErrorHas()) {
		include($Entity->Entity . 'form.php');
		exit;
	}

	Connect();

	$IdGenre += 0;
	$IdCountry += 0;

	if (empty($Id))
		$sql = "Insert";
	else
		$sql = "Update";

	$sql .= " $Entity->Table set Description = '$Description'";

	if (!empty($Id))
		$sql .= " where Id=$Id";

	ExecuteUpdate($sql);

	Disconnect();

	$EntityLink = SessionGet($Entity->Entity. "Link");
	SessionRemove($Entity->Entity . "Link");

	PageAbsoluteRedirect($EntityLink);
	exit;
?>