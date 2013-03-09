<?

function ImageNormalize($image) {
	if (!$image)
		return $image;
	if (strstr($image,'/'))
		return $image;
	return 'images/' . $image;
}

function HtmlNormalize($html) {
	$txt = nl2br($html);
	$txt = str_replace("><br>",">",$txt);
	$txt = str_replace("><BR>",">",$txt);

	return $txt;
}

function EmailLink($email) {
	if (empty($email))
		return $email;

	if (!strpos($email,"@"))
		return $email;

	return "<a href='mailto:$email'>$email</a>";
}

function UrlNormalize($url) {
	if (!strpos($url,":/"))
		$url = "http://" . $url;
	return $url;
}

function UrlLink($url, $text='') {
	if (empty($url))
		return $text;

	if (!strpos($url,":/"))
		$url = "http://" . $url;

	if (!$text)
		$text = $url;

	return "<a href='$url'>$text</a>";
}

function UrlNewLink($url,$text='') {
	if (empty($url))
		return $text;

	if (!strpos($url,":/"))
		$url = "http://" . $url;

	if (!$text)
		$text = $url;

	return "<a href='$url' target='_blank'>$text</a>";
}

function TextYesNo($yesno) {
	if ($yesno)
		return "Sí";
	else
		return "No";
}

function TextGenre($genre) {
	if ($genre==2)
		return "Femenino";
	elseif ($genre==1)
		return "Masculino";
	else
		return '';
}

function WhereAdd($where, $condicion) {
	if ($where)
		return "$where and $condicion";
	else
		return $condicion;
}

?>