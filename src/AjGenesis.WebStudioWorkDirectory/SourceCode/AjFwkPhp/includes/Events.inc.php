<?
	include_once($Page->Prefix.'ajfwk/Database.inc.php');
	include_once($Page->Prefix.'ajfwk/Pages.inc.php');
	include_once('Users.inc.php');

function EventWrite($parameter,$type='PG',$idpar=0) {
	if (UserIsAdministrator())
		return;

	global $REMOTE_ADDR;

	$p = strpos($parameter,"?");

	if ($p>0) {
		$subparameter = substr($parameter,$p+1);
		$parameter = substr($parameter,0,$p);
	}

	DbConnect();

	$IdUser = UserId()+0;

	$sql = "insert ajl_events set
		Parameter = '$parameter',
		SubParameter = '$subparameter',
		Type = '$type',
		DateTime = now(),
		Ip = '$REMOTE_ADDR',
		IdParameter = $idpar,
		IdUser = $IdUser";

	if (!DbExecuteQuery($sql)) {
		echo DbLastSqlError();
		echo "<br>";
		echo $sql;
	}

	DbDisconnect();
}

function EventPage($idpar=0, $ispage='') {
	if (!$ispage)
		$page = PageActual();
	else
		$page = $ispage;

	$subpage = strrchr($page,'/');

	if ($subpage)
		$page = substr($subpage,1);

	EventWrite($page,'PG',$idpar);
}

function EventoMail($id,$page='')
{
	Conectar();
	$mid = SesionToma("MailId");

	if ($id<>$mid) {
		setcookie("CkMid",$mid);
		SesionPone("MailId",$id);
		EventoGraba($page,'RM',$id);
	}
		
	Desconectar();
}

function EventLogin() {
	EventWrite(UserName(),'LG');
}

function EventLogout() {
	EventWrite(UserName(),'LO');
}

function EventReferer() {
	global $HTTP_REFERER;
	global $HTTP_HOST;

	if (!$HTTP_REFERER || !$HTTP_HOST)
		return;

	if (strstr($HTTP_REFERER, $HTTP_HOST))
		return;

	EventGraba($HTTP_REFERER,'RF');
}

?>