<?
  
	if (!$Entity)
		PageRedirect($Page->Prefix . 'index.php');

	$Page->Title = $Entity->Name;

	include_once($Page->Prefix.'ajfwk/includes/Connection.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Users.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Errors.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Pages.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Session.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Forms.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Utilities.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Translations.inc.php');

	Connect();
	
	SessionPut($Entity->Entity . 'Link',PageActual());

	if (!isset($Id))
		PageExit();

	$sql = "select Description from $Cfg[SqlPrefix]$Entity->Table where Id = $Id";
	$rs = ExecuteQuery($sql);
	$reg = NextRow($rs);
	$Description = $reg['Description'];

	include_once($Page->Prefix.'includes/Header.inc.php');
?>

<center>

<p>
<a href="<?= $Entity->Entities ?>.php"><?= $Entity->SetName ?></a>
&nbsp;
&nbsp;
<a href="<?= $Entity->Entity ?>Form.php?Id=<? echo $Id; ?>">Actualiza</a>
&nbsp;
&nbsp;
<a href="<?= $Entity->Entity ?>Delete.php?Id=<? echo $Id; ?>">Elimina</a>
</p>
<p>

<table cellspacing=1 cellpadding=2 class="form" width='90%'>
<?
	FieldStaticGenerate("Id",$Id);
	FieldStaticGenerate("Descripci&oacute;n", $Description);
?>
</table>

</center>

<?
	Disconnect();
	include_once($Page->Prefix.'includes/Footer.inc.php');
?>