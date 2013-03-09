<?
if (!$Page->NoChache) {
header("Expires: Mon, 20 Feb 1998 07:00:00 GMT");             // Date in the past
header("Last-Modified: " . gmdate("D, d M Y H:i:s") . "GMT"); // always modified
header("Cache-Control: no-cache, must-revalidate");           // HTTP/1.1
header("Pragma: no-cache");                                   // HTTP/1.0
}
?>