<?
	include_once('Configuration.inc.php');

	$Connected = 0;

function DbConnect() {
	global $Connected;
	global $Cfg;

	if (!$Connected) {
		mysql_pconnect($Cfg['SqlHost'], $Cfg['SqlUser'], $Cfg['SqlPassword']);
		if (mysql_errno())
			echo mysql_error();
	}
		
	mysql_select_db($Cfg['SqlBase']);
	$Connected++;
}

function DbDisconnect() {
	global $Connected;

	if ($Connected>1)
		$Connected--;
	else if ($Connected) {
		mysql_close();
		$Connected=0;
	}
}

function DbExecuteQuery($sql) {
	return mysql_query($sql);
}

function DbExecuteUpdate($sql) {
	mysql_query($sql);
}

function DbLastSqlError() {
	return mysql_error();
}

function DbNextRow(&$stmt) {
	return mysql_fetch_array($stmt);
}

function DbNextObject(&$stmt) {
	return mysql_fetch_object($stmt);
}

function DbFreeResult(&$stmt) {
	return mysql_free_result($stmt);
}

function DbNumRows(&$stmt) {
	return mysql_num_rows($stmt);
}

function DbLastId() {
	return mysql_insert_id();
}

?>