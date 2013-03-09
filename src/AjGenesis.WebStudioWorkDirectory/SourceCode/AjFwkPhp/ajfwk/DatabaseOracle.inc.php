<?

	include_once('Configuration.inc.php');
	
	$Connected = 0;

function DbConnect() {
	global $Connected;
	global $Cfg;
	global $OracleConn;

	if (!$Connected) {
		$OracleConn = OciLogon($Cfg['SqlUser'], $Cfg['SqlPassword'], $Cfg['SqlDatabase'])
		or
		die('No se pudo conectar a la base');
	}
		
	$Connected++;
}

function DbExecuteQuery($sql) {
	global $OracleConn;

	$stmt = OciParse($OracleConn,$sql);
	OciExecute($stmt);
	return $stmt;
}

function DbExecuteQueryInto($sql,&$result) {
	global $OracleConn;

	$stmt = OciParse($OracleConn,$sql);
	OciExecute($stmt);
	return OciFetchStatement($stmt,$result);
}

function DbExecuteUpdate($sql) {
	global $OracleConn;
	global $LastSqlError;

	$stmt = OciParse($OracleConn,$sql);
	@$r = OciExecute($stmt);

	if (!$r)
		$LastSqlError = OciError();
	else
		unset($LastSqlError);

	return $r;
}

function DbLastSqlError() {
	global $LastSqlError;

	$arr = $LastSqlError;
	if (is_array($arr))
		foreach ($arr as $key => $value)
			$txt .= "$key $value\n";
	return $txt;
}

function DbNextRow(&$stmt) {
	if (!OciFetchInto($stmt,$reg, OCI_ASSOC))
		return null;
	return $reg;
}

function DbDisconnect() {
	global $Connected;
	global $OracleConn;

	if ($Connected>1)
		$Connected--;
	else if ($Connected) {
		OciLogoff($OracleConn);
		$Connected=0;
	}
}
?>