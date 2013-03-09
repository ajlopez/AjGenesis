<?
	if ($Page->NoCache)
		session_cache_limiter('must-revalidate');

	session_start();

function SessionPut($name,$value)
{
	$_SESSION[$name]=$value;
}

function SessionGet($name)
{
	return $_SESSION[$name];
}

function SessionRemove($name)
{
	unset($_SESSION[$name]);
}

function SessionDestroy()
{
	session_unset();
	session_destroy();
}

?>