<?

function DateValidate($anio,$mes,$dia) {
	if (!$anio || !$mes || !$dia)
		return false;

	if (!checkdate($mes,$dia,$anio))
		return false;

	return true;
}

function DateIsBlank($anio,$mes,$dia) {
	if (!$anio && !$mes && !$dia)
		return true;
	return false;
}

function DateMakeSql($anio,$mes,$dia) {
	if (!DateValidate($anio,$mes,$dia))
		return '';

	$anio = substr('0000'.$anio,-4);
	$mes = substr('00'.$mes,-2);
	$dia = substr('00'.$dia,-2);

	return("$anio-$mes-$dia");
//	return(date('Y-m-d',mktime(0,0,0,$mes+0,$dia+0,$anio+0)));
}

function IdGenreValidate($idgenre) {
	if ($idgenre != 1 && $idgenre != 2)
		return false;

	return true;
}

function GenreValidate($genre) {
	if ($genre != 'M' && $genre != 'F')
		return false;

	return true;
}

function EmailValidate($email) {
	if (!$email)
		return false;

	if (!strpos($email,'@'))
		return false;

	if (strpos($email,'@')<>strrpos($email,'@'))
		return false;

	return true;
}

?>