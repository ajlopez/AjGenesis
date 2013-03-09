function EmailValidate(entered,alertbox)
{
	with (entered) {
		apos=value.indexOf("@"); 
		dotpos=value.lastIndexOf(".");
		lastpos=value.length-1;
		if (apos<1|| dotpos-apos<2 /* || lastpos-dotpos>3 */ || lastpos-dotpos<2) {
			if (alertbox) {alert(alertbox);} 
			return false;
		}
		else {
			return true;
		}
	}
}

function IsBlank(txt)
{
	for (k=0;k<txt.length;k++)
		if (txt.charAt(k)!=' ')
			return false;

	return true;
}

