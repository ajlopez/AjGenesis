<?

$Cfg['Host'] = $HTTP_SERVER_VARS["HTTP_HOST"];

	include_once($Page->Prefix.'includes/Configuration.inc.php');

function IsLocalhost() {
	global $Cfg;

	if (strstr($Cfg['Host'],"127.0.0.1") || strstr($Cfg['Host'],"localhost"))
		return true;

	return false;
}

function IsRemote() {
	global $Host;

	if (!IsLocalhost())
		return true;

	return false;
}

?>