<?
	if (!$Entity)
		PageRedirect($Page->Prefix.'index.php');
	
	$Page->Title = 'Actualiza ' . $Entity->Name;

	include_once($Page->Prefix.'ajfwk/includes/Connection.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Users.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Errors.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Pages.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Session.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Translations.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Forms.inc.php');

	Connect();
	
	if (!ErrorHas() && isset($Id)) {
		$sql = "select Description from $Cfg[SqlPrefix]$Entity->Table where Id = $Id";
		$rs = ExecuteQuery($sql);
		$row = NextRow($rs);
		$Description = $row['Description'];
		$PageTitle = "Actualiza $Entity->Name $Description";
		$IsNew = 0;
	}	
	else if (isset($Id))
		$IsNew = 0;
	else {
		$PageTitle = "Nuevo " . $Entity->Name;
		$IsNew = 1;
	}

	include_once($Page->Prefix.'includes/Header.inc.php');
?>

<center>

<p>
<a href="<?= $Entity->Entities ?>.php"><?= $Entity->SetName ?></a>
&nbsp;
&nbsp;
<?
	if (!$IsNew) {
?>
<a href="<?= $Entity->Entity ?>.php?Id=<? echo $Id; ?>"><?= $Entity->Name ?></a>
&nbsp;
&nbsp;
<a href="<?= $Entity->Entity ?>Delete.php?Id=<? echo $Id; ?>">Elimina</a>
&nbsp;
&nbsp;
<?
	}
?>
</p>


<?
	ErrorRender();
?>

<p>

<form action="<?= $Entity->Entity ?>Update.php" method=post>

<table cellspacing=1 cellpadding=2 class="form" width='98%'>
<?
	if (!$IsNew)
		FieldStaticGenerate("Id",$Id);

	FieldTextGenerate("Description","Descripción",$Description,40,true);

	FieldOkGenerate();
?>
</table>

<?
	if (!$IsNew)
		FieldIdGenerate($Id);
?>

</form>

</center>

<?
	Disconnect();
	include_once($Page->Prefix.'includes/Footer.inc.php');
?>