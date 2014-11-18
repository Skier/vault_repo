<?

include_once("Location.php");
include_once("include/connect.php");

class LocationInfo extends Location
{

    var $m_locationInfoId;
    var $m_locationName;
    var $m_language;

    function LocationInfo() {
    }

    function fetch($row) {
        $this->setLocationInfoId($row["LOCATION_INFO_ID"]);
        $this->setLocationName($row["LOCATION_NAME"]);
        $this->setLanguage($row["LANGUAGE"]);
        parent::fetch($row);
    }

    static function getByLocationId($locationId, $language) {
        $language = preprocess($language);
        $sql = "select li.LOCATION_INFO_ID, li.LOCATION_NAME, li.LANGUAGE,"
            . " l.LOCATION_ID, l.PARENT_LOCATION_ID"
            . " from VRBO_LOCATION_INFO li"
            . " inner join VRBO_LOCATION l on l.LOCATION_ID = li.LOCATION_ID"
            . " where li.LOCATION_ID = $locationId and li.LANGUAGE = $language";
        $res = execSql($sql);
        $rowsNumber = mysql_num_rows($res);
        if (0 == $rowsNumber) {
            return null;
        } else if (1 < $rowsNumber) {
            die("Duplicate location info found.");
        }

        $row = mysql_fetch_array($res);
        $item = new LocationInfo();
        $item->fetch($row);
        return $item;
    }

    static function getByParentId($parentId, $language) {
        $language = preprocess($language);
        $sql = "select li.LOCATION_INFO_ID, li.LOCATION_NAME, li.LANGUAGE,"
            . " l.LOCATION_ID, l.PARENT_LOCATION_ID"
            . "   from VRBO_LOCATION_INFO li"
            . "        inner join VRBO_LOCATION l on l.LOCATION_ID = li.LOCATION_ID"
            . "  where l.PARENT_LOCATION_ID " . ((is_null($parentId) || ("" == $parentId))? "is null": "= $parentId")
            . "    and li.LANGUAGE = $language"
            . "  order by li.LOCATION_NAME";
        $res = execSql($sql);

        $result = array();
        while($row = mysql_fetch_array($res)) {
            $item = new LocationInfo();
            $item->fetch($row);
            $result[] = $item;
        }
        return $result;
    }

    static function getPath($locationId, $language) {
        $item = LocationInfo::getByLocationId($locationId, $language);
        $pathArr[] = $item->getLocationName();
        while (!is_null($item->getParentId())) {
            $item = LocationInfo::getByLocationId($item->getParentId(), $language);
            $pathArr[] = $item->getLocationName();
        }
        $path = "";
        for ($index = count($pathArr) - 1; $index >= 0; $index --) {
            $path .= $pathArr[$index];
            if ($index > 0) {
                $path .= " -> ";
            }
        }
        return $path;
    }

    function setLocationInfoId($locationInfoId) {
        $this->m_locationInfoId = $locationInfoId;
    }

    function getLocationInfoId() {
        return $this->m_locationInfoId;
    }

    function setLocationName($locationName) {
        $this->m_locationName = trim($locationName);
    }

    function getLocationName() {
        return $this->m_locationName;
    }

    function setLanguage($language) {
        $this->m_language = trim($language);
    }

    function getLanguage() {
        return $this->m_language;
    }

}

?>
