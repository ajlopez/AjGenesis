<?
	if (!$Entity)
		PageRedirect($Page->Prefix . 'index.php');

	$Page->Title = $Entity->SetName;
	
	include_once($Page->Prefix . 'ajfwk/includes/Connection.inc.php');
	include_once($Page->Prefix . 'ajfwk/includes/Users.inc.php');
	include_once($Page->Prefix . 'ajfwk/includes/Forms.inc.php');
	include_once($Page->Prefix . 'ajfwk/includes/Pages.inc.php');

	Connect();

	SessionPut($Entity->Descriptor . 'Link',PageActual());

	$sql = "select " . $Entity->GetFieldList() . " from $Cfg[SqlPrefix]$Entity->Table order by 1";	 
	$rs = ExecuteQuery($sql);

	$titles = $Entity->GetTitles();

	include_once($Page->Prefix . 'includes/Header.inc.php');
?>

<center>

<p>
<a href="<?= $Entity->Descriptor ?>Form.php">Nuevo <?= $Entity->Name ?>...</a>
<p>

<?		
	TableOpen($titles,"98%");

	while ($reg=NextRow($rs)) 
		ShowRegister($reg);
				
	TableClose();
?>

</center>

<?
	include_once($Page->Prefix . 'includes/Footer.inc.php');
	Disconnect();

function ShowRegister($reg) {
	global $Entity;

	RowOpen();

	foreach ($Entity->Fields as $fld) {
		if ($fld->Name=='Id')
			DatumLinkGenerate($reg[$fld->Name],$Entity->Descriptor . ".php?Id=".$reg["Id"]);
		else
			DatumGenerate($reg[$fld->Name]);
	}

	RowClose();
}

?>