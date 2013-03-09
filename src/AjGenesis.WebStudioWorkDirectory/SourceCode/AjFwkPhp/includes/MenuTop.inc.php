<?
	include_once($Page->Prefix.'ajfwk/includes/Pages.inc.php');
	include_once($Page->Prefix.'ajfwk/includes/Users.inc.php');
?>
<table width="100%" class="TemplateColorFondoContenido" cellpadding="0" cellspacing="0">
	
	<td width="<?=AnchoMenuIzquierdo?>">
		<table width="100%">
			<td>
				<table width="100%" class="TemplateColorMenuIzquierdo" cellpadding="1" cellspacing="1">
					<td>
						&nbsp;
					</td>
				</table>
			</td>
		</table>	
	</td>
	

	<td>
		<table align="center">
<?
function MenuTopeOpcion($texto,$enlace)
{
	global $PaginaPrefijo;
?>		
			<td>
				<table width="100" class="TemplateColorFondo" cellpadding="1" cellspacing="1">
					<td class="TemplateColorFondoMenu" align="center">
<a href="<?=$PaginaPrefijo.$enlace?>" class="MenuGlobalLink"><?=$texto?></a>
					</td>
				</table>
			</td>
<?
}
	MenuTopeOpcion("Principal",PageMain());
	MenuTopeOpcion("Cursos","CursosMuestra.php");
	if (UserIdentified())
		MenuTopeOpcion("Mi P&aacute;gina","user.php");
	else
		MenuTopeOpcion("Mi P&aacute;gina","nouser.php");
?>
	
		</table>	
	</td>
<!--	
	<td width="<?=AnchoMenuDerecho?>">
		<table width="100%">
			<td>
				<table width="100%" class="TemplateColorFondo" cellpadding="1" cellspacing="1">
					<td>
						&nbsp;
					</td>
				</table>
			</td>
		</table>	
	</td>
-->
	
</table>