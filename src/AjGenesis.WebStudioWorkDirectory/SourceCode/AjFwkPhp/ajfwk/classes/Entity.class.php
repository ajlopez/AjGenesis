<?
class Entity {
	var	$Description;
	var	$Descriptor;
	var	$SetDescriptor;
	var	$Name;
	var	$SetName;

	var	$Fields;

	function GetTitles() {
		$titles = Array();

		foreach ($this->Fields as $fld) {
			$titles[] = $fld->GetTitle();
		}

		return $titles;
	}

	function GetFieldNames() {
		$names = Array();

		foreach ($this->Fields as $fld) {
			$names[] = $fld->Name;
		}

		return $names;
	}

	function GetFieldList() {
		$names = $this->GetFieldNames();
		return implode(',',$names);
	}
}

class EntityField {
	var	$Name;
	var	$Title;
	var	$Type;

	function EntityField($name,$title='',$type='') {
		$this->Name = $name;
		$this->Title = $title;
		$this->Type = $type;
	}

	function GetTitle() {
		if (!$this->Title)
			return $this->Name;
		return $this->Title;
	}
}
?>