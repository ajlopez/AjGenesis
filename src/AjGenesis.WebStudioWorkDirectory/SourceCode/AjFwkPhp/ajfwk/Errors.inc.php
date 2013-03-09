<?
if (!isset($Errors))
	$Errors = Array();

function ErrorAdd($message) {
	global $Errors;

	$Errors[] = $message;
}

function ErrorHas() {
	global $Errors;

	return count($Errors)>0;
}

function ErrorRender() {
	global $Errors;

	if (!count($Errors))
		return;

	echo '<p class="error">';

	foreach ($Errors as $err)
		echo $err . '<br>';
	echo '</p>';
}

function ErrorShow($message,$enlace='') {
	global $Page;

	header("Location: ".$Page->Prefix."errors.php?Message=".urlencode($message)."&Link=".urlencode($link));
	exit();
}

function ErrorSql() {
	$msg = LastSqlError();
	ErrorShow($msg);
}
?>