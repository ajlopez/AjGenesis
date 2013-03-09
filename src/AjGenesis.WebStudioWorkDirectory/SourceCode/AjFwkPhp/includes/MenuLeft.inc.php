<?
	include_once($Page->Prefix.'includes/Users.inc.php');
?>
<br>

<center>

<?
function MenuLeftOpen($title)
{
?>
<p>
<table class="menu" cellspacing=1 cellpadding=2 width="95%">
<tr>
<td align=center class="menutitle">
<? echo $title; ?>
</td>
</tr>
</tr>
<td valign="top" class="menuoption">
<?
}

function MenuLeftOption($text,$link)
{
	global $Page;

	echo "&nbsp;&nbsp;<strong>·</strong>&nbsp;&nbsp;";
	echo "<a target='_top' href='$Page->Prefix$link' class='menuoption'>$text</a>";
	echo "<br>\n";
}

function MenuLeftClose()
{
?>
</td>
</tr>
</table>

<br>
<br>

</p>

<?
}
?>

<?
	MenuLeftOpen($Cfg['SiteName']);
	MenuLeftOption('Main','index.php');
	MenuLeftOption('Notes','Notes.php');
	MenuLeftClose();

	if (UserIdentified()) {
		MenuLeftOpen(UserName());
		MenuLeftOption('Su P&aacute;gina', 'users/User.php');
		If (UserIsAdministrator()) {
			MenuLeftOption('Administrador','admin/index.php');
		}
		MenuLeftOption('Salir','users/Logout.php');
		MenuLeftClose();
	}
	else {
		MenuLeftOpen('Usuario');
		MenuLeftOption('Ingrese','users/Login.php');
		MenuLeftOption('Registrarse','users/Register.php');
		MenuLeftClose();
	}

?>
