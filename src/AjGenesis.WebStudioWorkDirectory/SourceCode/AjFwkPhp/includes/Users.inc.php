<?
include_once($Page->Prefix.'ajfwk/Pages.inc.php');
include_once($Page->Prefix.'ajfwk/Database.inc.php');
include_once($Page->Prefix.'ajfwk/Session.inc.php');
include_once('Events.inc.php');

function UserControl($link='') {
	global $PHP_SELF;
	global $HTTP_SERVER_VARS;
	global $PagePrefix;

	$User = SessionGet("CurrentUser");
	$UserId = $User->Id;

	if (empty($UserId)) {
		if (empty($link)) {
			$enlace = $PHP_SELF;
			if ($HTTP_SERVER_VARS["QUERY_STRING"])
				$enlace .= "?" . $HTTP_SERVER_VARS["QUERY_STRING"];
		}
		SessionPut("UserLink", $link);
		PageRedirect('users/login.php');
		exit;
	}
}

function UserIdentified() {
	$User = SessionGet("CurrentUser");
	if (isset($User))
		return(true);
	return(false);	
}	

function UserVerified() {
	if (!UserIdentified())
		return false;
	$User = UserActual();
	if (IsSet($User))
		return true;
	return false;
}

function UserActual() {
	return SessionGet("CurrentUser");
}

function UserId() {
	$User = UserActual();
	return($User->Id);
}

function UserName() {
	$User = UserActual();
	return($User->UserName);
}

function UserPassword() {
	$User = UserActual();
	return($User->Password);
}

function UserFirstName() {
	$User = UserActual();
	return($User->FirstName);
}

function UserLastName() {
	$User = UserActual();
	return($User->LastName);
}

function UserGenre() {
	$User = UserActual();
	return($User->Genre);
}

function UserEmail() {
	$User = UserActual();
	return($User->Email);
}

function UserIsAdministrator() {
	$User = UserActual();
	return($User->IsAdministrator);
}

function UserIsUser() {
	if (!UserIsAdministrator())
		return true;
	return false;
}

function UserIsInRole($role) {
}

function UserRole() {
	if (UserIsAdministrator())
		return 'Administrator';
	if (UserIdentified() && UserIsUser())
		return 'User';
}

function AdministratorControl($link='') {
	UserControl($link);

	if (!UserIsAdministrator())
		PageRedirect(PageMain());
}

function UserLogin($user) {
	SessionPut("CurrentUser", $user);
	EventLogin();
	DbConnect();
	DbExecuteUpdate("update users set DateTimeLastLogin = now(), LoginCount = LoginCount+1 where Id = " . UserId());
	DbDisconnect();
}

function UserLogout() {
	EventLogout();
	SessionDestroy();
}

function UserTranslate($Id) {
	global $UsersTable;

	if (!$Id)
		return '';

	if ($UsersTabla[$Id])
		return $UsersTabla[$Id];

	DbConnect();

	$rs = DbExecuteQuery("select UserName from users where Id = $Id");

	if ($rs && DbNumRows($rs)) {
		$reg = DbNextRow($rs);
		$Code = $reg['UserName'];
	}
	else
		$Code = $Id;

	$UsersTable[$Id] = $Code;

	DbDisconnect();

	return $Code;
}
?>