<?
	if (!$Entity)
		PageRedirect($Page->Prefix . 'index.php');

	$Page->Title = $Entity->SetName;
	
	include_once($Page->Prefix . 'ajfwk/includes/Connection.inc.php');
	include_once($Page->Prefix . 'ajfwk/includes/Users.inc.php');
	include_once($Page->Prefix . 'ajfwk/includes/Forms.inc.php');
	include_once($Page->Prefix . 'ajfwk/includes/Pages.inc.php');

	Connect();

	$sql = "select Id, Description from $Cfg[SqlPrefix]$Entity->Table order by 2";	 
	$rs = ExecuteQuery($sql);

	$titles = array("Descripci&oacute;n");

	include_once($Page->Prefix . 'includes/Header.inc.php');
?>

<center>

<p>
<a href="<?= $Entity->Entity ?>Form.php">Nuevo <?= $Entity->Name ?>...</a>
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
	DatumLinkGenerate($reg["Description"], $Entity->Entity . ".php?Id=".$reg["Id"]);
	RowClose();
}

?>