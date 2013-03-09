<?

$Randomized=false;

function Randomize()
{
	$Randomized=1;
	srand((double)microtime()*1000000);
}

function Random($min, $max)
{
  if ($min == $max)
    return $min;


	if (!$Randomized)
		Randomize();

  if ($min < $max)
    return rand() % ($max - $min) + $min;
  else
    return rand() % ($min - $max) + $max;
}

function RandName($n)
{
  for ($i=0, $str = ""; $i<$n; $i++)
    $str .= chr(Random(ord('a'), ord('z')));
  return $str;
}


?>