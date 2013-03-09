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

	echo "&nbsp;&nbsp;&nbsp;&nbsp;";
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
	MenuLeftClose();

	MenuLeftOpen('Entities');
<#
	for each List in Project.Model.Lists
#>
	MenuLeftOption('${List.Title}','${List.Entity.Name}List.php');
<#
	end for
#>
	MenuLeftClose();
/*
	TODO: User Login and Register

	if (UserIdentified()) {
		MenuLeftOpen(UserName());
		MenuLeftOption('My Page', 'users/User.php');
		If (UserIsAdministrator()) {
			MenuLeftOption('Administrator','admin/index.php');
		}
		MenuLeftOption('Logout','users/Logout.php');
		MenuLeftClose();
	}
	else {
		MenuLeftOpen('Users');
		MenuLeftOption('Login','users/Login.php');
		MenuLeftOption('Register','users/Register.php');
		MenuLeftClose();
	}
*/
?>
