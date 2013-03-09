<?
	include_once($Page->Prefix.'ajfwk/Configuration.inc.php');
	include_once($Page->Prefix.'ajfwk/Session.inc.php');
	include_once($Page->Prefix.'ajfwk/Cache.inc.php');
?>
<html>
<head>

<title><? echo $Cfg['SiteName']; ?> - <? echo $Page->Title; ?></title>

<META name="title" content="<? echo $Page->Title ?>">
<META name="description" content="<? echo $Cfg['SiteDescription'] ?>">
<META name="keywords" content="ajlopez, Angel Java Lopez">
<META name="language" content="es">
<META name="revisit-after" content="3 days">
<META name="rating" content="General">
<META name="author" content="Angel J Lopez">
<META name="owner" content="Angel J Lopez">
<META name="robot" content="index, follow">

<link rel="stylesheet" href="<? echo $Page->Prefix; ?>styles/style.css">
<?
	if ($Page->FileJs)
		echo "<script language='javascript' src='{$Page->Prefix}js/{$Page->FileJs}'></script>\n";
?>
</head>

